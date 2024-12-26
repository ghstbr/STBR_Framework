using System;

namespace STBR_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class ST_ValidValuesAttribute : Attribute
    {
        public string Value { get; set; }
        public string Description { get; set; }

        public ST_ValidValuesAttribute(string value, string description)
        {
            this.Value = value;
            this.Description = description;
        }


    }
}
