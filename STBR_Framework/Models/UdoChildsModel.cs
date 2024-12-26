using SAPbobsCOM;
using System.Collections.Generic;
using STBR_Framework.Enums;

namespace STBR_Framework.Models
{
    public class UdoChildsModel
    {
        public string Name { get; set; }
        public string TableFather { get; set; }
        public string Description { get; set; }
        public BoUTBTableType TableTypeSAP { get; set; }
        public TableType TableType { get; set; }
        public List<FieldModel> Fields { get; set; }
    }
}
