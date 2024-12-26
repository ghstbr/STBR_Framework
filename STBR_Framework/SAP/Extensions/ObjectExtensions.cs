using System;
using System.Runtime.InteropServices;

namespace STBR_Framework
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
