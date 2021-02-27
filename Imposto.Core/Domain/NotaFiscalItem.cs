using System;
using System.Xml.Serialization;

namespace Imposto.Core.Domain
{
    [Serializable]
    public class NotaFiscalItem
    {
        //[XmlElement("IdItem")]
        public int Id { get; set; }
        //[XmlElement("IdNotaFiscal")]
        public int IdNotaFiscal { get; set; }
        //[XmlElement("Cfop")]
        public string Cfop { get; set; }
        //[XmlElement("TipoIcms")]
        public string TipoIcms { get; set; }
        //[XmlElement("BaseIcms")]
        public double BaseIcms { get; set; }
        //[XmlElement("AliquotaIcms")]
        public double AliquotaIcms { get; set; }
        //[XmlElement("ValorIcms")]
        public double ValorIcms { get; set; }
        //[XmlElement("NomeProduto")]
        public string NomeProduto { get; set; }
        //[XmlElement("CodigoProduto")]
        public string CodigoProduto { get; set; }
        public double BaseIpi { get; set; }
        public double AliquotaIpi { get; set; }
        public double ValorIpi { get; set; }
        public double Desconto { get; set; }
    }
}
