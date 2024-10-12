using System;

namespace ST_Extensions
{
    public static class ComboBoxExtensions
    {
        public static bool ST_ClearData(this SAPbouiCOM.ComboBox oCombo)
        {
            try
            {
                if (oCombo.ValidValues.Count > 0)
                {

                    while (oCombo.ValidValues.Count > 0)
                    {

                        oCombo.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);

                    }
                }

            }
            catch (Exception ex) { throw ex; }

            return true;
        }
    }
}
