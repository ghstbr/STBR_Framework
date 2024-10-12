using Newtonsoft.Json;
using ST_Database.Models;
using System.Collections.Generic;
using System.IO;

namespace ST_Database
{
    internal sealed class Process
    {
        const string DirectoryFolderTables = "\\Tables";
        const string FilterFiles = "*.json";
        internal static Dictionary<object, TableModel> DictionaryTablesFields = new Dictionary<object, TableModel>();
        internal static Dictionary<object, UdoModel> DictionaryUdos = new Dictionary<object, UdoModel>();
        internal static Dictionary<object, UdoChildsModel> DictionaryUdosChilds = new Dictionary<object, UdoChildsModel>();

        internal static List<TableModel> ReadDataJson()
        {
            List<TableModel> tables = new List<TableModel>();
            //le diretório e pega todos os arquivos .json
            string directory = Directory.GetCurrentDirectory() + DirectoryFolderTables;
            if (Directory.Exists(directory))
                foreach (string arquivo in Directory.GetFiles(directory, FilterFiles))
                {
                    TableModel table = new TableModel();
                    string json = File.ReadAllText(arquivo);
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    table = JsonConvert.DeserializeObject<TableModel>(json, settings);
                    tables.Add(table);
                }

            return tables;
        }
        internal static List<TableModel> ReadDataClass()
        {
            List<TableModel> tables = new List<TableModel>();

            foreach (KeyValuePair<object, TableModel> table in Process.DictionaryTablesFields)
            {
                tables.Add(table.Value);
            }

            return tables;
        }
        internal static void RegisterTable(object obj, TableModel tables)
        {
            if (obj != null)
            {
                DictionaryTablesFields.Add(obj, tables);
            }
        }
        internal static void RegisterUdo(object obj, UdoModel udo)
        {
            if (obj != null)
            {
                DictionaryUdos.Add(obj, udo);
            }
        }

        internal static void RegisterUdoChild(object obj, UdoChildsModel udo)
        {
            if (obj != null)
            {
                DictionaryUdosChilds.Add(obj, udo);
            }
        }

        internal void Update()
        {
            //atualiza tabelas
            TableProcess.Update();

            //atualiza campos
            FieldsProcess.Update();

        }



    }
}
