using SAPbobsCOM;
using SAPbouiCOM;
using ST_Database.Bases;
using ST_Database.Extensions;
using ST_Database.Models;
using ST_Database.Queries;
using ST_Extensions;
using STBR_Framework;
using STBR_Framework.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ST_Database
{
    internal class FieldsProcess
    {
        internal static void Update()
        {
            List<TableModel> tablesJson = Process.ReadDataJson();
            CUFDModel userFields = new CUFDModel();
            OUTBModel userTables = new OUTBModel();

            #region CAMPOS VIA JSON

            List<OUTBModel> userTablesList = userTables.Tables();
            int qtyFields = tablesJson.Sum(x => x.Fields.Count());
            qtyFields += tablesJson.Where(x => x.Udos != null).Sum(x => x.Udos.Children.Sum(y => y.Fields.Count));


            ProgressBar pbFields = ST_B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Campos de usuários", qtyFields, false);

            //percorre as tabelas do json
            foreach (TableModel table in tablesJson)
            {
                table.Name = table.Name.ST_GetNameTable();
                //verifica se a tabela é do tipo usuário ou de sistema e busca os campos
                List<CUFDModel> userFieldList = table.TableType == TableType.System ?
                    userFields.Fields(table.Name) :
                    userFields.Fields("@" + table.Name);

                //percorre os campos da tabela
                foreach (FieldModel field in table.Fields)
                {
                    pbFields.Value++;
                    pbFields.Text = table.Name + " - " + field.Name;

                    if (userFieldList.Count(p => p.AliasID.ToUpper() == field.Name.ToUpper() &&
                                           p.TableID.ToUpper() == (table.TableType == TableType.System ?
                                                                        table.Name.ToUpper() : "@" + field.Name.ToUpper())) <= 0)
                    {
                        AddField(field, table);
                    }
                    else
                    {
                        if (VerifyField(field, table, userFieldList.Where(p => p.AliasID.ToUpper() == field.Name.ToUpper() &&
                                                                   p.TableID.ToUpper() == (table.TableType == TableType.System ?
                                                                                                 table.Name.ToUpper() : "@" + table.Name.ToUpper())).SingleOrDefault()))
                        {
                            UpdateField(field, table);
                        }
                    }
                }
            }

            pbFields.Stop();
            pbFields.ST_ClearMemory();

            #endregion

        }

        private static bool VerifyField(FieldModel field, TableModel table, CUFDModel cUFD)
        {
            bool mand = cUFD.NotNull == "Y";
            if (mand != (field.Mandatory == BoYesNoEnum.tYES) && field.Type != BoFieldTypes.db_Date && field.Type != BoFieldTypes.db_Memo)
            {
                return true;
            }
            if (field.Description != cUFD.Descr)
            {
                return true;
            }
            if (field.Name != cUFD.AliasID)
            {
                return true;
            }
            if (field.SubType != RelationalReader.GetSubType(cUFD.EditType))
            {
                return true;
            }
            if (field.TableReference != null)
            {
                if (field.TableReference != cUFD.RTable)
                {
                    return true;
                }
            }

            if (field.Size != cUFD.EditSize && field.Type == BoFieldTypes.db_Alpha && field.Type == BoFieldTypes.db_Numeric)
            {
                return true;
            }
            if (field.Size != cUFD.EditSize && field.Size > cUFD.EditSize && (field.Type == BoFieldTypes.db_Alpha || field.Type == BoFieldTypes.db_Numeric))
            {
                return true;
            }

            if (field.DefaultValue != null)
            {
                if (field.DefaultValue != cUFD.Dflt)
                {
                    return true;
                }
            }

            if (field.Type != RelationalReader.GetFieldType(cUFD.TypeID))
            {
                return true;
            }
            if (field.UdoReference != null)
            {
                if (field.UdoReference != cUFD.RelUDO)
                {
                    return true;
                }
            }

            if (field.ValidValues != null)
            {
                UFD1Model ufd1 = new UFD1Model();
                foreach (UFD1Model item in ufd1.FieldItems(table.Name.ST_GetNameTable(), field.Name))
                {
                    if (field.ValidValues.Where(p => p.Value == item.FldValue && p.Description == item.Descr).Count() <= 0)
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        private static void UpdateField(FieldModel field, TableModel table)
        {
            int intRetCode = -1;

            SAPbobsCOM.UserFieldsMD objUserFieldsMD = null;

            //instancia objeto para alterar campo
            objUserFieldsMD = (UserFieldsMD)ST_B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);


            if (objUserFieldsMD.GetByKey("@" + table.Name.ToUpper(), idField(field.Name, table.Name, table.TableType)))
            {

                //seta propriedades
                objUserFieldsMD.EditSize = field.Size;
                objUserFieldsMD.Mandatory = field.Mandatory;
                objUserFieldsMD.Description = field.Description;
                objUserFieldsMD.DefaultValue = field.DefaultValue;
                objUserFieldsMD.AddValidValues(field.ValidValues);


                //Atualiza Campos campo
                intRetCode = objUserFieldsMD.Update();
                //verifica e retorna erro
                if (intRetCode != 0 && intRetCode != -2035)
                {
                    ST_B1Exception.throwException(intRetCode);
                }
            }

            //mata objeto para reutilizar senao trava
            objUserFieldsMD.ST_ClearMemory();

        }

        private static int idField(string name, string tableName, TableType tableType)
        {
            Recordset _rset = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            string query = "";
            if (tableType == TableType.System)
            {
                query = string.Format(QuerySelect.Select("getFieldID"), name, tableName.ToUpper());
            }
            else
            {
                query = string.Format(QuerySelect.Select("getFieldID"), name, "@" + tableName.ToUpper());
            }


            _rset.DoQuery(query);

            string ret = _rset.Fields.Item(0).Value.ToString();
            _rset.ST_ClearMemory();

            return Convert.ToInt32(ret);
        }

        private static void AddField(FieldModel field, TableModel table)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserFieldsMD objUserFieldsMD = null;
            table.Name = table.Name.ST_GetNameTable();

            //instancia objeto para criar campo
            objUserFieldsMD = (UserFieldsMD)ST_B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

            //seta propriedades
            objUserFieldsMD.Name = field.Name;
            objUserFieldsMD.TableName = table.TableType == TableType.User ? "@" + table.Name.ToUpper() : table.Name.ToUpper();
            objUserFieldsMD.Type = field.Type;
            objUserFieldsMD.SubType = field.SubType;
            objUserFieldsMD.EditSize = field.Size;
            objUserFieldsMD.Mandatory = field.Mandatory;
            objUserFieldsMD.Description = field.Description;
            objUserFieldsMD.AddValidValues(field.ValidValues);
            if (!string.IsNullOrEmpty(field.UdoReference))
            {
                objUserFieldsMD.LinkedUDO = field.UdoReference;
            }

            if (!string.IsNullOrEmpty(field.TableReference))
            {
                objUserFieldsMD.LinkedTable = field.TableReference;
            }


            objUserFieldsMD.DefaultValue = field.DefaultValue;
            //adiciona campo
            intRetCode = objUserFieldsMD.Add();
            //verifica e retorna erro
            if (intRetCode != 0 && intRetCode != -2035)
            {
                ST_B1Exception.throwException(intRetCode);
            }

            //mata objeto para reutilizar senao trava
            objUserFieldsMD.ST_ClearMemory();

        }



    }
}
