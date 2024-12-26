using System.IO;
using System.Reflection;
using System;

namespace STBR_Framework
{
    public static class StringExtensions
    {
        public static Type[] ST_GetTypesFromAssembly(this string assemblyName)
        {
            return Assembly.Load(assemblyName).GetTypes();
        }

        public static string ST_GetNameTable(this string name)
        {
            InfosAddon InfosAddon = new InfosAddon();

            if (!name.ToUpper().StartsWith(InfosAddon.CustomInfo.Prefix_name))
                name = InfosAddon.CustomInfo.Prefix_name + name;

            if (name.Length > 20)
                name = name.Substring(0, 20);

            return name.ToUpper();

        }

        /// <summary>
        /// Converte o arquivo passado no caminho para string base 64
        /// </summary>
        /// <param name="path">Caminho do arquivo</param>
        /// <returns></returns>
        public static string ST_ConvertObjectToString(this string path)
        {
            FileStream stream = File.OpenRead(path);
            byte[] fileBytes = new byte[stream.Length];
            stream.Read(fileBytes, 0, fileBytes.Length);
            stream.Close();
            return Convert.ToBase64String(fileBytes);

        }

        /// <summary>
        /// Converte a string base 64 para byte[]
        /// </summary>
        /// <param name="fileString">String do arquivo</param>
        /// <returns></returns>
        public static byte[] ST_ConvertStringToByte(this string fileString)
        {
            return Convert.FromBase64String(fileString);
        }

    }
}
