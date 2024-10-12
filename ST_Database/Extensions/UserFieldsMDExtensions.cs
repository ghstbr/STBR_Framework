using ST_Database.Models;
using System.Collections.Generic;

namespace ST_Database.Extensions
{
    internal static class UserFieldsMDExtensions
    {
        internal static SAPbobsCOM.UserFieldsMD AddValidValues(this SAPbobsCOM.UserFieldsMD oUserFieldsMd, List<ValidValuesModel> values)
        {
            if (values != null)
            {
                int volta = 1;

                foreach (ValidValuesModel value in values)
                {

                    oUserFieldsMd.ValidValues.Value = value.Value;
                    oUserFieldsMd.ValidValues.Description = value.Description;
                    if (volta <= values.Count)
                    {
                        oUserFieldsMd.ValidValues.Add();
                    }
                    volta++;
                }
            }
            return oUserFieldsMd;
        }
    }
}
