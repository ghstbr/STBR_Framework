using SAPbouiCOM;
using System.Collections.Generic;
using System.Linq;
using STBR_Framework.Attributes;

namespace STBR_Framework
{
    public abstract class ST_FormBase
    {
        private readonly List<string> formUid;
        public Form oForm;
        public Dictionary<string, object> formParameters = new Dictionary<string, object>();


        protected Item GetItem(string uid)
        {
            //CapturaFormulario();
            return this.oForm.Items.Item(uid);
        }

        internal void CapturaFormulario()
        {
            try
            {
                oForm = ST_B1AppDomain.Application.Forms.Item(this.formUid);
            }
            catch
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
            }
        }


        protected ST_FormBase()
        {
            formUid = new List<string>();

            ST_FormAttribute attribute = null;
            int index = 0;
            foreach (object obj2 in base.GetType().GetCustomAttributes(false))
            {
                if (obj2 is ST_FormAttribute)
                {
                    attribute = obj2 as ST_FormAttribute;

                    if (!string.IsNullOrEmpty(attribute.formUid))
                    {
                        string form = attribute.formUid;
                        this.formUid.Add(form.Substring(0, 1) == "@" ? form.Replace("@", "UDO_FT_") : form);

                    }

                    string formulario = this.formUid.SingleOrDefault(e => e == attribute.formUid);

                    if (!string.IsNullOrEmpty(formulario))
                    {
                        ST_B1AppDomain.RegisterFormByType(formulario, this);

                    }



                    index++;

                }
            }
            if (attribute == null)
            {
                ST_B1Exception.writeLog("Falha ao indexar Form. Por favor checar os atributos informados");
            }


            this.OnInitializeFormEvents();


        }

        public virtual void OnCustomInitialize() { }


        private void OnInitializeFormEvents()
        {

            #region Item Listeners
            GlobalEvents.ItemPressed_Before += new GlobalEvents.ItemEventHandler(ItemPressedFilterBefore);
            GlobalEvents.ItemPressed_After += new GlobalEvents.ItemEventHandler(ItemPressedFilterAfter);

            GlobalEvents.ItemClicked_Before += new GlobalEvents.ItemEventHandler(ItemClickedFilterBefore);
            GlobalEvents.ItemClicked_After += new GlobalEvents.ItemEventHandler(ItemClickedFilterAfter);
            GlobalEvents.ChooseFromList_Before += new GlobalEvents.ItemEventHandler(ChooseFromListFilterBefore);
            GlobalEvents.ChooseFromList_After += new GlobalEvents.ItemEventHandler(ChooseFromListFilterAfter);
            GlobalEvents.ComboSelect_Before += new GlobalEvents.ItemEventHandler(ComboSelectFilterBefore);
            GlobalEvents.ComboSelect_After += new GlobalEvents.ItemEventHandler(ComboSelectFilterAfter);

            GlobalEvents.Form_Unload_Before += new GlobalEvents.ItemEventHandler(FormUnloadFilterBefore);
            GlobalEvents.Form_Unload_After += new GlobalEvents.ItemEventHandler(FormUnloadFilterAfter);

            GlobalEvents.DoubleClick_Before += new GlobalEvents.ItemEventHandler(DoubleClickFilterBefore);
            GlobalEvents.DoubleClick_After += new GlobalEvents.ItemEventHandler(DoubleClickFilterAfter);
            GlobalEvents.FormActivate_Before += new GlobalEvents.ItemEventHandler(FormActivateFilterBefore);
            GlobalEvents.FormActivate_After += new GlobalEvents.ItemEventHandler(FormActivateFilterAfter);
            GlobalEvents.FormClose_Before += new GlobalEvents.ItemEventHandler(FormCloseFilterBefore);
            GlobalEvents.FormClose_After += new GlobalEvents.ItemEventHandler(FormCloseFilterAfter);
            GlobalEvents.FormKeyDown_Before += new GlobalEvents.ItemEventHandler(FormKeyDownFilterBefore);
            GlobalEvents.FormKeyDown_After += new GlobalEvents.ItemEventHandler(FormKeyDownFilterAfter);

            GlobalEvents.KeyDown_Before += new GlobalEvents.ItemEventHandler(KeyDownFilterBefore);
            GlobalEvents.KeyDown_After += new GlobalEvents.ItemEventHandler(KeyDownFilterAfter);

            GlobalEvents.FormDeactivate_Before += new GlobalEvents.ItemEventHandler(FormDeactivateFilterBefore);
            GlobalEvents.FormDeactivate_After += new GlobalEvents.ItemEventHandler(FormDeactivateFilterAfter);
            GlobalEvents.GotFocus_Before += new GlobalEvents.ItemEventHandler(GotFocusFilterBefore);
            GlobalEvents.GotFocus_After += new GlobalEvents.ItemEventHandler(GotFocusFilterAfter);
            GlobalEvents.LostFocus_Before += new GlobalEvents.ItemEventHandler(LostFocusFilterBefore);
            GlobalEvents.LostFocus_After += new GlobalEvents.ItemEventHandler(LostFocusFilterAfter);
            GlobalEvents.Form_Load_Before += new GlobalEvents.ItemEventHandler(FormLoadFilterBefore);
            GlobalEvents.Form_Load_After += new GlobalEvents.ItemEventHandler(FormLoadFilterAfter);
            GlobalEvents.Matrix_Link_Pressed_Before += new GlobalEvents.ItemEventHandler(MatrixLinkPressedFilterBefore);
            GlobalEvents.Matrix_Link_Pressed_After += new GlobalEvents.ItemEventHandler(MatrixLinkPressedFilterAfter);
            GlobalEvents.Form_Resize_Before += new GlobalEvents.ItemEventHandler(FormResizeFilterBefore);
            GlobalEvents.Form_Resize_After += new GlobalEvents.ItemEventHandler(FormResizeFilterAfter);

            #endregion

            #region RightClick Listeners
            GlobalEvents.RightClick_Before += new GlobalEvents.RightClickEventHandler(RightClickFilterBefore);
            GlobalEvents.RightClick_After += new GlobalEvents.RightClickEventHandler(RightClickFilterAfter);
            #endregion

            #region FormData Listeners
            GlobalEvents.FormDataAdd_Before += new GlobalEvents.FormDataEventHandler(FormDataAddFilterBefore);
            GlobalEvents.FormDataAdd_After += new GlobalEvents.FormDataEventHandler(FormDataAddFilterAfter);
            GlobalEvents.FormDataDelete_Before += new GlobalEvents.FormDataEventHandler(FormDataDeleteFilterBefore);
            GlobalEvents.FormDataDelete_After += new GlobalEvents.FormDataEventHandler(FormDataDeleteFilterAfter);
            GlobalEvents.FormDataLoad_Before += new GlobalEvents.FormDataEventHandler(FormDataLoadFilterBefore);
            GlobalEvents.FormDataLoad_After += new GlobalEvents.FormDataEventHandler(FormDataLoadFilterAfter);
            GlobalEvents.FormDataUpdate_Before += new GlobalEvents.FormDataEventHandler(FormDataUpdateFilterBefore);
            GlobalEvents.FormDataUpdate_After += new GlobalEvents.FormDataEventHandler(FormDataUpdateFilterAfter);
            #endregion

            #region ProgressBar Listeners
            GlobalEvents.ProgressBar_Created_After += new GlobalEvents.ProgressBarEventHandler(ProgressBar_Created_After);
            GlobalEvents.ProgressBar_Created_Before += new GlobalEvents.ProgressBarEventHandler(ProgressBar_Created_Before);
            GlobalEvents.ProgressBar_Released_After += new GlobalEvents.ProgressBarEventHandler(ProgressBar_Released_After);
            GlobalEvents.ProgressBar_Released_Before += new GlobalEvents.ProgressBarEventHandler(ProgressBar_Released_Before);
            GlobalEvents.ProgressBar_Stopped_After += new GlobalEvents.ProgressBarEventHandler(ProgressBar_Stopped_After);
            GlobalEvents.ProgressBar_Stopped_Before += new GlobalEvents.ProgressBarEventHandler(ProgressBar_Stopped_Before);
            #endregion

            #region UDO Listeners
            GlobalEvents.UdoFormOpen += new GlobalEvents.UdoEventHandler(UdoForm_Open);
            GlobalEvents.UdoFormBuild += new GlobalEvents.UdoEventHandler(UdoForm_Build);
            #endregion

            #region App Listeners
            GlobalEvents.AppCompanyChanged += new GlobalEvents.AppEventHandler(AppCompanyChanged);
            GlobalEvents.AppLanguageChanged += new GlobalEvents.AppEventHandler(AppLanguageChanged);
            GlobalEvents.AppFontChanged += new GlobalEvents.AppEventHandler(AppFontChanged);
            GlobalEvents.AppServerTerminition += new GlobalEvents.AppEventHandler(AppServerTerminition);
            GlobalEvents.AppShutdown += new GlobalEvents.AppEventHandler(AppShutdown);
            #endregion

            #region Print Listeners

            GlobalEvents.Print_Before += new GlobalEvents.PrintEventHandler(PrintFilterBefore);
            GlobalEvents.Print_After += new GlobalEvents.PrintEventHandler(PrintFilterAfter);
            GlobalEvents.Print_Data_Before += new GlobalEvents.ReportDataEventHandler(PrintDataFilterBefore);
            GlobalEvents.Print_Data_After += new GlobalEvents.ReportDataEventHandler(PrintDataFilterAfter);
            GlobalEvents.Print_Layout_Before += new GlobalEvents.PrintEventHandler(PrintLayoutFilterBefore);
            GlobalEvents.Print_Layout_After += new GlobalEvents.PrintEventHandler(PrintLayoutFilterAfter);

            #endregion


        }



        #region ProgressBar Actions
        public virtual void ProgressBar_Stopped_Before(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }
        public virtual void ProgressBar_Stopped_After(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }

        public virtual void ProgressBar_Released_Before(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }
        public virtual void ProgressBar_Released_After(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }

        public virtual void ProgressBar_Created_Before(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }
        public virtual void ProgressBar_Created_After(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }
        #endregion

        #region Item Actions


        private void FormUnloadFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                Form_Unload_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormUnloadFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                Form_Unload_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void Form_Unload_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void Form_Unload_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void ItemPressedFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                ItemPressed_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void ItemPressedFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                ItemPressed_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void ItemPressed_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void ItemPressed_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void ItemClickedFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                ItemClicked_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void ItemClickedFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                ItemClicked_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void ItemClicked_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void ItemClicked_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void ChooseFromListFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                ChooseFromLiVZ_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void ChooseFromListFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                ChooseFromLiVZ_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void ChooseFromLiVZ_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void ChooseFromLiVZ_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void ComboSelectFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                ComboSelect_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void ComboSelectFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                ComboSelect_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void ComboSelect_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void ComboSelect_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void DoubleClickFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                DoubleClick_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void DoubleClickFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                DoubleClick_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void DoubleClick_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void DoubleClick_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormActivateFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormActivate_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormActivateFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormActivate_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void FormActivate_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void FormActivate_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormCloseFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormClose_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormCloseFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormClose_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void FormClose_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void FormClose_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormKeyDownFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormKeyDown_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormKeyDownFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormKeyDown_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void FormKeyDown_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void FormKeyDown_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }


        private void KeyDownFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                KeyDown_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void KeyDownFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                KeyDown_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void KeyDown_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void KeyDown_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }


        private void FormDeactivateFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormDeactivate_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormDeactivateFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormDeactivate_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void FormDeactivate_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void FormDeactivate_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void GotFocusFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                if (ST_B1AppDomain.Application.Forms.Equals(formUID))
                    oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                GotFocus_After(formUID, ref pVal, ref bubbleEvent);

            }
        }
        private void GotFocusFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                if (ST_B1AppDomain.Application.Forms.Equals(formUID))
                    oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                GotFocus_Before(formUID, ref pVal, ref bubbleEvent);

            }
        }
        public virtual void GotFocus_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void GotFocus_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void LostFocusFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {

            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                if (ST_B1AppDomain.Application.Forms.Equals(formUID))
                    oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                LostFocus_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void LostFocusFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {

            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                if (ST_B1AppDomain.Application.Forms.Equals(formUID))
                    oForm = ST_B1AppDomain.Application.Forms.Item(formUID);
                LostFocus_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void LostFocus_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void LostFocus_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormLoadFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                //oForm = VZ_B1AppDomain.Application.Forms.Item(formUID);
                Form_Load_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormLoadFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                //oForm = VZ_B1AppDomain.Application.Forms.Item(formUID);
                Form_Load_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void Form_Load_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void Form_Load_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void MatrixLinkPressedFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                Matrix_Link_Pressed_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void MatrixLinkPressedFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                Matrix_Link_Pressed_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void Matrix_Link_Pressed_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void Matrix_Link_Pressed_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormResizeFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                Form_Resize_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormResizeFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                Form_Resize_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void Form_Resize_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void Form_Resize_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        #endregion

        #region App Actions
        public virtual void AppCompanyChanged(ref BoAppEventTypes pVal) { }
        public virtual void AppLanguageChanged(ref BoAppEventTypes pVal) { }
        public virtual void AppFontChanged(ref BoAppEventTypes pVal) { }
        public virtual void AppServerTerminition(ref BoAppEventTypes pVal) { }
        public virtual void AppShutdown(ref BoAppEventTypes pVal) { }
        #endregion

        #region UDO Actions
        private void UdoForm_Build(ref UDOEvent pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.UDOCode))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                UdoFormBuild(ref pVal, ref bubbleevent);
            }
        }
        public virtual void UdoFormBuild(ref UDOEvent pVal, ref bool bubbleevent) { }
        private void UdoForm_Open(ref UDOEvent pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.UDOCode))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                UdoFormOpen(ref pVal, ref bubbleevent);
            }
        }
        public virtual void UdoFormOpen(ref UDOEvent pVal, ref bool bubbleevent) { }
        #endregion

        #region FormData Actions

        private void FormDataAddFilterBefore(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                FormDataAdd_Before(ref pVal, ref bubbleevent);
            }
        }
        private void FormDataAddFilterAfter(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                FormDataAdd_After(ref pVal, ref bubbleevent);
            }
        }
        public virtual void FormDataAdd_Before(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        public virtual void FormDataAdd_After(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }


        private void FormDataDeleteFilterBefore(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                FormDataDelete_Before(ref pVal, ref bubbleevent);
            }
        }
        private void FormDataDeleteFilterAfter(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                FormDataDelete_After(ref pVal, ref bubbleevent);
            }
        }
        public virtual void FormDataDelete_Before(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        public virtual void FormDataDelete_After(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }


        private void FormDataUpdateFilterBefore(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                FormDataUpdate_Before(ref pVal, ref bubbleevent);
            }
        }
        private void FormDataUpdateFilterAfter(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                FormDataUpdate_After(ref pVal, ref bubbleevent);
            }
        }
        public virtual void FormDataUpdate_Before(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        public virtual void FormDataUpdate_After(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }



        private void FormDataLoadFilterBefore(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                FormDataLoad_Before(ref pVal, ref bubbleevent);
            }
        }
        private void FormDataLoadFilterAfter(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = ST_B1AppDomain.Application.Forms.ActiveForm;
                FormDataLoad_After(ref pVal, ref bubbleevent);
            }
        }
        public virtual void FormDataLoad_Before(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        public virtual void FormDataLoad_After(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        #endregion

        #region RightClick Actions
        public virtual void RightClickFilterAfter(ref ContextMenuInfo pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormUID))
            {
                RightClick_After(ref pVal, ref bubbleEvent);
            }
        }
        public virtual void RightClickFilterBefore(ref ContextMenuInfo pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormUID))
            {
                RightClick_Before(ref pVal, ref bubbleEvent);
            }
        }
        public virtual void RightClick_After(ref ContextMenuInfo pVal, ref bool bubbleEvent) { }
        public virtual void RightClick_Before(ref ContextMenuInfo pVal, ref bool bubbleEvent) { }
        #endregion

        #region Print Actions

        private void PrintFilterBefore(ref PrintEventInfo pVal, ref bool bubbleEvent)
        {

            Print_Before(ref pVal, ref bubbleEvent);

        }

        private void PrintFilterAfter(ref PrintEventInfo pVal, ref bool bubbleEvent)
        {

            Print_After(ref pVal, ref bubbleEvent);

        }

        public virtual void Print_Before(ref PrintEventInfo pVal, ref bool bubbleEvent) { }
        public virtual void Print_After(ref PrintEventInfo pVal, ref bool bubbleEvent) { }


        private void PrintDataFilterBefore(ref ReportDataInfo pVal, ref bool bubbleEvent)
        {

            Print_Data_Before(ref pVal, ref bubbleEvent);

        }

        private void PrintDataFilterAfter(ref ReportDataInfo pVal, ref bool bubbleEvent)
        {

            Print_Data_After(ref pVal, ref bubbleEvent);

        }

        public virtual void Print_Data_Before(ref ReportDataInfo pVal, ref bool bubbleEvent) { }
        public virtual void Print_Data_After(ref ReportDataInfo pVal, ref bool bubbleEvent) { }


        private void PrintLayoutFilterBefore(ref PrintEventInfo pVal, ref bool bubbleEvent)
        {

            Print_Layout_Before(ref pVal, ref bubbleEvent);

        }

        private void PrintLayoutFilterAfter(ref PrintEventInfo pVal, ref bool bubbleEvent)
        {

            Print_Layout_After(ref pVal, ref bubbleEvent);

        }

        public virtual void Print_Layout_Before(ref PrintEventInfo pVal, ref bool bubbleEvent) { }
        public virtual void Print_Layout_After(ref PrintEventInfo pVal, ref bool bubbleEvent) { }

        #endregion
    }
}
