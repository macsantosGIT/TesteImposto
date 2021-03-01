using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Imposto.Core.Domain;
using Imposto.Core.Service;
using Imposto.Core.Data;

namespace TesteImpostoTest
{
    [TestClass]
    public class UnitTestEmissaoNota
    {
        [TestMethod]
        public void TestEmitirNotaFiscalValid()
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

            bool resultado;
            try
            {
                notaFiscal = service.EmitirNotaFiscal(pedido);
                var nomeArquivo = notaFiscal.NumeroNotaFiscal.ToString();
                resultado = true;
            }
            catch (Exception)
            {
                resultado = false;
            }

            Assert.IsTrue(resultado);
        }
        [TestMethod]
        public void TestEmitirNotaFiscalInValid()
        {
            INotaFiscalRepository notaFiscalRepository = new NotaFiscalRepository();

            NotaFiscalService service = new NotaFiscalService(notaFiscalRepository);
            Pedido pedido = new Pedido();
            NotaFiscal notaFiscal = new NotaFiscal();

            bool resultado;
            try
            {
                notaFiscal = service.EmitirNotaFiscal(pedido);
                var nomeArquivo = notaFiscal.NumeroNotaFiscal.ToString();
                resultado = true;
            }
            catch (Exception)
            {
                resultado = false;
            }

            Assert.IsTrue(resultado);
        }

    }
}
