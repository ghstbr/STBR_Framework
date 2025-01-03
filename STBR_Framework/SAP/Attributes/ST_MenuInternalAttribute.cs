using STBR_Framework.Utils;
using System;

namespace STBR_Framework.Attributes
{
    /// <summary>
    /// Atributo para informar quais menus tratar
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class ST_MenuInternalAttribute : Attribute
    {
        public string menuUid;
        public string description;

        public ST_MenuInternalAttribute(string menuUid, string description)
        {
            this.menuUid = ST_Menus.GetMenuID(menuUid);
            this.description = description;
        }
    }
}
