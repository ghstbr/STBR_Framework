using System.Collections.Generic;
using System.Reflection;
using STBR_Framework.Attributes;
using STBR_Framework.Enums;
using STBR_Framework.Models;

namespace STBR_Framework
{
    public abstract class ST_TableBase
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }

        private readonly List<string> TableId;

        protected ST_TableBase()
        {

            TableId = new List<string>();
            ST_TablesAttribute attribute = null;
            ST_FieldsAttribute flAttribute = null;
            ST_UdoAttribute udoAttribute = null;
            ST_UdoChildAttribute udoChildAttribute = null;
            ST_ValidValuesAttribute validValues = null;

            foreach (object obj2 in base.GetType().GetCustomAttributes(false))
            {

                #region Atributo Tabelas

                if (obj2 is ST_TablesAttribute)
                {
                    attribute = obj2 as ST_TablesAttribute;

                    if (!string.IsNullOrEmpty(attribute.Name))
                    {
                        TableModel tb = new TableModel();
                        tb.Name = attribute.Name;
                        tb.Description = attribute.Description;
                        tb.TableTypeSAP = attribute.TypeTable;
                        tb.Fields = new List<FieldModel>();
                        tb.TableType = attribute.SystemTable ? TableType.System : TableType.User;

                        foreach (PropertyInfo info in this.GetType().GetProperties())
                        {
                            List<ValidValuesModel> vlrs = new List<ValidValuesModel>();
                            FieldModel cp = new FieldModel();
                            foreach (object field in info.GetCustomAttributes(true))
                            {

                                if (field is ST_FieldsAttribute)
                                {
                                    flAttribute = field as ST_FieldsAttribute;

                                    RelationalReader.verifyTypes(cp, info, flAttribute, tb.Name);

                                }

                                if (field is ST_ValidValuesAttribute)
                                {
                                    validValues = field as ST_ValidValuesAttribute;
                                    vlrs.Add(new ValidValuesModel() { Description = validValues.Description, Value = validValues.Value });
                                }


                            }

                            if (!string.IsNullOrEmpty(cp.Name))
                            {
                                if (vlrs.Count > 0)
                                {
                                    cp.ValidValues = vlrs;
                                }
                                tb.Fields.Add(cp);
                            }



                        }


                        ST_B1AppDomain.RegisterTable(this, tb);
                    }

                }

                #endregion

                #region Atributo Udo

                if (obj2 is ST_UdoAttribute)
                {

                    udoAttribute = obj2 as ST_UdoAttribute;

                    if (!string.IsNullOrEmpty(udoAttribute.Code))
                    {
                        UdoModel ud = new UdoModel();
                        //ud.TableName = udoAttribute.TableName;
                        //ud.Name = udoAttribute.Name;
                        //ud.Code = udoAttribute.Code;
                        //ud.Cancel = udoAttribute.Cancel;
                        //ud.Close = udoAttribute.Close;
                        //ud.CreateDefaultForm = udoAttribute.CreateDefaultForm;
                        //ud.Delete = udoAttribute.Delete;
                        //ud.Find = udoAttribute.Find;
                        //ud.YearTransfer = udoAttribute.YearTransfer;
                        //ud.ManageSeries = udoAttribute.ManageSeries;
                        //ud.ObjectType = udoAttribute.ObjectType;
                        //ud.Form = udoAttribute.Form;
                        //ud.EnableEnhancedform = udoAttribute.EnableEnhancedform;
                        //ud.RebuildEnhancedForm = udoAttribute.RebuildEnhancedForm;
                        //ud.Log = udoAttribute.Log;

                        ST_B1AppDomain.RegisterUdo(this, ud);
                    }

                }

                #endregion

                #region Atributo UdoFilhos

                if (obj2 is ST_UdoChildAttribute)
                {
                    udoChildAttribute = obj2 as ST_UdoChildAttribute;

                    if (!string.IsNullOrEmpty(udoChildAttribute.TableName))
                    {
                        UdoChildsModel udf = new UdoChildsModel();
                        //udf.TableName = udoChildAttribute.TableName;
                        //udf.TableFather = udoChildAttribute.TableFather;

                        ST_B1AppDomain.RegisterUdoChild(this, udf);
                    }

                }

                #endregion


            }
            if (attribute == null)
            {
                ST_B1Exception.writeLog("Falha ao indexar Tabela. Por favor checar os atributos informados");
            }



        }

    }
}
