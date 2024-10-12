using System;

namespace ST_Database.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ST_UdoChildAttribute : Attribute
    {
        public string TableName { get; set; }
        public string TableFather { get; set; }

        public ST_UdoChildAttribute(string tableName, string tableFather)
        {
            this.TableName = tableName;
            this.TableFather = tableFather;
        }

    }
}
