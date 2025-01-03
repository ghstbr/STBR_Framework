using System.Collections.Generic;

namespace STBR_Framework
{
    public class Dictionaries
    {
        public static Dictionary<int, string> GetDictionary_ObjectTypes_Ptbr()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "Plano de Contas");
            dictionary.Add(2, "Parceiros de Negócios");
            dictionary.Add(3, "Bancos");
            dictionary.Add(4, "Itens");
            dictionary.Add(5, "Grupos de Impostos");
            dictionary.Add(6, "Listas de Preços");
            dictionary.Add(7, "Preços Especiais");
            dictionary.Add(8, "Propriedades de Itens");
            dictionary.Add(12, "Usuários");
            dictionary.Add(13, "Nota Fiscal de Saída");
            dictionary.Add(14, "Nota de Crédito de Saída");
            dictionary.Add(15, "Nota de Entrega de Saída");
            dictionary.Add(16, "Devolução de Nota de Entrega de Saída");
            dictionary.Add(17, "Pedido de Venda");
            dictionary.Add(18, "Nota Fiscal de Entrada");
            dictionary.Add(19, "Nota de Crédito de Entrada");
            dictionary.Add(20, "Nota de Entrega de Entrada");
            dictionary.Add(21, "Devolução de Nota de Entrega de Entrada");
            dictionary.Add(22, "Pedido de Compra");

            return dictionary;
        }

        public static Dictionary<int, string> GetDictionary_ObjectTypes_Table()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "OACT");
            dictionary.Add(2, "OCRD");
            dictionary.Add(3, "ODSC");
            dictionary.Add(4, "OITM");
            dictionary.Add(5, "OITG");
            dictionary.Add(6, "OPLN");
            dictionary.Add(7, "OSPP");
            dictionary.Add(8, "OITB");
            dictionary.Add(12, "OUSR");
            dictionary.Add(13, "OINV");
            dictionary.Add(14, "ORIN");
            dictionary.Add(15, "ODLN");
            dictionary.Add(16, "ORDN");
            dictionary.Add(17, "ORDR");
            dictionary.Add(18, "OPCH");
            dictionary.Add(19, "ORPC");
            dictionary.Add(20, "OIGN");
            dictionary.Add(21, "ORDN");
            dictionary.Add(22, "OPOR");

            return dictionary;
        }
    }
}
