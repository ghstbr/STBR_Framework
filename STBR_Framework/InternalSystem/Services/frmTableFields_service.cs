using SAPbobsCOM;
using SAPbouiCOM;
using STBR_Framework.Models;
using STBR_Framework.SAP.Services;
using STBR_Framework.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace STBR_Framework.InternalSystem.Services
{
    internal class frmTableFields_service
    {
        private InfosAddon_service _infos = new InfosAddon_service();
        private InfoModel _info = new InfoModel();

        public frmTableFields_service()
        {
            _info = _infos.ReadInfoJson();
        }


        internal void AjustaCampos(Grid oGridTabelas)
        {
            try
            {
                //oGridTabelas.Item.Height = 150;
            }
            catch { }

        }

        internal void CarregaCombos(Form oForm)
        {
            ComboBox oCombo = (ComboBox)oForm.Items.Item("cbTipoT").Specific;
            foreach (BoUTBTableType item in System.Enum.GetValues(typeof(BoUTBTableType)))
                oCombo.ValidValues.Add(item.GetHashCode().ToString(), item.ToString());


            oCombo = (ComboBox)oForm.Items.Item("cbTipoC").Specific;
            foreach (BoFieldTypes item in System.Enum.GetValues(typeof(BoFieldTypes)))
                oCombo.ValidValues.Add(item.GetHashCode().ToString(), item.ToString());

            oCombo = (ComboBox)oForm.Items.Item("cbSubC").Specific;
            foreach (BoFldSubTypes item in System.Enum.GetValues(typeof(BoFldSubTypes)))
                oCombo.ValidValues.Add(item.GetHashCode().ToString(), item.ToString());




        }

        internal void FiltraComboSubTipo(Form oForm, string selectedValue)
        {
            ComboBox oComboSub = (ComboBox)oForm.Items.Item("cbSubC").Specific;
            for (int i = 0; i < oComboSub.ValidValues.Count; i++)
            {
                oComboSub.ValidValues.Remove(0, BoSearchKey.psk_Index);
                i--;
            }
            oForm.DataSources.UserDataSources.Item("UD_SubC").Value = "";

            switch (selectedValue)
            {
                case "0":
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_None.GetHashCode().ToString(), BoFldSubTypes.st_None.ToString());
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_Address.GetHashCode().ToString(), BoFldSubTypes.st_Address.ToString());
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_Phone.GetHashCode().ToString(), BoFldSubTypes.st_Phone.ToString());
                    oForm.DataSources.UserDataSources.Item("UD_SubC").Value = "0";
                    break;

                case "1":
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_None.GetHashCode().ToString(), BoFldSubTypes.st_None.ToString());
                    oForm.DataSources.UserDataSources.Item("UD_SubC").Value = "0";
                    break;

                case "2":
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_None.GetHashCode().ToString(), BoFldSubTypes.st_None.ToString());
                    oForm.DataSources.UserDataSources.Item("UD_SubC").Value = "0";
                    break;

                case "3":
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_None.GetHashCode().ToString(), BoFldSubTypes.st_None.ToString());
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_Time.GetHashCode().ToString(), BoFldSubTypes.st_Time.ToString());
                    oForm.DataSources.UserDataSources.Item("UD_SubC").Value = "0";
                    break;

                case "4":
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_Rate.GetHashCode().ToString(), BoFldSubTypes.st_Rate.ToString());
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_Sum.GetHashCode().ToString(), BoFldSubTypes.st_Sum.ToString());
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_Price.GetHashCode().ToString(), BoFldSubTypes.st_Price.ToString());
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_Quantity.GetHashCode().ToString(), BoFldSubTypes.st_Quantity.ToString());
                    oComboSub.ValidValues.Add(BoFldSubTypes.st_Percentage.GetHashCode().ToString(), BoFldSubTypes.st_Percentage.ToString());
                    oForm.DataSources.UserDataSources.Item("UD_SubC").Value = "82";
                    break;

                default:
                    break;

            }
        }


        internal void NovaTabela(Form oForm)
        {
            string nomeArquivo = oForm.DataSources.UserDataSources.Item("UD_Nome").ValueEx;

            if (string.IsNullOrEmpty(nomeArquivo))
            {
                ST_Mensagens.StatusBarError("Informe o nome da tabela");
                return;
            }

            string path = Directory.GetCurrentDirectory() + _info.DirectoryTables;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //ler diretorio e verificar se ja existe arquivo com mesmo nome e se sim, numero sequencial
            int i = 0;
            Directory.GetFiles(path, _info.FilterFiles)
                .Where(x => Path.GetFileNameWithoutExtension(x).Contains(nomeArquivo))
                .OrderBy(x => Path.GetFileNameWithoutExtension(x).Contains(nomeArquivo))
                .ToList().ForEach(x =>
                {
                    if (Path.GetFileNameWithoutExtension(x).Contains(nomeArquivo))
                        i++;
                });



            TableModel table = new TableModel();
            if (i <= 0)
                table.FileName = nomeArquivo + ".json";
            else
                table.FileName = nomeArquivo + i.ToString() + ".json";

            table.Name = "NovaTabela";
            table.Description = "Descrição da nova tabela";
            table.TableTypeSAP = BoUTBTableType.bott_NoObject;
            table.DisplayMenu = BoYesNoEnum.tNO;
            table.ApplayAuthorization = BoYesNoEnum.tNO;

            string newTable = Newtonsoft.Json.JsonConvert.SerializeObject(table);


            File.Create(path + "\\" + table.FileName).Close();
            File.WriteAllText(path + "\\" + table.FileName, newTable);
            oForm.Close();
            //CarregaTabelas((Grid)oForm.Items.Item("gdTabelas").Specific);

        }



        internal void CarregaTabelas(Grid oGridTabelas)
        {
            DataTable oDT = oGridTabelas.DataTable;
            oDT.Rows.Clear();
            string path = Directory.GetCurrentDirectory() + _info.DirectoryTables;
            if (Directory.Exists(path))
            {
                foreach (string arquivo in Directory.GetFiles(path, _info.FilterFiles))
                {
                    string json = File.ReadAllText(arquivo);
                    string nomeArquivo = Path.GetFileName(arquivo);
                    TableModel table = Newtonsoft.Json.JsonConvert.DeserializeObject<TableModel>(json);
                    oDT.Rows.Add();
                    oDT.SetValue("Id", oDT.Rows.Count - 1, nomeArquivo);
                    oDT.SetValue("TableName", oDT.Rows.Count - 1, table.Name);
                    oDT.SetValue("TableDescription", oDT.Rows.Count - 1, table.Description);
                    oDT.SetValue("TableType", oDT.Rows.Count - 1, Enum.GetName(typeof(BoUTBTableType), table.TableTypeSAP));
                    oDT.SetValue("DisplayMenu", oDT.Rows.Count - 1, table.DisplayMenu == BoYesNoEnum.tYES ? "Y" : "N");
                    oDT.SetValue("ApplayAuthorization", oDT.Rows.Count - 1, table.ApplayAuthorization == BoYesNoEnum.tYES ? "Y" : "N");
                    oDT.SetValue("AutoCreate", oDT.Rows.Count - 1, table.AutoCreate ? "Y" : "N");
                }
            }
            FormataGridTabelas(oGridTabelas);

        }

        private void FormataGridTabelas(Grid oGrid)
        {
            for (int i = 0; i < oGrid.Columns.Count; i++)
            {
                switch (oGrid.Columns.Item(i).TitleObject.Caption)
                {
                    case "Id":
                        oGrid.Columns.Item(i).Width = 20;
                        oGrid.Columns.Item(i).Editable = false;
                        break;
                    case "TableName":
                        oGrid.Columns.Item(i).Width = 100;
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Nome da Tabela";
                        break;
                    case "TableDescription":
                        oGrid.Columns.Item(i).Width = 200;
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Descrição da Tabela";
                        break;
                    case "TableType":
                        oGrid.Columns.Item(i).Width = 100;
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Tipo de Tabela";


                        break;

                    case "DisplayMenu":
                        oGrid.Columns.Item(i).Width = 20;
                        oGrid.Columns.Item(i).Type = BoGridColumnType.gct_CheckBox;
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Exibir Menu";
                        break;
                    case "ApplayAuthorization":
                        oGrid.Columns.Item(i).Width = 20;
                        oGrid.Columns.Item(i).Type = BoGridColumnType.gct_CheckBox;
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Aplicar Autorização";
                        break;

                    case "AutoCreate":
                        oGrid.Columns.Item(i).Width = 20;
                        oGrid.Columns.Item(i).Type = BoGridColumnType.gct_CheckBox;
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Auto Criar";
                        break;

                    default:
                        break;
                }
            }
        }

        private void FormataGridCampos(Grid oGrid)
        {
            for (int i = 0; i < oGrid.Columns.Count; i++)
            {
                switch (oGrid.Columns.Item(i).TitleObject.Caption)
                {
                    case "Name":
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Nome";
                        break;

                    case "Description":
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Descrição";
                        break;

                    case "Type":
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Tipo";
                        break;

                    case "SubType":
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "SubTipo";
                        break;

                    case "Mandatory":
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).Type = BoGridColumnType.gct_CheckBox;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Obrigatório";
                        break;

                    case "DefaultValue":
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Valor Padrão";
                        break;

                    case "Size":
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Tamanho";
                        break;

                    case "ValidValues":
                        oGrid.Columns.Item(i).Editable = false;
                        oGrid.Columns.Item(i).Type = BoGridColumnType.gct_CheckBox;
                        oGrid.Columns.Item(i).TitleObject.Caption = "Valores Válidos";
                        break;


                    default:
                        break;
                }
            }
        }


        internal TableModel SelecionaTabela(string tableName)
        {
            TableModel table = new TableModel();
            string path = Directory.GetCurrentDirectory() + _info.DirectoryTables;
            if (Directory.Exists(path))
            {
                foreach (string arquivo in Directory.GetFiles(path, _info.FilterFiles))
                {
                    string json = File.ReadAllText(arquivo);
                    TableModel tableTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<TableModel>(json);
                    if (string.IsNullOrEmpty(tableTemp.FileName))
                        tableTemp.FileName = Path.GetFileName(arquivo);

                    if (tableTemp.Name == tableName)
                    {
                        table = tableTemp;
                        break;
                    }
                }
            }
            return table;
        }

        private TableModel SelecionaTabelaFile(string fileName)
        {
            TableModel table = new TableModel();
            string path = Directory.GetCurrentDirectory() + _info.DirectoryTables;
            if (Directory.Exists(path))
            {
                foreach (string arquivo in Directory.GetFiles(path, _info.FilterFiles))
                {
                    string json = File.ReadAllText(arquivo);
                    string nomeArquivo = Path.GetFileName(arquivo);
                    TableModel tableTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<TableModel>(json);
                    if (nomeArquivo == fileName)
                    {
                        table = tableTemp;
                        break;
                    }
                }

            }
            return table;
        }

        internal void SelecionaTabela(Form oForm, int row)
        {
            try
            {
                Grid oGridTables = (Grid)oForm.Items.Item("gdTabelas").Specific;
                string tableName = oGridTables.DataTable.GetValue("TableName", row).ToString();
                string fileName = oGridTables.DataTable.GetValue("Id", row).ToString();
                TableModel table = SelecionaTabelaFile(fileName);
                if (table.Name != null)
                {
                    oForm.Freeze(true);
                    oForm.DataSources.UserDataSources.Item("UD_NomeT").ValueEx = table.Name;
                    oForm.DataSources.UserDataSources.Item("UD_DescT").ValueEx = table.Description;
                    oForm.DataSources.UserDataSources.Item("UD_TipoT").ValueEx = table.TableTypeSAP.GetHashCode().ToString();
                    oForm.DataSources.UserDataSources.Item("UD_ViewM").ValueEx = table.DisplayMenu == BoYesNoEnum.tYES ? "Y" : "N";
                    oForm.DataSources.UserDataSources.Item("UD_Auth").ValueEx = table.ApplayAuthorization == BoYesNoEnum.tYES ? "Y" : "N";
                    oForm.DataSources.UserDataSources.Item("UD_CAuto").ValueEx = table.AutoCreate ? "Y" : "N";
                    oForm.Freeze(false);
                }
                CarregaCamposGrid(oForm, table);

            }
            catch (Exception ex)
            {
                ST_Mensagens.StatusBarError(ex.Message);
            }
        }

        internal void CarregaCamposGrid(Form oForm, TableModel table)
        {
            Grid oGridFields = (Grid)oForm.Items.Item("gdCampos").Specific;
            oGridFields.DataTable.Rows.Clear();
            if (table.Fields != null)
            {
                ProgressBar pb = ST_B1AppDomain.Application.StatusBar.CreateProgressBar("Carregando campos...", table.Fields.Count, false);
                foreach (FieldModel field in table.Fields)
                {
                    pb.Value++;

                    oGridFields.DataTable.Rows.Add();
                    oGridFields.DataTable.SetValue("Name", oGridFields.DataTable.Rows.Count - 1, field.Name);
                    oGridFields.DataTable.SetValue("Description", oGridFields.DataTable.Rows.Count - 1, field.Description);
                    oGridFields.DataTable.SetValue("Type", oGridFields.DataTable.Rows.Count - 1, Enum.GetName(typeof(BoFieldTypes), field.Type));
                    oGridFields.DataTable.SetValue("SubType", oGridFields.DataTable.Rows.Count - 1, Enum.GetName(typeof(BoFldSubTypes), field.SubType));
                    oGridFields.DataTable.SetValue("Size", oGridFields.DataTable.Rows.Count - 1, field.Size);
                    oGridFields.DataTable.SetValue("Mandatory", oGridFields.DataTable.Rows.Count - 1, field.Mandatory == BoYesNoEnum.tYES ? "Y" : "N");

                    if (field.ValidValues == null)
                        oGridFields.DataTable.SetValue("ValidValues", oGridFields.DataTable.Rows.Count - 1, "N");
                    else
                        oGridFields.DataTable.SetValue("ValidValues", oGridFields.DataTable.Rows.Count - 1, field.ValidValues.Count > 0 ? "Y" : "N");

                    oGridFields.DataTable.SetValue("DefaultValue", oGridFields.DataTable.Rows.Count - 1, field.DefaultValue);
                }
                pb.Stop();
            }
            FormataGridCampos(oGridFields);
        }

        internal void SalvaTabela(Form oForm)
        {
            try
            {
                string nomeTabela = oForm.DataSources.UserDataSources.Item("UD_NomeT").ValueEx;
                Grid grid = (Grid)oForm.Items.Item("gdTabelas").Specific;
                string fileName = string.Empty;
                for (int i = 0; i < grid.DataTable.Rows.Count; i++)
                {
                    if (grid.Rows.IsSelected(i))
                    {
                        fileName = grid.DataTable.GetValue("Id", i).ToString();
                        break;
                    }

                }

                string path = Directory.GetCurrentDirectory() + _info.DirectoryTables;
                if (Directory.Exists(path))
                {
                    foreach (string arquivo in Directory.GetFiles(path, _info.FilterFiles))
                    {
                        string json = File.ReadAllText(arquivo);
                        string fileNameTemp = Path.GetFileName(arquivo);
                        TableModel tableTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<TableModel>(json);
                        if (fileNameTemp == fileName)
                        {
                            tableTemp.Name = oForm.DataSources.UserDataSources.Item("UD_NomeT").ValueEx;
                            tableTemp.Description = oForm.DataSources.UserDataSources.Item("UD_DescT").ValueEx;
                            tableTemp.TableTypeSAP = (BoUTBTableType)Enum.Parse(typeof(BoUTBTableType), oForm.DataSources.UserDataSources.Item("UD_TipoT").ValueEx);
                            tableTemp.DisplayMenu = oForm.DataSources.UserDataSources.Item("UD_ViewM").ValueEx == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                            tableTemp.ApplayAuthorization = oForm.DataSources.UserDataSources.Item("UD_Auth").ValueEx == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                            tableTemp.AutoCreate = oForm.DataSources.UserDataSources.Item("UD_CAuto").ValueEx == "Y" ? true : false;
                            if (string.IsNullOrEmpty(tableTemp.FileName))
                                tableTemp.FileName = fileNameTemp;
                            string newTable = Newtonsoft.Json.JsonConvert.SerializeObject(tableTemp);
                            File.WriteAllText(arquivo, newTable);
                            ST_Mensagens.StatusBarSuccess("Dados Tabela salvos com sucesso!");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ST_Mensagens.StatusBarError(ex.Message);
            }
        }

        internal void DeletaTabela(Grid oGridTabelas)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + _info.DirectoryTables;
                if (Directory.Exists(path))
                {

                    for (int row = 0; row < oGridTabelas.Rows.Count; row++)
                    {
                        if (oGridTabelas.Rows.IsSelected(row))
                        {
                            string fileName = oGridTabelas.DataTable.GetValue("Id", row).ToString();
                            File.Delete(path + "\\" + fileName);
                            oGridTabelas.DataTable.Rows.Remove(row);
                            ST_Mensagens.StatusBarSuccess("Tabela deletada com sucesso");
                            break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ST_Mensagens.StatusBarError(ex.Message);
            }
        }

        internal void SelecionaCampo(Form oForm, int row)
        {
            try
            {
                Grid oGridFields = (Grid)oForm.Items.Item("gdCampos").Specific;
                string fieldName = oGridFields.DataTable.GetValue("Name", row).ToString();
                string tableName = oForm.DataSources.UserDataSources.Item("UD_NomeT").ValueEx;
                oForm.DataSources.UserDataSources.Item("UD_NomeC").ValueEx = oGridFields.DataTable.GetValue("Name", row).ToString();
                oForm.DataSources.UserDataSources.Item("UD_DescC").ValueEx = oGridFields.DataTable.GetValue("Description", row).ToString();
                oForm.DataSources.UserDataSources.Item("UD_TipoC").ValueEx = oGridFields.DataTable.GetValue("Type", row).ToString();
                string nomeTipo = oGridFields.DataTable.GetValue("Type", row).ToString();
                string idTipo = Enum.Parse(typeof(BoFieldTypes), nomeTipo).GetHashCode().ToString();
                FiltraComboSubTipo(oForm, idTipo);
                oForm.DataSources.UserDataSources.Item("UD_SubC").ValueEx = oGridFields.DataTable.GetValue("SubType", row).ToString();
                oForm.DataSources.UserDataSources.Item("UD_Tam").ValueEx = oGridFields.DataTable.GetValue("Size", row).ToString();
                oForm.DataSources.UserDataSources.Item("UD_Obrig").ValueEx = oGridFields.DataTable.GetValue("Mandatory", row).ToString();
                oForm.DataSources.UserDataSources.Item("UD_ValVal").ValueEx = oGridFields.DataTable.GetValue("ValidValues", row).ToString();
                oForm.DataSources.UserDataSources.Item("UD_ValPC").ValueEx = oGridFields.DataTable.GetValue("DefaultValue", row).ToString();

                Grid oGridValidValues = (Grid)oForm.Items.Item("gdValorV").Specific;
                oGridValidValues.DataTable.Rows.Clear();
                if (oGridFields.DataTable.GetValue("ValidValues", row).ToString() == "Y")
                {
                    FieldModel field = new FieldModel();
                    TableModel table = SelecionaTabela(tableName);
                    field = table.Fields.Where(x => x.Name == fieldName).FirstOrDefault();
                    if (field.ValidValues != null)
                    {
                        foreach (ValidValuesModel validValue in field.ValidValues)
                        {
                            oGridValidValues.DataTable.Rows.Add();
                            oGridValidValues.DataTable.SetValue("Value", oGridValidValues.DataTable.Rows.Count - 1, validValue.Value);
                            oGridValidValues.DataTable.SetValue("Description", oGridValidValues.DataTable.Rows.Count - 1, validValue.Description);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                ST_Mensagens.StatusBarError(ex.Message);
            }
        }

        internal void SalvaCampo(Form oForm, string tableName)
        {
            try
            {
                //pegar o nome do campo da grid selecionado
                Grid oGridFields = (Grid)oForm.Items.Item("gdCampos").Specific;
                string fieldName = string.Empty;
                for (int i = 0; i < oGridFields.Rows.Count; i++)
                {
                    if (oGridFields.Rows.IsSelected(i))
                    {
                        fieldName = oGridFields.DataTable.GetValue("Name", i).ToString();
                        break;
                    }
                }

                string path = Directory.GetCurrentDirectory() + _info.DirectoryTables;
                if (Directory.Exists(path))
                {
                    foreach (string arquivo in Directory.GetFiles(path, _info.FilterFiles))
                    {
                        string json = File.ReadAllText(arquivo);
                        TableModel tableTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<TableModel>(json);
                        if (tableTemp.Name == tableName)
                        {
                            FieldModel field = new FieldModel();
                            field.Name = oForm.DataSources.UserDataSources.Item("UD_NomeC").ValueEx;
                            field.Description = oForm.DataSources.UserDataSources.Item("UD_DescC").ValueEx;
                            field.Type = (BoFieldTypes)Enum.Parse(typeof(BoFieldTypes), oForm.DataSources.UserDataSources.Item("UD_TipoC").ValueEx);
                            field.SubType = (BoFldSubTypes)Enum.Parse(typeof(BoFldSubTypes), oForm.DataSources.UserDataSources.Item("UD_SubC").ValueEx);
                            field.Mandatory = oForm.DataSources.UserDataSources.Item("UD_Obrig").ValueEx == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                            field.DefaultValue = oForm.DataSources.UserDataSources.Item("UD_ValPC").ValueEx;
                            field.Size = int.Parse(oForm.DataSources.UserDataSources.Item("UD_Tam").ValueEx);
                            List<ValidValuesModel> list = new List<ValidValuesModel>();
                            if (oForm.DataSources.UserDataSources.Item("UD_ValVal").ValueEx == "Y")
                            {
                                Grid oGridValidValues = (Grid)oForm.Items.Item("gdValorV").Specific;
                                for (int i = 0; i < oGridValidValues.DataTable.Rows.Count; i++)
                                {
                                    ValidValuesModel validValue = new ValidValuesModel();
                                    validValue.Value = oGridValidValues.DataTable.GetValue("Value", i).ToString();
                                    validValue.Description = oGridValidValues.DataTable.GetValue("Description", i).ToString();
                                    if (!string.IsNullOrEmpty(validValue.Value))
                                        list.Add(validValue);

                                }
                                field.ValidValues = list;
                            }
                            else
                                field.ValidValues = list;

                            if (tableTemp.Fields == null)
                                tableTemp.Fields = new List<FieldModel>();

                            FieldModel fieldTemp = tableTemp.Fields.Where(x => x.Name == fieldName).FirstOrDefault();

                            if (fieldTemp != null)
                            {
                                tableTemp.Fields.Update(x => x.Name == fieldName, field);
                            }
                            else
                                tableTemp.Fields.Add(field);

                            string newTable = Newtonsoft.Json.JsonConvert.SerializeObject(tableTemp);
                            File.WriteAllText(arquivo, newTable);
                            ST_Mensagens.StatusBarSuccess("Dados Campo salvos com sucesso!");





                            break;
                        }
                    }
                }
                CarregaCamposGrid(oForm, SelecionaTabela(tableName));
            }
            catch (Exception ex)
            {
                ST_Mensagens.StatusBarError(ex.Message);
            }

        }

        internal void DeletaCampo(Form oForm, string tableName)
        {
            try
            {
                //pegar o nome do campo da grid selecionado
                Grid oGridFields = (Grid)oForm.Items.Item("gdCampos").Specific;
                string fieldName = string.Empty;
                for (int i = 0; i < oGridFields.Rows.Count; i++)
                {
                    if (oGridFields.Rows.IsSelected(i))
                    {
                        fieldName = oGridFields.DataTable.GetValue("Name", i).ToString();
                        break;
                    }
                }
                string path = Directory.GetCurrentDirectory() + _info.DirectoryTables;
                if (Directory.Exists(path))
                {
                    foreach (string arquivo in Directory.GetFiles(path, _info.FilterFiles))
                    {
                        string json = File.ReadAllText(arquivo);
                        TableModel tableTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<TableModel>(json);
                        if (tableTemp.Name == tableName)
                        {
                            tableTemp.Fields.Remove(tableTemp.Fields.Where(x => x.Name == fieldName).FirstOrDefault());
                            string newTable = Newtonsoft.Json.JsonConvert.SerializeObject(tableTemp);
                            File.WriteAllText(arquivo, newTable);
                            ST_Mensagens.StatusBarSuccess("Campo deletado com sucesso!");
                            break;
                        }
                    }
                }
                CarregaCamposGrid(oForm, SelecionaTabela(tableName));
            }
            catch (Exception ex)
            {
                ST_Mensagens.StatusBarError(ex.Message);
            }
        }

        internal void NovoCampo(Form oForm, string campo, string tabela)
        {
            try
            {


                string path = Directory.GetCurrentDirectory() + _info.DirectoryTables;
                if (Directory.Exists(path))
                {
                    foreach (string arquivo in Directory.GetFiles(path, _info.FilterFiles))
                    {
                        string json = File.ReadAllText(arquivo);
                        TableModel tableTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<TableModel>(json);
                        if (tableTemp.Name == tabela)
                        {
                            FieldModel field = new FieldModel();
                            field.Name = campo;
                            field.Description = "Descrição do campo";
                            field.Type = BoFieldTypes.db_Alpha;
                            field.SubType = BoFldSubTypes.st_None;
                            field.Mandatory = BoYesNoEnum.tNO;
                            field.DefaultValue = string.Empty;
                            field.Size = 50;
                            field.ValidValues = new List<ValidValuesModel>();
                            if (tableTemp.Fields == null)
                                tableTemp.Fields = new List<FieldModel>();
                            tableTemp.Fields.Add(field);
                            string newTable = Newtonsoft.Json.JsonConvert.SerializeObject(tableTemp);
                            File.WriteAllText(arquivo, newTable);
                            ST_Mensagens.StatusBarSuccess("Campo criado com sucesso!");
                            oForm.Close();
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                ST_Mensagens.StatusBarError(ex.Message);
            }
        }








    }
}
