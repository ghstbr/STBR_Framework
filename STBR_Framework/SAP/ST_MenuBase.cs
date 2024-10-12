using SAPbouiCOM;
using STBR_Framework.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace STBR_Framework
{
    /// <summary>
    /// Base para criacao de Menus. Deve ser herdada em outra classe
    /// </summary>
    public abstract class ST_MenuBase
    {
        internal readonly List<string> menuUid;


        protected ST_MenuBase()
        {
            this.menuUid = new List<string>();
            ST_MenuAttribute attribute = null;
            int index = 0;
            foreach (object obj2 in base.GetType().GetCustomAttributes(false))
            {
                if (obj2 is ST_MenuAttribute)
                {
                    attribute = obj2 as ST_MenuAttribute;

                    this.menuUid.Add(attribute.menuUid);

                    ST_B1AppDomain.RegisterMenuByType(this.menuUid.SingleOrDefault(e => e == attribute.menuUid), this);

                    index++;
                }
            }



            if (attribute == null)
            {
                ST_B1Exception.writeLog("Falha ao indexar Form. Por favor checar os atributos informados");
            }


            this.OnInitializeFormEvents();


        }

        private void OnInitializeFormEvents()
        {

            #region Menu Listeners
            GlobalEvents.MenuClick_Before += new GlobalEvents.MenuEventHandler(MenuClickFilterBefore);
            GlobalEvents.MenuClick_After += new GlobalEvents.MenuEventHandler(MenuClickFilterAfter);
            #endregion

        }


        #region MenuClick Actions

        private void MenuClickFilterAfter(ref MenuEvent pVal, ref bool bubbleEvent)
        {
            if (this.menuUid.Contains(pVal.MenuUID))
            {
                MenuClick_After(ref pVal, ref bubbleEvent);

            }
        }
        private void MenuClickFilterBefore(ref MenuEvent pVal, ref bool bubbleEvent)
        {
            if (this.menuUid.Contains(pVal.MenuUID))
            {
                MenuClick_Before(ref pVal, ref bubbleEvent);
            }
        }
        public virtual void MenuClick_After(ref MenuEvent pVal, ref bool bubbleEvent) { }
        public virtual void MenuClick_Before(ref MenuEvent pVal, ref bool bubbleEvent) { }

        #endregion
    }
}
