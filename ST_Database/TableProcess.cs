using SAPbobsCOM;
using SAPbouiCOM;
using ST_Database.Models;
using ST_Extensions;
using STBR_Framework;
using STBR_Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST_Database
{
    internal class TableProcess
    {

        internal static void Update()
        {
            List<TableModel> tablesJson = Process.ReadDataJson();
            OUTBModel userTables = new OUTBModel();

            #region TABELAS VIA JSON

            List<OUTBModel> userTablesList = userTables.Tables();
            ProgressBar pbTables = ST_B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Tabelas JSON", tablesJson.Count, false);
            foreach (TableModel table in tablesJson)
            {
                pbTables.Value++;
                pbTables.Text = table.Name;
                ProcessTable(table);
                if (table.Udos != null && table.Udos.Children.Count > 0)
                    foreach (UdoChildsModel udoChild in table.Udos.Children)
                    {
                        ProcessTable(new TableModel
                        {
                            Name = udoChild.Name,
                            Description = udoChild.Description,
                            TableTypeSAP = udoChild.TableTypeSAP,
                            TableType = udoChild.TableType
                        });
                    }
            }
            pbTables.Stop();
            pbTables.ST_ClearMemory();

            #endregion

            #region TABELAS VIA CLASSE

            userTablesList = userTables.Tables();
            ProgressBar pbTablesClass = ST_B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Tabelas Classes", Process.DictionaryTablesFields.Count, false);
            foreach (KeyValuePair<object, TableModel> table in Process.DictionaryTablesFields.Where(x => x.Value.TableType == TableType.User))
            {
                pbTablesClass.Value++;
                pbTablesClass.Text = table.Value.Name;
                if (userTablesList.Where(x => x.TableName == table.Value.Name).Count() <= 0)
                {
                    AddTable(table.Value);
                }
                else
                {
                    OUTBModel tb = userTablesList.Where(x => x.TableName == table.Value.Name).SingleOrDefault();

                    if (tb.Descr != table.Value.Description)
                    {
                        UpdateTable(table.Value);
                    }

                }
            }
            pbTablesClass.Stop();
            pbTablesClass.ST_ClearMemory();

            #endregion

            void ProcessTable(TableModel table)
            {
                if (userTablesList.Where(x => x.TableName == DefaultNames.Prefix.ToUpper() + "_" + table.Name.ToUpper()).Count() <= 0)
                {
                    AddTable(table);
                }
                else
                {
                    OUTBModel tb = userTablesList.Where(x => x.TableName == DefaultNames.Prefix.ToUpper() + "_" + table.Name.ToUpper()).SingleOrDefault();


                    string description = table.Description;

                    if (!description.ToUpper().StartsWith("ST-"))
                        description = DefaultNames.Prefix + "-" + table.Description;

                    description = description.Length > 30
                                             ? description.Substring(0, 30).ToUpper()
                                             : description.ToUpper();


                    if (tb.Descr.ToUpper() != description)
                    {
                        UpdateTable(table);
                    }
                }
            }

        }

        internal static void AddTable(TableModel table)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserTablesMD objUserTablesMD = null;

            try
            {
                //instancia objeto para criar tabela
                objUserTablesMD = (UserTablesMD)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.oUserTables);

                //seta propriedades
                //string name = table.Name.ST_GetNameTable();
                string name = table.Name;
                string description = table.Description;


                //if (!description.ToUpper().StartsWith("ST-"))
                //    description = DefaultNames.Prefix + "-" + table.Description;

                objUserTablesMD.TableName = name.Length > 20 ? name.Substring(0, 20).ToUpper() : name.ToUpper();
                objUserTablesMD.TableDescription = description.Length > 30
                                                            ? description.Substring(0, 30).ToUpper()
                                                            : description.ToUpper();
                objUserTablesMD.TableType = table.TableTypeSAP;

                //adiciona tabela
                intRetCode = objUserTablesMD.Add();
                //verifica e retorna erro
                if (intRetCode != 0 && intRetCode != -2035)
                {
                    ST_B1Exception.throwException(intRetCode);
                }

                //mata objeto para reutilizar senao trava
                objUserTablesMD.ST_ClearMemory();


            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao Criar Tabela :: " + table.Name + " - ", ex);

            }
        }

        private static void UpdateTable(TableModel table)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserTablesMD objUserTablesMD = null;

            try
            {
                //instancia objeto para atualizar tabela
                objUserTablesMD = (UserTablesMD)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.oUserTables);
                //string name = table.Name.ST_GetNameTable();
                string name = table.Name;



                if (objUserTablesMD.GetByKey(name))
                {

                    string description = table.Description;

                    //if (!description.ToUpper().StartsWith("ST-"))
                    //    description = DefaultNames.Prefix + "-" + table.Description;

                    objUserTablesMD.TableDescription = description.Length > 30
                                                            ? description.Substring(0, 30).ToUpper()
                                                            : description.ToUpper();

                    //atualiza tabela
                    intRetCode = objUserTablesMD.Update();
                    //verifica e retorna erro
                    if (intRetCode != 0 && intRetCode != -2035)
                    {
                        ST_B1Exception.throwException("MetaData.UpdateTable: ", new Exception(ST_B1AppDomain.Company.GetLastErrorDescription()));
                    }

                }
                else
                {
                    ST_B1AppDomain.Application.SetStatusBarMessage("Tabela não localizada no sistema :: " + table.Name, BoMessageTime.bmt_Short, true);
                }
                objUserTablesMD.ST_ClearMemory();

            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao Atualizar Tabela :: " + table.Name + " - ", ex);
            }
        }

    }
}
