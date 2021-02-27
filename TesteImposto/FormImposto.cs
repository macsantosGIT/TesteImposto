using Imposto.Core.Service;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Imposto.Core.Domain;
using Imposto.Core.Data;
using Imposto.Core.Util;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();

        INotaFiscalRepository _notaFiscalRepository;
        

        public FormImposto(INotaFiscalRepository notaFiscalRepository)
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            _notaFiscalRepository = notaFiscalRepository;
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Desconto %", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            if (!ValidarCampoVazio()) return;

            if (!ValidarUF(txtEstadoOrigem.Text))
            {
                MessageBox.Show("Estado de Origem inválido!");
                return;
            }

            if (!ValidarUF(txtEstadoDestino.Text))
            {
                MessageBox.Show("Estado de Destino inválido!");
                return;
            }

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            if (table.Rows.Count == 0) 
            { 
                MessageBox.Show("Inserir pelo menos 1(um) item no pedido!");
                return;
            }

            NotaFiscalService service = new NotaFiscalService(_notaFiscalRepository);
            pedido.EstadoOrigem = txtEstadoOrigem.Text;
            pedido.EstadoDestino = txtEstadoDestino.Text;
            pedido.NomeCliente = textBoxNomeCliente.Text;

            foreach (DataRow row in table.Rows)
            {
                var brinde = !String.IsNullOrEmpty(row["Brinde"].ToString());

                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = brinde,
                        CodigoProduto =  row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        Desconto = Convert.ToDouble(row["Desconto %"].ToString()),
                        ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())            
                    });
            }

            if (service.GerarNotaFiscal(pedido))
            {
                LimparCampo();
                MessageBox.Show("Operação efetuada com sucesso");
            }
            else
            {
                MessageBox.Show("Não á possível inserrir esta Nota Fiscal!");
            }
        }

        private void LimparCampo()
        {
            dataGridViewPedidos.DataSource = GetTablePedidos();
            textBoxNomeCliente.Text = "";
            txtEstadoOrigem.Text = "";
            txtEstadoDestino.Text = "";
        }

        private bool ValidarCampoVazio()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox && control.Text == "")
                {
                    MessageBox.Show($"O campo {control.Tag}, deve ser preenchido!");
                    return false;
                }
            }
            return true;
        }

        private bool ValidarUF(string uf)
        {
            var listaUF = Common.ListaUF();
            return listaUF.Contains(uf);
        }
    }
}
