using Newtonsoft.Json;
using STBR_Framework.Models;
using System.Collections.Generic;
using System.IO;

namespace STBR_Framework.SAP.Services
{
    internal class InfosAddon_service
    {
        InfoModel _info = new InfoModel();

        internal InfosAddon_service()
        {
            _info = ReadInfoJson();
        }

        internal InfoModel ReadInfoJson()
        {

            InfoModel infos = new InfoModel();
            string directory = Directory.GetCurrentDirectory() + "\\Infos";
            if (Directory.Exists(directory))
                foreach (string arquivo in Directory.GetFiles(directory, "infos.json"))
                {

                    string json = File.ReadAllText(arquivo);
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    infos = JsonConvert.DeserializeObject<InfoModel>(json, settings);

                }
            else
            {
                Directory.CreateDirectory(directory);
                infos.DirectoryTables = "\\\\Tables";
                infos.FilterFiles = "*.json";
                infos.Prefix_name = "ST_";
                infos.Prefix_description = "ST-";
                //criar arquivo
                string json = JsonConvert.SerializeObject(infos, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(directory + "\\infos.json", json);

            }



            return infos;
        }

        internal void SaveInfoJson(InfoModel infos)
        {
            string directory = Directory.GetCurrentDirectory() + "\\Infos";
            if (Directory.Exists(directory))
            {
                string json = JsonConvert.SerializeObject(infos, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(directory + "\\infos.json", json);
            }
        }


        internal List<TableModel> ReadDataJson()
        {
            List<TableModel> tables = new List<TableModel>();
            //le diretório e pega todos os arquivos .json
            string directory = Directory.GetCurrentDirectory() + _info.DirectoryTables;
            if (Directory.Exists(directory))
                foreach (string arquivo in Directory.GetFiles(directory, _info.FilterFiles))
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


        internal List<TableModel> ReadDataClass()
        {
            List<TableModel> tables = new List<TableModel>();

            foreach (KeyValuePair<object, TableModel> table in ST_B1AppDomain.DictionaryTablesFields)
            {
                tables.Add(table.Value);
            }

            return tables;
        }




    }
}
