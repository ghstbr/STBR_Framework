using SAPbobsCOM;
using System.Collections.Generic;
using STBR_Framework.Enums;

namespace STBR_Framework.Models
{
    public class TableModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BoUTBTableType TableTypeSAP { get; set; }
        public BoYesNoEnum DisplayMenu { get; set; }
        public BoYesNoEnum ApplayAuthorization { get; set; }
        public TableType TableType { get; set; }
        public UdoModel Udos { get; set; }
        public List<FieldModel> Fields { get; set; }
        public string FileName { get; set; }
        public bool AutoCreate { get; set; }

    }
}
