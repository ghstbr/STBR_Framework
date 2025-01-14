using STBR_Framework.Attributes;

namespace STBR_Framework.InternalSystem.Models
{
    [ST_Tables("CF", "Config Framework", SAPbobsCOM.BoUTBTableType.bott_NoObject, false)]
    public class ST_CF : ST_TableBase
    {
        [ST_Fields("UserDB", "User name DB", 50, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string UserDB { get; set; }
        [ST_Fields("PasswdDB", "Password DB", 50, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string PasswdDB { get; set; }
        [ST_Fields("Port", "Port", 50, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string Port { get; set; }
        [ST_Fields("LicenseUrl", "License URL", 250, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string LicenseUrl { get; set; }
        [ST_Fields("HardwareKey", "Hardware Key", 250, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string HardwareKey { get; set; }
        [ST_Fields("ClientId", "Client Id", 250, false, SAPbobsCOM.BoFldSubTypes.st_None)]
        public string ClientId { get; set; }
    }
}
