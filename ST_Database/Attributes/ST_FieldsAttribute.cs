using System;
using SAPbobsCOM;

namespace ST_Database.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ST_FieldsAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public string DefaultValue { get; set; }
        public bool Mandatory { get; set; }
        public BoFldSubTypes SubType { get; set; }
        public string LinkUdo { get; set; }


        /// <summary>
        /// Define Informações para os campos a serem criados na base sap
        /// </summary>
        /// <param name="name">Nome do Campo na base SAP</param>
        /// <param name="description">Descrição do campo na base SAP</param>
        /// <param name="size">Tamanho para o campo</param>
        /// <param name="mandatory">Define se o preenchimento do campo é obrigatorio</param>
        /// <param name="subType">Define o subTipo do Campo para o sistema SAP</param>
        public ST_FieldsAttribute(string name, string description, int size, bool mandatory, BoFldSubTypes subType)
        {
            this.Name = name;
            this.Description = description;
            this.Size = size;
            this.Mandatory = mandatory;
            this.SubType = subType;
        }

        public ST_FieldsAttribute(string name, string description, int size, bool mandatory, BoFldSubTypes subType, string linkUdo)
        {
            this.Name = name;
            this.Description = description;
            this.Size = size;
            this.Mandatory = mandatory;
            this.SubType = subType;
            this.LinkUdo = linkUdo;
        }

        public ST_FieldsAttribute(string name, string description, int size, string defaultValue, bool mandatory, BoFldSubTypes subType)
        {
            this.Name = name;
            this.Description = description;
            this.Size = size;
            this.DefaultValue = defaultValue;
            this.Mandatory = mandatory;
            this.SubType = subType;
        }
    }
}
