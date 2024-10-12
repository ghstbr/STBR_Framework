using ST_Database.Attributes;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ST_Database
{
    public class DB
    {
        private static void CreateInstanceClass(Type[] nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            foreach (Type type in nameSpace)
            {
                if (type.Name != "Program" && type.CustomAttributes.Count() > 0)
                {
                    try
                    {
                        foreach (object obj2 in type.GetCustomAttributes(false))
                        {
                            if (obj2 is ST_TablesAttribute)
                            {
                                Activator.CreateInstance(type);
                            }
                        }

                    }
                    catch { }

                }

            }

        }
        public DB(Type[] types = null)
        {
            CreateInstanceClass(types);
            Process process = new Process();
            process.Update();
        }
    }
}
