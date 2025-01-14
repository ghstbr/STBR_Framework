
using STBR_Framework.Attributes;

namespace STBR_Framework.InternalSystem.Models
{
    [ST_Tables("Products", "Products", SAPbobsCOM.BoUTBTableType.bott_NoObject, false)]
    public class ST_Products : ST_TableBase
    {
        public string Code { get; set; }
        public string Name { get; set; }

        [ST_Fields("Description", "Description", 250, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string Description { get; set; }
        [ST_Fields("Key", "Key", 255, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string Key { get; set; }
        [ST_Fields("Active", "Active", 1, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        [ST_ValidValues("Y", "Yes")]
        [ST_ValidValues("N", "No")]
        public string Active { get; set; }
        [ST_Fields("Version", "Version", 50, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string Version { get; set; }

        [ST_Fields("Msg", "Msg", 250, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string Msg { get; set; }



    }
}
