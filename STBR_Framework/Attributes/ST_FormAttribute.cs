using System;

namespace STBR_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class ST_FormAttribute : Attribute
    {
        public string formUid;
        public string description;

        public ST_FormAttribute(string formUid, string description)
        {
            this.formUid = formUid;
            this.description = description;
        }
    }
}
