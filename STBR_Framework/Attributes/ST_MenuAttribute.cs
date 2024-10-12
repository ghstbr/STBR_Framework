using System;

namespace STBR_Framework.Attributes
{
    /// <summary>
    /// Atributo para informar quais menus tratar
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class ST_MenuAttribute : Attribute
    {
        public string menuUid;
        public string description;

        public ST_MenuAttribute(string menuUid, string description)
        {
            this.menuUid = menuUid;
            this.description = description;
        }
    }
}
