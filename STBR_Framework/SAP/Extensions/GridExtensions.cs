using System;
using STBR_Framework.Utils;

namespace STBR_Framework
{
    public static class GridExtensions
    {

        public static string ST_GetValueSelected(this SAPbouiCOM.Grid grid, string column)
        {
            try
            {
                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    if (grid.Rows.IsSelected(i))
                        return grid.DataTable.GetValue(column, i).ToString();

                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Error VZ_GetValueSelected :: ", ex);
                ST_Mensagens.StatusBarError(ex.Message);
                return string.Empty;
            }

        }


    }
}
