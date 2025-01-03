using SAPbouiCOM;
using System;
using System.Collections.Generic;
using STBR_Framework.Attributes;
using STBR_Framework.InternalSystem.Services;
using STBR_Framework.Utils;
using RE = STBR_Framework.InternalSystem.Forms.FormsFiles;


namespace STBR_Framework.Default.forms
{
    [ST_Form("frmTableFields", "Tabelas de usuários")]
    internal class frmTableFields : ST_FormBase
    {
        frmTableFields_service _service = new frmTableFields_service();
        private bool ctrlNewTable = false;
        private bool ctrlNewField = false;
        string _frmNovoCampo = "frmNewField";
        string _frmNovaTabela = "frmNewTable";
        internal Grid oGridTabelas;
        internal Grid oGridValoresValidos;
        internal Grid oGridCampos;
        internal Button oBtNovaTabela;
        internal Button oBtDeletaTabela;
        internal Button oBtAtualizaGrid;
        internal Button oBtSalvaTabela;
        internal Button oBtNovoV;
        internal Button oBtDeletaV;
        internal Button oBtSalvaCampo;
        internal Button oBtDeletaCampo;
        internal Button oBtNovoCampo;
        internal Button oBtUdo;
        internal CheckBox oCkValoresValidos;
        internal ComboBox oCbTipoCampo;
        internal ComboBox oCbTipoTabela;
        internal EditText oEtNomeTabela;

        internal UserDataSource udNomeTabela;
        internal UserDataSource udDescTabela;
        internal UserDataSource udTipoTabela;
        internal UserDataSource udViewM;
        internal UserDataSource udAuth;
        internal UserDataSource udCAuto;

        internal UserDataSource udNomeCampo;
        internal UserDataSource udDescCampo;
        internal UserDataSource udTipoCampo;
        internal UserDataSource udSubTipoCampo;
        internal UserDataSource udTamanhoCampo;
        internal UserDataSource udValorPadraoCampo;
        internal UserDataSource udValorValidoCampo;
        internal UserDataSource udObrigatorioCampo;


        public override void OnCustomInitialize()
        {
            //oForm.Freeze(true);
            oForm.FreezeForm();
            _service.CarregaCombos(this.oForm);

            #region Grids

            oGridTabelas = (Grid)oForm.Items.Item("gdTabelas").Specific;
            oGridValoresValidos = (Grid)oForm.Items.Item("gdValorV").Specific;
            oGridCampos = (Grid)oForm.Items.Item("gdCampos").Specific;

            #endregion

            #region Buttons

            oBtNovaTabela = (Button)oForm.Items.Item("btNovaT").Specific;
            oBtDeletaTabela = (Button)oForm.Items.Item("btDelT").Specific;
            oBtAtualizaGrid = (Button)oForm.Items.Item("btAtual").Specific;
            oBtSalvaTabela = (Button)oForm.Items.Item("btSalvar").Specific;
            oBtUdo = (Button)oForm.Items.Item("btUdo").Specific;

            oBtNovoV = (Button)oForm.Items.Item("btNovoV").Specific;
            oBtDeletaV = (Button)oForm.Items.Item("btDeletaV").Specific;

            oBtSalvaCampo = (Button)oForm.Items.Item("btSalvaC").Specific;
            oBtDeletaCampo = (Button)oForm.Items.Item("btDeletaC").Specific;
            oBtNovoCampo = (Button)oForm.Items.Item("btNovoC").Specific;

            #endregion

            #region CheckBox

            oCkValoresValidos = (CheckBox)oForm.Items.Item("ckValidC").Specific;

            #endregion

            #region EditText

            oEtNomeTabela = (EditText)oForm.Items.Item("etNomeT").Specific;

            #endregion

            #region ComboBox

            oCbTipoCampo = (ComboBox)oForm.Items.Item("cbTipoC").Specific;
            oCbTipoTabela = (ComboBox)oForm.Items.Item("cbTipoT").Specific;

            #endregion

            #region UserDataSource

            udNomeTabela = oForm.DataSources.UserDataSources.Item("UD_NomeT");
            udDescTabela = oForm.DataSources.UserDataSources.Item("UD_DescT");
            udTipoTabela = oForm.DataSources.UserDataSources.Item("UD_TipoT");
            udViewM = oForm.DataSources.UserDataSources.Item("UD_ViewM");
            udAuth = oForm.DataSources.UserDataSources.Item("UD_Auth");
            udCAuto = oForm.DataSources.UserDataSources.Item("UD_CAuto");

            udNomeCampo = oForm.DataSources.UserDataSources.Item("UD_NomeC");
            udDescCampo = oForm.DataSources.UserDataSources.Item("UD_DescC");
            udTipoCampo = oForm.DataSources.UserDataSources.Item("UD_TipoC");
            udSubTipoCampo = oForm.DataSources.UserDataSources.Item("UD_SubC");
            udTamanhoCampo = oForm.DataSources.UserDataSources.Item("UD_Tam");
            udValorPadraoCampo = oForm.DataSources.UserDataSources.Item("UD_ValPC");
            udValorValidoCampo = oForm.DataSources.UserDataSources.Item("UD_ValVal");
            udObrigatorioCampo = oForm.DataSources.UserDataSources.Item("UD_Obrig");

            #endregion


            oGridValoresValidos.AutoResizeColumns();
            OcultaValoresValidos(true);
            _service.CarregaTabelas(oGridTabelas);
            //oForm.Freeze(false);
            oForm.FreezeForm(false);
        }

        private void OcultaValoresValidos(bool oculta)
        {
            oForm.Freeze(true);
            oGridValoresValidos.Item.Visible = !oculta;
            oBtNovoV.Item.Visible = !oculta;
            oBtDeletaV.Item.Visible = !oculta;
            oForm.Freeze(false);
        }


        public override void Form_Resize_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            _service.AjustaCampos(oGridTabelas);
        }

        public override void ItemClicked_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (pVal.ItemUID == oCkValoresValidos.Item.UniqueID && pVal.ActionSuccess)
                OcultaValoresValidos(oCkValoresValidos.Checked);
        }

        public override void FormActivate_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (ctrlNewTable)
            {
                try
                {
                    oForm.Freeze(true);
                    ctrlNewTable = false;
                    _service.CarregaTabelas(oGridTabelas);
                    oForm.Freeze(false);
                }
                catch (System.Exception ex)
                {
                    oForm.Freeze(false);
                    ST_Mensagens.StatusBarError(ex.Message);
                }

            }

            if (ctrlNewField)
            {
                try
                {
                    oForm.Freeze(true);
                    ctrlNewField = false;
                    _service.CarregaCamposGrid(oForm, _service.SelecionaTabela(udNomeTabela.ValueEx));
                    oForm.Freeze(false);
                }
                catch (System.Exception ex)
                {
                    oForm.Freeze(false);
                    ST_Mensagens.StatusBarError(ex.Message);
                }

            }

        }

        public override void ItemPressed_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            try
            {
                oForm.Freeze(true);

                if (pVal.ItemUID == oBtNovaTabela.Item.UniqueID)
                {
                    ClearDataTables();
                    ST_Forms.OpenUserForm(_frmNovaTabela, RE.frmNewTable);
                    ctrlNewTable = true;
                }

                if (pVal.ItemUID == oBtDeletaTabela.Item.UniqueID)
                    _service.DeletaTabela(oGridTabelas);


                if (pVal.ItemUID == oBtSalvaTabela.Item.UniqueID)
                {
                    _service.SalvaTabela(oForm);
                    _service.CarregaTabelas(oGridTabelas);
                    ClearDataTables();
                }


                if (pVal.ItemUID == oBtAtualizaGrid.Item.UniqueID)
                    _service.CarregaTabelas(oGridTabelas);

                if (pVal.ItemUID == oGridTabelas.Item.UniqueID && pVal.ColUID == "RowsHeader" && pVal.ActionSuccess && pVal.Row >= 0)
                {
                    _service.SelecionaTabela(this.oForm, pVal.Row);
                    ClearDataFields();
                }


                if (pVal.ItemUID == oGridCampos.Item.UniqueID && pVal.ColUID == "RowsHeader" && pVal.ActionSuccess && pVal.Row >= 0)
                {
                    _service.SelecionaCampo(this.oForm, pVal.Row);
                    OcultaValoresValidos(!oCkValoresValidos.Checked);
                }

                if (pVal.ItemUID == oBtSalvaCampo.Item.UniqueID)
                {
                    _service.SalvaCampo(oForm, udNomeTabela.Value);
                    ClearDataFields();
                }


                if (pVal.ItemUID == oBtDeletaCampo.Item.UniqueID)
                    _service.DeletaCampo(oForm, udNomeTabela.Value);

                if (pVal.ItemUID == oBtNovoCampo.Item.UniqueID)
                {
                    Form newForm = ST_Forms.OpenUserForm(_frmNovoCampo, RE.frmNewField);
                    if (newForm != null)
                    {
                        newForm.DataSources.UserDataSources.Item("UD_NomeT").Value = udNomeTabela.Value;
                    }

                    ctrlNewField = true;
                }

                if (pVal.ItemUID == oBtNovoV.Item.UniqueID)
                    oGridValoresValidos.DataTable.Rows.Add();

                if (pVal.ItemUID == oBtDeletaV.Item.UniqueID)
                {
                    for (int i = 0; i < oGridValoresValidos.DataTable.Rows.Count; i++)
                    {
                        if (oGridValoresValidos.Rows.IsSelected(i))
                            oGridValoresValidos.DataTable.Rows.Remove(i);
                    }
                }

                if (pVal.ItemUID == oBtUdo.Item.UniqueID)
                {
                    string nomeTabela = oGridTabelas.ST_GetValueSelected("TableName").ToString();
                    string fileName = oGridTabelas.ST_GetValueSelected("Id").ToString();
                    if (string.IsNullOrEmpty(nomeTabela))
                    {
                        ST_Mensagens.Box("Selecione uma tabela para criar o UDO");
                        throw new Exception("Selecione uma tabela para criar o UDO");
                    }


                    if (oCbTipoTabela.Selected.Value != "1" && oCbTipoTabela.Selected.Value != "3")
                    {
                        ST_Mensagens.Box("Apenas tabelas de Cadastros e Documentos podem ser convertidas em UDO");
                        throw new Exception("Apenas tabelas de Cadastros e Documentos podem ser convertidas em UDO");
                    }

                    Dictionary<string, object> parms = new Dictionary<string, object>();
                    parms.Add("nomeTabela", nomeTabela);
                    parms.Add("fileName", fileName);
                    ST_Forms.OpenUserForm("frmUdo", RE.frmUdo, parms);
                    
                    
                }



                oForm.Freeze(false);
            }
            catch (System.Exception ex)
            {
                oForm.Freeze(false);
                ST_Mensagens.StatusBarError(ex.Message);
            }
            finally
            {
                oForm.Freeze(false);
            }


        }

        public override void ComboSelect_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            try
            {
                if (pVal.ItemUID == oCbTipoCampo.Item.UniqueID && pVal.ActionSuccess)
                    _service.FiltraComboSubTipo(oForm, oCbTipoCampo.Selected.Value);
            }
            catch (Exception ex)
            {
                ST_Mensagens.StatusBarError(ex.Message);
            }
        }



        private void ClearDataTables()
        {
            udNomeTabela.Value = "";
            udDescTabela.Value = "";
            udTipoTabela.Value = "";
            udViewM.Value = "N";
            udAuth.Value = "N";
            udCAuto.Value = "N";
        }

        private void ClearDataFields()
        {
            oEtNomeTabela.Item.Click();
            udNomeCampo.Value = "";
            udDescCampo.Value = "";
            udTipoCampo.Value = "";
            udSubTipoCampo.Value = "";
            udTamanhoCampo.Value = "";
            udValorPadraoCampo.Value = "";
            udValorValidoCampo.Value = "N";
            udObrigatorioCampo.Value = "N";

            OcultaValoresValidos(true);
            for (int i = 0; i < oGridValoresValidos.DataTable.Rows.Count; i++)
            {
                oGridValoresValidos.DataTable.Rows.Remove(0);
                i--;
            }

        }

    }
}
