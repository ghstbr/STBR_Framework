using SAPbobsCOM;
using ST_Database.Models;
using System;
using System.Collections.Generic;

namespace ST_Database.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ST_UdoAttribute : Attribute
    {
        public string TableName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public SAPbobsCOM.BoYesNoEnum Cancel { get; set; }
        public SAPbobsCOM.BoYesNoEnum Close { get; set; }
        public SAPbobsCOM.BoYesNoEnum CreateDefaultForm { get; set; }
        public SAPbobsCOM.BoYesNoEnum Delete { get; set; }
        public SAPbobsCOM.BoYesNoEnum Find { get; set; }
        public SAPbobsCOM.BoYesNoEnum YearTransfer { get; set; }
        public SAPbobsCOM.BoYesNoEnum ManageSeries { get; set; }
        public SAPbobsCOM.BoUDOObjType ObjectType { get; set; }
        public string Form { get; set; }
        public SAPbobsCOM.BoYesNoEnum EnableEnhancedform { get; set; }
        public BoYesNoEnum RebuildEnhancedForm { get; set; }
        public bool Log { get; set; }

        internal List<UdoChildsModel> Children { get; set; }


        public ST_UdoAttribute(string tableName, string name, string code, BoYesNoEnum cancel, BoYesNoEnum close,
            BoYesNoEnum createDefaultForm,
            BoYesNoEnum delete, BoYesNoEnum find, BoYesNoEnum yearTransfer, BoYesNoEnum manageSeries,
            BoUDOObjType objectType, string form,
            BoYesNoEnum enableEnhancedform, BoYesNoEnum rebuildEnhancedForm, bool log = false)
        {


            this.TableName = tableName;
            this.Name = name;
            this.Code = code;
            this.Cancel = cancel;
            this.Close = close;
            this.CreateDefaultForm = createDefaultForm;
            this.Delete = delete;
            this.Find = find;
            this.YearTransfer = yearTransfer;
            this.ManageSeries = manageSeries;
            this.ObjectType = objectType;
            this.Form = form;
            this.EnableEnhancedform = enableEnhancedform;
            this.RebuildEnhancedForm = rebuildEnhancedForm;
            this.Log = log;
        }




    }
}
