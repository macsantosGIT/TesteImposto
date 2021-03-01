using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Imposto.Core.Domain;
using Imposto.Core.Service;
using Imposto.Core.Data;

namespace TesteImpostoTest
{
    [TestClass]
    public class UnitTestInseriNotaFiscal
    {
        [TestMethod]
        public void TestInseriNotaFiscalValid()
        {
            INotaFiscalRepository notaFiscalRepository = new NotaFiscalRepository();

            NotaFiscalService service = new NotaFiscalService(notaFiscalRepository);
            Pedido pedido = new Pedido();
            pedido.EstadoOrigem = "SP";
            pedido.EstadoDestino = "BA";
            pedido.NomeCliente = "Nome Cliente";
            pedido.ItensDoPedido.Add(
                new PedidoItem()
                {
                    Brinde = false,
                    CodigoProduto = "12-33-22",
                    NomeProduto = "Item 1",
                    Desconto = 0.0,
                    ValorItemPedido = 100
                });

            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal = service.EmitirNotaFiscal(pedido);
            var nomeArquivo = notaFiscal.NumeroNotaFiscal.ToString();

            bool resultado;
            try
            {
                resultado = service.GerarXML(notaFiscal, nomeArquivo);
                if (resultado)
                {
                    resultado = service.InserirNotaFiscal(notaFiscal);
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }
            Assert.IsTrue(resultado);
        }
        [TestMethod]
        public void TestInseriNotaFiscalInValid()
        {
            INotaFiscalRepository notaFiscalRepository = new NotaFiscalRepository();

            NotaFiscalService service = new NotaFiscalService(notaFiscalRepository);
            Pedido pedido = new Pedido();
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal = service.EmitirNotaFiscal(pedido);
            var nomeArquivo = notaFiscal.NumeroNotaFiscal.ToString();

            bool resultado;
            try
            {
                resultado = service.GerarXML(notaFiscal, nomeArquivo);
                if (resultado)
                {
                    resultado = service.InserirNotaFiscal(notaFiscal);
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }
            Assert.IsTrue(resultado);
        }
    }
}
