using System.Collections.Generic;

namespace ST_Database.Models
{
    internal class UdoModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string TableFather { get; set; }
        public string Form { get; set; }
        public string Cancel { get; set; }
        public string Close { get; set; }
        public string CreateDefaultForm { get; set; }
        public string Delete { get; set; }
        public string Find { get; set; }
        public string YearTransfer { get; set; }
        public string ManageSeries { get; set; }
        public string ObjectType { get; set; }
        public string EnableEnhancedform { get; set; }
        public string RebuildEnhancedForm { get; set; }
        public string Log { get; set; }
        public List<UdoChildsModel> Children { get; set; }
    }
}
