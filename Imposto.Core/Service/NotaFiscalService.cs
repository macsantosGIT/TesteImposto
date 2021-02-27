using Imposto.Core.Data;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Dapper;
using System.Data;
using Imposto.Core.Util;

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
            notaFiscal = EmitirNotaFiscal(pedido);

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

        public NotaFiscal EmitirNotaFiscal(Pedido pedido)
        {
            NotaFiscal nota = new NotaFiscal();
            nota.NumeroNotaFiscal = 99991;
            nota.Serie = new Random().Next(Int32.MaxValue);
            nota.NomeCliente = pedido.NomeCliente;

            nota.EstadoDestino = pedido.EstadoDestino;
            nota.EstadoOrigem = pedido.EstadoOrigem;

            if (pedido.ItensDoPedido.Count > 0)
                nota.ItensDaNotaFiscal = new List<NotaFiscalItem>();

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();
                notaFiscalItem.Desconto = itemPedido.Desconto;

                var desconto = Common.DescontoItem(nota.EstadoDestino, itemPedido.ValorItemPedido, itemPedido.Desconto);
                itemPedido.ValorItemPedido = itemPedido.ValorItemPedido - desconto;
                notaFiscalItem.Cfop = Common.Cfop(nota.EstadoOrigem, nota.EstadoDestino);
                notaFiscalItem.TipoIcms = Common.TipoIcms(nota.EstadoOrigem, nota.EstadoDestino, itemPedido.Brinde);
                notaFiscalItem.AliquotaIcms = Common.AliquotaIcms(nota.EstadoOrigem, nota.EstadoDestino, itemPedido.Brinde);
                notaFiscalItem.AliquotaIpi = Common.AliquotaIpi(itemPedido.Brinde);
                notaFiscalItem.BaseIcms = Common.BaseIcms(notaFiscalItem.Cfop, itemPedido.ValorItemPedido);
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                notaFiscalItem.BaseIpi = itemPedido.ValorItemPedido;
                notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;
                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                nota.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            return nota;
        }


    }
}
