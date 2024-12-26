using SAPbobsCOM;
using STBR_Framework.Enums;
using STBR_Framework.Models;
using STBR_Framework.Utils;
using System.Collections.Generic;
using System;
using SAPbouiCOM;
using STBR_Framework.Queries;

namespace STBR_Framework.SAP.Services
{
    internal class Fields_service
    {
        private InfosAddon_service infoService;
        

        public Fields_service()
        {
            infoService = new InfosAddon_service();
        }

        private Dictionary<int, string> GetFieldsSap(string table)
        {
            string query = string.Format(QuerySelect.Select("getFieldsTable"), table);
            Dictionary<int, string> fields = new Dictionary<int, string>();
            Recordset oRs = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            oRs.DoQuery(query);
            while (!oRs.EoF)
            {
                fields.Add(int.Parse(oRs.Fields.Item("FieldID").Value.ToString()), oRs.Fields.Item("AliasID").Value.ToString());
                oRs.MoveNext();
            }
            oRs.ST_ClearMemory();
            return fields;
        }


        internal void ProcessFieldsJson()
        {
            try
            {
                List<TableModel> tablesJson = infoService.ReadDataJson();
                foreach (TableModel table in tablesJson)
                {
                    if (table.AutoCreate)
                    {
                        List<FieldModel> fields = table.Fields;
                        string tableName = table.TableType == TableType.User ? "@" + table.Name.ST_GetNameTable() : table.Name.ST_GetNameTable();
                        Dictionary<int, string> fieldsSap = GetFieldsSap(tableName);
                        ProgressBar pbFields = ST_B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Campos JSON", fields.Count, false);
                        pbFields.Text = table.Name;
                        foreach (FieldModel field in fields)
                        {
                            pbFields.Value++;
                            pbFields.Text = table.Name + " - " + field.Name;

                            if (!fieldsSap.ContainsValue(field.Name))
                            {
                                AddFields(field, table);
                            }
                        }

                        pbFields.Stop();
                        pbFields.ST_ClearMemory();

                    }

                }
            }
            catch (Exception ex)
            {

                ST_Mensagens.StatusBarError(ex.Message);
                ST_B1Exception.throwException("Error ProcessFieldsJson:", ex);
            }
        }

        internal void ProcessFieldsClass()
        {
            try
            {
                foreach (KeyValuePair<object, TableModel> table in ST_B1AppDomain.DictionaryTablesFields)
                {
                    if (table.Value.AutoCreate)
                    {
                        List<FieldModel> fields = table.Value.Fields;
                        Dictionary<int, string> fieldsSap = GetFieldsSap(table.Value.Name.ST_GetNameTable());
                        ProgressBar pbFields = ST_B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Campos Class", fields.Count, false);
                        pbFields.Text = table.Value.Name;
                        foreach (FieldModel field in fields)
                        {
                            pbFields.Value++;
                            pbFields.Text = table.Value.Name + " - " + field.Name;
                            if (!fieldsSap.ContainsValue(field.Name))
                            {
                                AddFields(field, table.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ST_Mensagens.StatusBarError(ex.Message);
                ST_B1Exception.throwException("Error ProcessFieldsClass:", ex);
            }
        }


        private void AddFields(FieldModel field, TableModel table)
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
            objUserFieldsMD.EditSize = field.Type == BoFieldTypes.db_Numeric ? 11 : field.Size;
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
                ST_B1Exception.throwException(intRetCode, "Field: " + objUserFieldsMD.Name);
            }

            //mata objeto para reutilizar senao trava
            objUserFieldsMD.ST_ClearMemory();
        }


    }
}
