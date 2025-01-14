using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SAPbobsCOM;
using System;
using System.Diagnostics;

namespace STBR_Framework.SAP.Utils
{
    public class ST_Reports
    {

        public static void ExportLayout(string reportCode, string pathExport, out string fileName)
        {
            fileName = string.Empty;
            try
            {

                ReportLayoutsService oReportLayoutsService = (ReportLayoutsService)ST_B1AppDomain.Company.GetCompanyService().GetBusinessService(ServiceTypes.ReportLayoutsService);
                ReportParams oReportParams = (ReportParams)oReportLayoutsService.GetDataInterface(ReportLayoutsServiceDataInterfaces.rlsdiReportParams);
                oReportParams.ReportCode = reportCode;

                DefaultReportParams oDefaultReportParams = (DefaultReportParams)oReportLayoutsService.GetDefaultReport(oReportParams);
                fileName = oDefaultReportParams.LayoutCode + ".rpt";
                Debug.WriteLine(oDefaultReportParams.LayoutCode);

                BlobParams oBlobParams = (BlobParams)ST_B1AppDomain.Company.GetCompanyService().GetDataInterface(CompanyServiceDataInterfaces.csdiBlobParams);
                oBlobParams.Table = "RDOC";
                oBlobParams.Field = "Template";
                BlobTableKeySegment oBlobTableKeySegment = oBlobParams.BlobTableKeySegments.Add();
                oBlobTableKeySegment.Name = "DocCode";
                oBlobTableKeySegment.Value = oDefaultReportParams.LayoutCode;
                oBlobParams.FileName = pathExport + "\\" + fileName;
                ST_B1AppDomain.Company.GetCompanyService().SaveBlobToFile(oBlobParams);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                ST_B1Exception.throwException("ExportLayout", ex);
            }


        }

        public static void conexaoCrystal32(ref ReportDocument rptDoc)
        {
            string server = ST_B1AppDomain.Company.Server.ToString().Replace(":30013", ":30015");
            string[] srvfinal = server.Split('@');
            try
            {
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.DatabaseName = ST_B1AppDomain.Company.CompanyDB;
                connectionInfo.UserID = ST_B1AppDomain.UserDB;
                connectionInfo.Password = ST_B1AppDomain.PasswdDB;

                
                connectionInfo.ServerName = srvfinal[1].ToString();

                // Aplique as informações de logon a cada tabela no relatório
                foreach (Table table in rptDoc.Database.Tables)
                {
                    TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                    tableLogOnInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogOnInfo);
                }

                rptDoc.VerifyDatabase();
            }
            catch
            {

                string strConnection = string.Format("DRIVER={0};UID={1};PWD={2};SERVERNODE={3};DATABASE={4};CS={4}",
                    "{HDBODBC32}", ST_B1AppDomain.UserDB, ST_B1AppDomain.PasswdDB, srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB);


                NameValuePairs2 logonProps2 = rptDoc.DataSourceConnections[0].LogonProperties;

                logonProps2.Set("Provider", "{HDBODBC32}");
                logonProps2.Set("Server Type", "{HDBODBC32}");
                logonProps2.Set("Connection String", strConnection);
                logonProps2.Set("Locale Identifier", "1033");

                rptDoc.DataSourceConnections.Clear();
                for (int i = 0; i < rptDoc.DataSourceConnections[0].LogonProperties.Count; i++)
                {
                    rptDoc.DataSourceConnections[0].LogonProperties.RemoveAt(i);
                }

                rptDoc.DataSourceConnections[0].SetLogonProperties(logonProps2);
                rptDoc.DataSourceConnections[0].SetConnection(srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB, false);
                rptDoc.Refresh();
                rptDoc.SetDatabaseLogon(ST_B1AppDomain.UserDB, ST_B1AppDomain.PasswdDB, srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB, false);

                rptDoc.VerifyDatabase();
            }
        }

        public static void conexaoCrystal32(ref ReportDocument rptDoc, string userDB, string passwdDB, string portDB)
        {
            string server = ST_B1AppDomain.Company.Server.ToString().Replace(":30013", ":" + portDB);
            string[] srvfinal = server.Split('@');
            try
            {
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.DatabaseName = ST_B1AppDomain.Company.CompanyDB;
                connectionInfo.UserID = userDB;
                connectionInfo.Password = passwdDB;


                connectionInfo.ServerName = srvfinal[1].ToString();

                // Aplique as informações de logon a cada tabela no relatório
                foreach (Table table in rptDoc.Database.Tables)
                {
                    TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                    tableLogOnInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogOnInfo);
                }

                rptDoc.VerifyDatabase();
            }
            catch
            {

                string strConnection = string.Format("DRIVER={0};UID={1};PWD={2};SERVERNODE={3};DATABASE={4};CS={4}",
                    "{HDBODBC32}", userDB, passwdDB, srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB);


                NameValuePairs2 logonProps2 = rptDoc.DataSourceConnections[0].LogonProperties;

                logonProps2.Set("Provider", "{HDBODBC32}");
                logonProps2.Set("Server Type", "{HDBODBC32}");
                logonProps2.Set("Connection String", strConnection);
                logonProps2.Set("Locale Identifier", "1033");

                rptDoc.DataSourceConnections.Clear();
                for (int i = 0; i < rptDoc.DataSourceConnections[0].LogonProperties.Count; i++)
                {
                    rptDoc.DataSourceConnections[0].LogonProperties.RemoveAt(i);
                }

                rptDoc.DataSourceConnections[0].SetLogonProperties(logonProps2);
                rptDoc.DataSourceConnections[0].SetConnection(srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB, false);
                rptDoc.Refresh();
                rptDoc.SetDatabaseLogon(userDB, passwdDB, srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB, false);

                rptDoc.VerifyDatabase();
            }
        }

        public static void conexaoCrystal64(ref ReportDocument rptDoc)
        {
            string server = ST_B1AppDomain.Company.Server.ToString().Replace(":30013", ":30015");
            string[] srvfinal = server.Split('@');
            try
            {
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.DatabaseName = ST_B1AppDomain.Company.CompanyDB;
                connectionInfo.UserID = ST_B1AppDomain.UserDB;
                connectionInfo.Password = ST_B1AppDomain.PasswdDB;

                
                connectionInfo.ServerName = srvfinal[1].ToString();

                // Aplique as informações de logon a cada tabela no relatório
                foreach (Table table in rptDoc.Database.Tables)
                {
                    TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                    tableLogOnInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogOnInfo);
                }

                rptDoc.VerifyDatabase();
            }
            catch
            {

                string strConnection = string.Format("DRIVER={0};UID={1};PWD={2};SERVERNODE={3};DATABASE={4};CS={4}",
                    "{HDBODBC}", ST_B1AppDomain.UserDB, ST_B1AppDomain.PasswdDB, srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB);


                NameValuePairs2 logonProps2 = rptDoc.DataSourceConnections[0].LogonProperties;

                logonProps2.Set("Provider", "{HDBODBC}");
                logonProps2.Set("Server Type", "{HDBODBC}");
                logonProps2.Set("Connection String", strConnection);
                logonProps2.Set("Locale Identifier", "1033");

                rptDoc.DataSourceConnections.Clear();
                for (int i = 0; i < rptDoc.DataSourceConnections[0].LogonProperties.Count; i++)
                {
                    rptDoc.DataSourceConnections[0].LogonProperties.RemoveAt(i);
                }

                rptDoc.DataSourceConnections[0].SetLogonProperties(logonProps2);
                rptDoc.DataSourceConnections[0].SetConnection(srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB, false);
                rptDoc.Refresh();
                rptDoc.SetDatabaseLogon(ST_B1AppDomain.UserDB, ST_B1AppDomain.PasswdDB, srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB, false);

                rptDoc.VerifyDatabase();
            }
        }

        public static void conexaoCrystal64(ref ReportDocument rptDoc, string userDB, string passwdDB, string portDB)
        {
            string server = ST_B1AppDomain.Company.Server.ToString().Replace(":30013", ":" + portDB);
            string[] srvfinal = server.Split('@');
            try
            {
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.DatabaseName = ST_B1AppDomain.Company.CompanyDB;
                connectionInfo.UserID = userDB;
                connectionInfo.Password = passwdDB;


                connectionInfo.ServerName = srvfinal[1].ToString();

                // Aplique as informações de logon a cada tabela no relatório
                foreach (Table table in rptDoc.Database.Tables)
                {
                    TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                    tableLogOnInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogOnInfo);
                }

                rptDoc.VerifyDatabase();
            }
            catch
            {

                string strConnection = string.Format("DRIVER={0};UID={1};PWD={2};SERVERNODE={3};DATABASE={4};CS={4}",
                    "{HDBODBC}", userDB, passwdDB, srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB);


                NameValuePairs2 logonProps2 = rptDoc.DataSourceConnections[0].LogonProperties;

                logonProps2.Set("Provider", "{HDBODBC}");
                logonProps2.Set("Server Type", "{HDBODBC}");
                logonProps2.Set("Connection String", strConnection);
                logonProps2.Set("Locale Identifier", "1033");

                rptDoc.DataSourceConnections.Clear();
                for (int i = 0; i < rptDoc.DataSourceConnections[0].LogonProperties.Count; i++)
                {
                    rptDoc.DataSourceConnections[0].LogonProperties.RemoveAt(i);
                }

                rptDoc.DataSourceConnections[0].SetLogonProperties(logonProps2);
                rptDoc.DataSourceConnections[0].SetConnection(srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB, false);
                rptDoc.Refresh();
                rptDoc.SetDatabaseLogon(userDB, passwdDB, srvfinal[1].ToString(), ST_B1AppDomain.Company.CompanyDB, false);

                rptDoc.VerifyDatabase();
            }
        }

        public static void ExportToPdf(ReportDocument rptDoc, string path, string fileName)
        {
            try
            {
                string fileNamePdf = path + "\\" + fileName + ".pdf";
                // Configura as opções de exportação
                CrystalDecisions.Shared.ExportOptions exportOptions = new CrystalDecisions.Shared.ExportOptions();
                DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions formatTypeOptions = new PdfRtfWordFormatOptions();
                diskFileDestinationOptions.DiskFileName = fileNamePdf;
                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                exportOptions.DestinationOptions = diskFileDestinationOptions;
                exportOptions.FormatOptions = formatTypeOptions;
                //rptDoc.Export(exportOptions);
                rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, fileNamePdf);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                ST_B1Exception.throwException("ExportToPdf :: ", ex);
            }
        }
    }
}
