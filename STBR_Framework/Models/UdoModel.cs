using SAPbobsCOM;
using System.Collections.Generic;

namespace STBR_Framework.Models
{
    public class UdoModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string TableFather { get; set; }
        public string Form { get; set; }
        public BoYesNoEnum Cancel { get; set; }
        public BoYesNoEnum Close { get; set; }
        public BoYesNoEnum CreateDefaultForm { get; set; }
        public BoYesNoEnum Delete { get; set; }
        public BoYesNoEnum Find { get; set; }
        public BoYesNoEnum YearTransfer { get; set; }
        public BoYesNoEnum ManageSeries { get; set; }
        public BoUDOObjType ObjectType { get; set; }
        public BoYesNoEnum EnableEnhancedform { get; set; }
        public BoYesNoEnum RebuildEnhancedForm { get; set; }
        public bool Log { get; set; }
        public List<UdoChildsModel> Children { get; set; }
    }
}
