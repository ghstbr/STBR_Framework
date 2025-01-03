using SAPbouiCOM;
using STBR_Framework.SAP.Services;
using STBR_Framework.Utils;
using System;

namespace STBR_Framework.SAP.Database
{
    internal class DB
    {
        public DB()
        {
            //processa tabelas json
            Tables_service tablesService = new Tables_service();
            tablesService.ProcessTableJson();
            tablesService.ProcessTableClass();

            //processa campos json
            Fields_service fieldsService = new Fields_service();
            fieldsService.ProcessFieldsJson();
            fieldsService.ProcessFieldsClass();
        }

        public DB(ref UserDataSource log)
        {
            ST_Mensagens.StatusBar("Iniciando atualização de tabelas e campos... :: " + DateTime.Now.ToString("HH:mm:ss"));
            //processa tabelas json
            Tables_service tablesService = new Tables_service();
            log.Value += "\r\nIniciando atualização tabelas json...";
            tablesService.ProcessTableJson(true);
            log.Value += "\r\nTabelas json atualizadas...";
            log.Value += "\r\nIniciando atualização tablas classes...";
            tablesService.ProcessTableClass();
            log.Value += "\r\nTabelas classes atualizadas...";

            //processa campos json
            Fields_service fieldsService = new Fields_service();
            log.Value += "\r\nIniciando atualização campos json... ";
            fieldsService.ProcessFieldsJson(true);
            log.Value += "\r\nCampos json atualizados... ";
            log.Value += "\r\nIniciando atualização campos classes... ";
            fieldsService.ProcessFieldsClass();
            log.Value += "\r\nCampos classes atualizados... ";


            //processa udos json
            Udos_service udosService = new Udos_service();
            log.Value += "\r\nIniciando atualização udos json... ";
            udosService.ProcessUdoJson();
            log.Value += "\r\nUdos json atualizados... ";
            log.Value += "\r\nIniciando atualização udos classes... ";
            udosService.ProcessUdoClasses();
            log.Value += "\r\nUdos classes atualizados... ";

        }
    }
}
