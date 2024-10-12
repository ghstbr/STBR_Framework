using SAPbobsCOM;
using System;

namespace ST_Database.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ST_TablesAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BoUTBTableType TypeTable { get; set; }
        public bool SystemTable { get; set; }


        public ST_TablesAttribute(string name, string description, BoUTBTableType typeTable, bool systemTable)
        {
            this.TypeTable = typeTable;
            this.Name = name;
            this.Description = description;
            this.SystemTable = systemTable;
        }

        public ST_TablesAttribute(string name, string description, bool systemTable)
        {
            this.Name = name;
            this.Description = description;
            this.SystemTable = systemTable;
        }
    }
}
