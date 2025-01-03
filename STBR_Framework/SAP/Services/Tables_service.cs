using SAPbobsCOM;
using SAPbouiCOM;
using STBR_Framework.Enums;
using STBR_Framework.Models;
using STBR_Framework.Queries;
using STBR_Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace STBR_Framework.SAP.Services
{
    internal class Tables_service
    {
        private InfosAddon_service infoService;
        private InfosAddon InfosAddon;
        private List<OUTBModel> _userTables;
        public Tables_service()
        {
            infoService = new InfosAddon_service();
            InfosAddon = new InfosAddon();
            _userTables = GetUserTables();
        }


        private void AddTable(TableModel table)
        {

            try
            {
                int intRetCode = -1;
                SAPbobsCOM.UserTablesMD objUserTablesMD = null;

                //instancia objeto para criar tabela
                objUserTablesMD = (UserTablesMD)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.oUserTables);

                //seta propriedades
                string name = table.Name.ST_GetNameTable();
                string description = table.Description;

                if (!name.StartsWith(InfosAddon.CustomInfo.Prefix_name))
                    name = InfosAddon.CustomInfo.Prefix_name + table.Name;

                if (!description.ToUpper().StartsWith(InfosAddon.CustomInfo.Prefix_description))
                    description = InfosAddon.CustomInfo.Prefix_description + table.Description;

                objUserTablesMD.TableName = name.Length > 20 ? name.Substring(0, 20).ToUpper() : name.ToUpper();
                objUserTablesMD.TableDescription = description.Length > 30
                                                            ? description.Substring(0, 30).ToUpper()
                                                            : description.ToUpper();
                objUserTablesMD.TableType = table.TableTypeSAP;
                objUserTablesMD.ApplyAuthorization = table.ApplayAuthorization;
                objUserTablesMD.DisplayMenu = table.DisplayMenu;

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
        private void UpdateTable(TableModel table)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserTablesMD objUserTablesMD = null;

            try
            {
                //instancia objeto para atualizar tabela
                objUserTablesMD = (UserTablesMD)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.oUserTables);
                string name = table.Name.ST_GetNameTable();



                if (objUserTablesMD.GetByKey(name))
                {

                    string description = table.Description;

                    if (!description.ToUpper().StartsWith(InfosAddon.CustomInfo.Prefix_description))
                        description = InfosAddon.CustomInfo.Prefix_description + table.Description;

                    objUserTablesMD.TableDescription = description.Length > 30
                                                            ? description.Substring(0, 30).ToUpper()
                                                            : description.ToUpper();

                    objUserTablesMD.ApplyAuthorization = table.ApplayAuthorization;
                    objUserTablesMD.DisplayMenu = table.DisplayMenu;
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





        private List<OUTBModel> GetUserTables()
        {
            List<OUTBModel> userTables = new List<OUTBModel>();
            Recordset recSet = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            userTables = recSet.ST_DoQuery(QuerySelect.Select("outbList"), new OUTBModel());
            recSet.ST_ClearMemory();
            return userTables;
        }

        internal void ProcessTableJson(bool autoCreate = false)
        {
            ST_Mensagens.StatusBarWarning("Atualizando tabelas por Json... :: " + DateTime.Now.ToString("HH:mm:ss"));
            List<TableModel> tablesJson = infoService.ReadDataJson();
            //List<OUTBModel> userTables = GetUserTables();

            ProgressBar pbTables = ST_B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Tabelas JSON", tablesJson.Count, false);

            try
            {

                foreach (TableModel table in tablesJson)
                {
                    pbTables.Value++;
                    if (!table.AutoCreate && !autoCreate)
                        continue;

                    string name = (InfosAddon.CustomInfo.Prefix_name + table.Name).ToUpper();
                    pbTables.Text = name;

                    if (_userTables.Where(x => x.TableName == name.ToUpper()).Count() <= 0)
                    {
                        AddTable(table);
                    }
                    else
                    {
                        OUTBModel tb = _userTables.Where(x => x.TableName == name).SingleOrDefault();
                        string desc = InfosAddon.CustomInfo.Prefix_description + table.Description;
                        if (tb != null)
                            if (tb.Descr != desc.ToUpper())
                            {
                                UpdateTable(table);
                            }
                    }

                }

            }
            catch (Exception ex)
            {
                pbTables.Stop();
                pbTables.ST_ClearMemory();
                ST_Mensagens.StatusBarError(ex.Message);
            }
            finally
            {
                pbTables.Stop();
                pbTables.ST_ClearMemory();
                ST_Mensagens.StatusBarSuccess("Tabelas por Json Atualizadas com Sucesso :: " + DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        internal void ProcessTableClass()
        {
            ST_Mensagens.StatusBarWarning("Atualizando tabelas por Classe... :: " + DateTime.Now.ToString("HH:mm:ss"));
            ProgressBar pbTablesClass = ST_B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Tabelas Classes", ST_B1AppDomain.DictionaryTablesFields.Count, false);
            try
            {
                //List<OUTBModel> userTablesList = GetUserTables();

                foreach (KeyValuePair<object, TableModel> table in ST_B1AppDomain.DictionaryTablesFields.Where(x => x.Value.TableType == TableType.User))
                {
                    if (table.Value.AutoCreate == false)
                        continue;

                    pbTablesClass.Value++;
                    pbTablesClass.Text = table.Value.Name;
                    if (_userTables.Where(x => x.TableName == table.Value.Name).Count() <= 0)
                    {
                        AddTable(table.Value);
                    }
                    else
                    {
                        OUTBModel tb = _userTables.Where(x => x.TableName == table.Value.Name).SingleOrDefault();

                        if (tb.Descr != table.Value.Description)
                        {
                            UpdateTable(table.Value);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                pbTablesClass.Stop();
                pbTablesClass.ST_ClearMemory();
                ST_Mensagens.StatusBarError(ex.Message);
            }
            finally
            {
                pbTablesClass.Stop();
                pbTablesClass.ST_ClearMemory();
                ST_Mensagens.StatusBarSuccess("Tabelas por Classe Atualizadas com Sucesso :: " + DateTime.Now.ToString("HH:mm:ss"));
            }
        }





    }
}
