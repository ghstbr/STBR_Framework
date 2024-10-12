using SAPbobsCOM;
using STBR_Framework.Enums;
using System.Collections.Generic;

namespace ST_Database.Models
{
    internal class UdoChildsModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BoUTBTableType TableTypeSAP { get; set; }
        public TableType TableType { get; set; }
        public List<FieldModel> Fields { get; set; }
    }
}
