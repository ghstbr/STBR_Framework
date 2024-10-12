using System;
using System.Runtime.InteropServices;

namespace ST_Extensions
{
    public static class ObjectExtensions
    {
        public static void ST_ClearMemory(this object obj)
        {
            Marshal.FinalReleaseComObject(obj);
            obj = null;
            GC.Collect();
        }
    }
}
