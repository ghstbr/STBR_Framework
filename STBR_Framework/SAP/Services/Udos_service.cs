using SAPbobsCOM;
using SAPbouiCOM;
using STBR_Framework.Models;
using STBR_Framework.Utils;
using System.Collections.Generic;
using System;
using System.Linq;
using STBR_Framework.Queries;

namespace STBR_Framework.SAP.Services
{
    internal class Udos_service
    {
        private static bool controlUdo { get; set; }
        private InfosAddon_service infoService;
        

        public Udos_service()
        {
            infoService = new InfosAddon_service();
            
        }


        internal void ProcessUdoJson()
        {
            try
            {
                List<TableModel> tablesJson = infoService.ReadDataJson();

            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Error ProcessUdoJson :: ", ex);
                ST_Mensagens.StatusBarError(ex.Message);
            }
        }

        internal void ProcessUdoClasses()
        {
            try
            {
                controlUdo = false;
                ProgressBar pbUdos = ST_B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Registrando Udos", ST_B1AppDomain.DictionaryUdos.Count, false);
                foreach (KeyValuePair<object, UdoModel> udo in ST_B1AppDomain.DictionaryUdos)
                {
                    pbUdos.Value++;
                    pbUdos.Text = "Registrando Udo " + udo.Value.Code;
                    TableModel table = ST_B1AppDomain.DictionaryTablesFields.Where(x => x.Value.Name == udo.Value.Name).FirstOrDefault().Value;

                    table.Udos = udo.Value;
                    foreach (KeyValuePair<object, UdoChildsModel> child in ST_B1AppDomain.DictionaryUdosChilds.Where(p => p.Value.TableFather == udo.Value.Name))
                    {
                        if (table.Udos.Children == null)
                        {
                            table.Udos.Children = new List<UdoChildsModel>();
                        }
                        table.Udos.Children.Add(child.Value);
                    }

                    Recordset oRsReg = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                    string sql = String.Format(QuerySelect.Select("udoList"), table.Udos.Code);
                    oRsReg.DoQuery(sql);
                    oRsReg.MoveFirst();
                    string currentScreen = oRsReg.Fields.Item(1).Value.ToString();
                    int qtd = oRsReg.RecordCount;
                    oRsReg.ST_ClearMemory();
                    //string newScreen = !string.IsNullOrEmpty(table.Udos.Form) ?
                    //                            VZ_B1AppDomain.DictionaryUdosForms.Where(x => x.Key == table.Udos.Form).Select(x => x.Value).SingleOrDefault() :
                    //                            "";
                    string newScreen = string.Empty;
                    if (string.IsNullOrEmpty(newScreen))
                        newScreen = "";

                    int comp = string.Compare(currentScreen, newScreen.Replace("\r\n", "\r"));
                    if (!string.IsNullOrEmpty(currentScreen) && comp != 0)
                    {
                        UpdateUdo(table);
                    }
                    else if (string.IsNullOrEmpty(currentScreen) && comp != 0)
                    {
                        AddUdo(table);
                    }
                    else if (comp == 0 && qtd <= 0)
                    {
                        AddUdo(table);
                    }
                }

                pbUdos.Stop();
                pbUdos.ST_ClearMemory();

                ST_Mensagens.StatusBarSuccess("Verificação de UDO concluida!");

                if (controlUdo)
                {
                    ST_Mensagens.Box("Dados foram alterados o sistema será reiniciado");
                    ST_B1AppDomain.Company.Disconnect();
                    ST_B1AppDomain.Application.Menus.Item("3329").Activate();
                    ST_B1AppDomain.Application.Forms.ActiveForm.Items.Item("3").Click();
                }

            }
            catch (Exception ex)
            {
                ST_B1Exception.writeLog(ex.Message);
            }
        }


        private static void AddUdo(TableModel table)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;

            oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)(ST_B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)));
            oUserObjectMD.CanCancel = table.Udos.Cancel;
            oUserObjectMD.CanClose = table.Udos.Close;
            oUserObjectMD.CanCreateDefaultForm = table.Udos.CreateDefaultForm;
            oUserObjectMD.EnableEnhancedForm = table.Udos.EnableEnhancedform;
            if (table.Udos.CreateDefaultForm == SAPbobsCOM.BoYesNoEnum.tYES)
            {
                if (table.Udos.ObjectType != BoUDOObjType.boud_Document)
                {
                    oUserObjectMD.FormColumns.FormColumnAlias = "Code";
                    oUserObjectMD.FormColumns.FormColumnDescription = "Code";
                }
                else
                {
                    oUserObjectMD.FormColumns.FormColumnAlias = "DocEntry";
                    oUserObjectMD.FormColumns.FormColumnDescription = "DocEntry";
                }

                int voltaCampos = 1;
                foreach (FieldModel field in table.Fields)
                {
                    oUserObjectMD.FormColumns.Add();
                    oUserObjectMD.FormColumns.SetCurrentLine(voltaCampos);
                    oUserObjectMD.FormColumns.FormColumnAlias = "U_" + field.Name;
                    oUserObjectMD.FormColumns.FormColumnDescription = field.Description;
                    oUserObjectMD.FormColumns.Editable = BoYesNoEnum.tYES;
                    oUserObjectMD.FormColumns.SonNumber = 0;
                    voltaCampos++;
                }

            }

            oUserObjectMD.CanDelete = table.Udos.Delete;
            oUserObjectMD.CanFind = table.Udos.Find;
            if (table.Udos.Find == SAPbobsCOM.BoYesNoEnum.tYES)
            {
                if (table.Udos.ObjectType != BoUDOObjType.boud_Document)
                {
                    oUserObjectMD.FindColumns.ColumnAlias = "Code";
                    oUserObjectMD.FindColumns.ColumnDescription = "Code";
                    oUserObjectMD.FindColumns.Add();
                    oUserObjectMD.FindColumns.ColumnAlias = "Name";
                    oUserObjectMD.FindColumns.ColumnDescription = "Name";
                }
                else
                {
                    oUserObjectMD.FindColumns.ColumnAlias = "DocEntry";
                    oUserObjectMD.FindColumns.ColumnDescription = "DocEntry";
                    oUserObjectMD.FindColumns.ColumnAlias = "DocNum";
                    oUserObjectMD.FindColumns.ColumnDescription = "DocNum";
                }

                int voltaFind = 1;
                foreach (FieldModel field in table.Fields)
                {
                    oUserObjectMD.FindColumns.Add();
                    oUserObjectMD.FindColumns.SetCurrentLine(voltaFind);
                    oUserObjectMD.FindColumns.ColumnAlias = "U_" + field.Name;
                    oUserObjectMD.FindColumns.ColumnDescription = field.Description;
                    voltaFind++;
                }
            }


            oUserObjectMD.CanYearTransfer = table.Udos.YearTransfer;
            oUserObjectMD.Code = table.Udos.Code;
            oUserObjectMD.ManageSeries = table.Udos.ManageSeries;
            oUserObjectMD.Name = table.Udos.Name;
            oUserObjectMD.ObjectType = table.Udos.ObjectType;
            oUserObjectMD.TableName = table.Name.ST_GetNameTable();


            if (table.Udos.Log)
            {
                oUserObjectMD.CanLog = BoYesNoEnum.tYES;
                oUserObjectMD.LogTableName = "L" + table.Name.ST_GetNameTable();
            }


            int numerofilho = 1;

            if (table.Udos.Children != null)
            {
                foreach (UdoChildsModel listUdo in table.Udos.Children)
                {
                    if (numerofilho > 1)
                    {
                        oUserObjectMD.ChildTables.Add();
                    }

                    oUserObjectMD.ChildTables.TableName = listUdo.Name;
                    oUserObjectMD.ChildTables.ObjectName = listUdo.Name;

                    if (table.Udos.EnableEnhancedform == BoYesNoEnum.tNO)
                    {
                        foreach (TableModel tableChild in ST_B1AppDomain.DictionaryTablesFields.Where(p => p.Value.Name == listUdo.Name).Select(p => p.Value))
                        {
                            int voltaFilhos = 1;
                            foreach (FieldModel field in tableChild.Fields)
                            {
                                oUserObjectMD.FormColumns.Add();
                                oUserObjectMD.FormColumns.SetCurrentLine(voltaFilhos);
                                oUserObjectMD.FormColumns.FormColumnAlias = "U_" + field.Name;
                                oUserObjectMD.FormColumns.FormColumnDescription = field.Description;
                                oUserObjectMD.FormColumns.Editable = BoYesNoEnum.tYES;
                                oUserObjectMD.FormColumns.SonNumber = numerofilho;
                                voltaFilhos++;
                            }

                        }

                    }
                    else
                    {
                        foreach (TableModel tableChild in ST_B1AppDomain.DictionaryTablesFields.Where(p => p.Value.Name == listUdo.Name).Select(p => p.Value))
                        {
                            int voltaNovo = 0;
                            foreach (FieldModel field in tableChild.Fields)
                            {

                                if (voltaNovo > 0)
                                {
                                    oUserObjectMD.EnhancedFormColumns.Add();
                                }

                                oUserObjectMD.EnhancedFormColumns.SetCurrentLine(voltaNovo);
                                oUserObjectMD.EnhancedFormColumns.ColumnAlias = "U_" + field.Name;
                                oUserObjectMD.EnhancedFormColumns.ColumnDescription = field.Description;
                                oUserObjectMD.EnhancedFormColumns.Editable = BoYesNoEnum.tYES;
                                oUserObjectMD.EnhancedFormColumns.ColumnIsUsed = BoYesNoEnum.tYES;
                                oUserObjectMD.EnhancedFormColumns.ChildNumber = numerofilho;
                                voltaNovo++;

                            }
                        }
                    }

                    numerofilho++;

                }
            }

            oUserObjectMD.RebuildEnhancedForm = table.Udos.RebuildEnhancedForm;

            //oUserObjectMD.FormSRF = table.Udos.Form;

            //oUserObjectMD.FormSRF =
            //    !string.IsNullOrEmpty(table.Udos.Form) ?
            //    VZ_B1AppDomain.DictionaryUdosForms.Where(x => x.Key == table.Udos.Form).Select(x => x.Value).SingleOrDefault() :
            //    "";

            intRetCode = oUserObjectMD.Add();

            //mata objeto para reutilizar senao trava
            oUserObjectMD.ST_ClearMemory();

            //verifica e retorna erro
            if (intRetCode != 0 && intRetCode != -2035)
            {
                ST_B1Exception.throwException(intRetCode);
            }
            else
            {
                controlUdo = true;
            }

        }

        private static void UpdateUdo(TableModel table)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;

            oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)(ST_B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)));
            oUserObjectMD.GetByKey(table.Udos.Code);
            oUserObjectMD.CanCancel = table.Udos.Cancel;
            oUserObjectMD.CanClose = table.Udos.Close;
            oUserObjectMD.CanCreateDefaultForm = table.Udos.CreateDefaultForm;
            oUserObjectMD.EnableEnhancedForm = table.Udos.EnableEnhancedform;
            oUserObjectMD.CanDelete = table.Udos.Delete;
            oUserObjectMD.CanFind = table.Udos.Find;
            oUserObjectMD.CanYearTransfer = table.Udos.YearTransfer;
            oUserObjectMD.Code = table.Udos.Code;
            oUserObjectMD.ManageSeries = table.Udos.ManageSeries;
            oUserObjectMD.Name = table.Udos.Name;
            oUserObjectMD.ObjectType = table.Udos.ObjectType;
            oUserObjectMD.TableName = table.Name.ST_GetNameTable();
            oUserObjectMD.RebuildEnhancedForm = table.Udos.RebuildEnhancedForm;

            //oUserObjectMD.FormSRF =
            //    !string.IsNullOrEmpty(table.Udos.Form) ?
            //    VZ_B1AppDomain.DictionaryUdosForms.Where(x => x.Key == table.Udos.Form).Select(x => x.Value).SingleOrDefault() :
            //    "";

            intRetCode = oUserObjectMD.Update();

            //mata objeto para reutilizar senao trava
            oUserObjectMD.ST_ClearMemory();


            //verifica e retorna erro
            if (intRetCode != 0 && intRetCode != -2035)
            {
                string teste = ST_B1AppDomain.Company.GetLastErrorDescription();
                ST_B1AppDomain.Application.SetStatusBarMessage(teste, BoMessageTime.bmt_Short, true);
                ST_B1Exception.throwException(intRetCode);

            }
            else
            {
                controlUdo = true;
            }

        }


    }
}
