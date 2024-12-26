using SAPbobsCOM;
using STBR_Framework.Attributes;
using STBR_Framework.Models;
using System.Reflection;

namespace STBR_Framework
{
    internal static class RelationalReader
    {

        private static string msgError = "Subtipo incorreto para {0} -- {1} -- {2}";


        //Configura tipo SAP baseado no tipo modelo.
        internal static void verifyTypes(FieldModel field, PropertyInfo info, ST_FieldsAttribute attribute, string tableName)
        {
            #region Dados dos Atributos

            field.Description = attribute.Description;
            field.Mandatory = attribute.Mandatory ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
            field.Name = attribute.Name;
            //field.TableName = tableName;
            field.UdoReference = attribute.LinkUdo;


            #endregion


            if (info.PropertyType == typeof(System.String))
            {
                switch (attribute.SubType)
                {

                    case BoFldSubTypes.st_None:

                        if (attribute.Size > 254)
                        {
                            field.Type = BoFieldTypes.db_Memo;
                            field.SubType = BoFldSubTypes.st_None;
                        }
                        else
                        {
                            field.Type = BoFieldTypes.db_Alpha;
                            field.SubType = BoFldSubTypes.st_None;
                            field.Size = attribute.Size;
                        }

                        break;

                    case BoFldSubTypes.st_Address:
                        field.Type = BoFieldTypes.db_Alpha;
                        field.SubType = BoFldSubTypes.st_Address;
                        break;

                    case BoFldSubTypes.st_Phone:
                        field.Type = BoFieldTypes.db_Alpha;
                        field.SubType = BoFldSubTypes.st_Phone;
                        break;

                    case BoFldSubTypes.st_Image:
                        field.Type = BoFieldTypes.db_Alpha;
                        field.SubType = BoFldSubTypes.st_Image;
                        break;

                    default:
                        ST_B1Exception.writeLog(string.Format(msgError, info.PropertyType.ToString(), attribute.Name, attribute.TypeId));
                        break;

                }



            }
            else if (info.PropertyType == typeof(System.Int32) || info.PropertyType == typeof(System.Int16) || info.PropertyType == typeof(System.Int64))
            {
                switch (attribute.SubType)
                {
                    case BoFldSubTypes.st_None:
                        field.Type = BoFieldTypes.db_Numeric;
                        field.SubType = BoFldSubTypes.st_None;
                        field.Size = attribute.Size;
                        break;

                    case BoFldSubTypes.st_Time:
                        field.Type = BoFieldTypes.db_Date;
                        field.SubType = BoFldSubTypes.st_Time;
                        break;

                    default:
                        ST_B1Exception.writeLog(string.Format(msgError, info.PropertyType.ToString(), attribute.Name, attribute.TypeId));
                        break;
                }


            }
            else if (info.PropertyType == typeof(System.DateTime))
            {

                field.Type = BoFieldTypes.db_Date;
                field.SubType = BoFldSubTypes.st_None;

            }
            else if (info.PropertyType == typeof(System.Double))
            {

                switch (attribute.SubType)
                {
                    case BoFldSubTypes.st_None:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Sum;
                        break;

                    case BoFldSubTypes.st_Percentage:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Percentage;
                        break;

                    case BoFldSubTypes.st_Measurement:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Measurement;
                        break;

                    case BoFldSubTypes.st_Price:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Price;
                        break;

                    case BoFldSubTypes.st_Quantity:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Quantity;
                        break;

                    case BoFldSubTypes.st_Rate:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Rate;
                        break;

                    case BoFldSubTypes.st_Sum:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Sum;
                        break;

                    default:
                        ST_B1Exception.writeLog(string.Format(msgError, info.PropertyType.ToString(), attribute.Name, attribute.TypeId));
                        break;
                }

            }
            else if (info.PropertyType == typeof(System.Decimal))
            {

                switch (attribute.SubType)
                {
                    case BoFldSubTypes.st_None:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Sum;
                        break;

                    case BoFldSubTypes.st_Percentage:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Percentage;
                        break;

                    case BoFldSubTypes.st_Measurement:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Measurement;
                        break;

                    case BoFldSubTypes.st_Price:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Price;
                        break;

                    case BoFldSubTypes.st_Quantity:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Quantity;
                        break;

                    case BoFldSubTypes.st_Rate:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Rate;
                        break;

                    case BoFldSubTypes.st_Sum:
                        field.Type = BoFieldTypes.db_Float;
                        field.SubType = BoFldSubTypes.st_Sum;
                        break;

                    default:
                        ST_B1Exception.writeLog(string.Format(msgError, info.PropertyType.ToString(), attribute.Name, attribute.TypeId));
                        break;
                }

            }






        }


        internal static BoFldSubTypes GetSubType(string tipo)
        {

            switch (tipo)
            {
                case "":
                    return BoFldSubTypes.st_None;

                case "?":
                    return BoFldSubTypes.st_Address;

                case "#":
                    return BoFldSubTypes.st_Phone;

                case "T":
                    return BoFldSubTypes.st_Time;

                case "R":
                    return BoFldSubTypes.st_Rate;

                case "S":
                    return BoFldSubTypes.st_Sum;

                case "P":
                    return BoFldSubTypes.st_Price;

                case "Q":
                    return BoFldSubTypes.st_Quantity;

                case "%":
                    return BoFldSubTypes.st_Percentage;

                case "M":
                    return BoFldSubTypes.st_Measurement;

                case "B":
                    return BoFldSubTypes.st_Link;

                case "I":
                    return BoFldSubTypes.st_Image;

                default:
                    return BoFldSubTypes.st_None;
            }

        }

        internal static BoFieldTypes GetFieldType(string tipo)
        {

            switch (tipo)
            {
                case "M":
                    return BoFieldTypes.db_Memo;

                case "A":
                    return BoFieldTypes.db_Alpha;

                case "D":
                    return BoFieldTypes.db_Date;

                case "N":
                    return BoFieldTypes.db_Numeric;

                case "B":
                    return BoFieldTypes.db_Float;

                default:
                    return BoFieldTypes.db_Alpha;
            }

        }
    }
}
