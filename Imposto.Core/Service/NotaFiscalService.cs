using Imposto.Core.Data;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Dapper;
using System.Data;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        INotaFiscalRepository _notaFiscalRepository;

        public NotaFiscalService(INotaFiscalRepository notaFiscalRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
        }

        public IEnumerable<NotaFiscal> GetAllNotas()
        {
            IEnumerable<NotaFiscal> notas = new List<NotaFiscal>();
            notas = _notaFiscalRepository.GerAllNotaFiscal();

            return notas;
        }

        public bool InserirNotaFiscal(NotaFiscal notaFiscal)
        {
            return _notaFiscalRepository.Insert(notaFiscal);
        }

        public bool GerarNotaFiscal(Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);

            var nomeArquivo = notaFiscal.NumeroNotaFiscal.ToString();
            var ok = GerarXML(notaFiscal, nomeArquivo);

            if(ok)
                ok = _notaFiscalRepository.Insert(notaFiscal);

            return ok;
        }

        public bool GerarXML(Object obj, string nomeArquivo)
        {
            try
            {
                string caminho = @"D:/Entrevistas/Teste/Nota" + nomeArquivo + ".xml";

                FileStream stream = new FileStream(caminho, FileMode.Create);
                XmlSerializer serializador = new XmlSerializer(obj.GetType());
                serializador.Serialize(stream, obj);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
