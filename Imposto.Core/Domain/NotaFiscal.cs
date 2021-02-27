using Imposto.Core.Util;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Imposto.Core.Domain
{
    [Serializable]
    //[XmlRoot("NotaFiscal")]
    public class NotaFiscal
    {
        //[XmlElement("id")]
        public int Id { get; set; }
        //[XmlElement("NumeroNotaFiscal")]
        public int NumeroNotaFiscal { get; set; }
        //[XmlElement("Serie")]
        public int Serie { get; set; }
        //[XmlElement("NomeCliente")]
        public string NomeCliente { get; set; }
        //[XmlElement("EstadoDestino")]
        public string EstadoDestino { get; set; }
        //[XmlElement("EstadoOrigem")]
        public string EstadoOrigem { get; set; }
        //[XmlElement("ItensDaNotaFiscal")]
        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }

    }
}
