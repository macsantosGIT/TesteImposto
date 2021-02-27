using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {

        private SqlConnection dbCon;
        private void connection()
        {
            dbCon = new SqlConnection(DbConnection.ConnectionString);
        }

        public bool Delete(int notaFiscalId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotaFiscal> GerAllNotaFiscal()
        {
            connection();
            if (dbCon.State == ConnectionState.Closed)
                dbCon.Open();
            var nota = dbCon.Query<NotaFiscal>("select Id, NumeroNotaFiscal, Serie, NomeCliente, EstadoDestino, EstadoOrigem from NotaFiscal", commandType: CommandType.Text);
            dbCon.Close();
            return nota;
        }

        public bool Insert(NotaFiscal notaFiscal)
        {
            connection();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@pId", notaFiscal.Id, dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("pNumeroNotaFiscal", notaFiscal.NumeroNotaFiscal);
                param.Add("pSerie", notaFiscal.Serie);
                param.Add("pNomeCliente", notaFiscal.NomeCliente);
                param.Add("pEstadoDestino", notaFiscal.EstadoDestino);
                param.Add("pEstadoOrigem", notaFiscal.EstadoOrigem);

                var query = "dbo.P_NOTA_FISCAL";
                connection();
                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();
                
                var result = dbCon.Execute(query, param, commandType: CommandType.StoredProcedure);

                var idNota = 0;
                if (result != 0)
                {
                    idNota = param.Get<int>("@pId");
                }

                query = "dbo.P_NOTA_FISCAL_ITEM";
                foreach (var item in notaFiscal.ItensDaNotaFiscal)
                {
                    param = new DynamicParameters();
                    param.Add("@pId", 0);
                    param.Add("@pIdNotaFiscal", idNota);
                    param.Add("@pCfop", item.Cfop);
                    param.Add("@pTipoIcms", item.TipoIcms);
                    param.Add("@pBaseIcms", item.BaseIcms);
                    param.Add("@pAliquotaIcms", item.AliquotaIcms);
                    param.Add("@pValorIcms", item.ValorIcms);
                    param.Add("@pNomeProduto", item.NomeProduto);
                    param.Add("@pCodigoProduto", item.CodigoProduto);
                    param.Add("@pBaseIpi", item.BaseIpi);
                    param.Add("@pAliquotaIpi", item.AliquotaIpi);
                    param.Add("@pValorIpi", item.ValorIpi);
                    param.Add("@pDesconto", item.Desconto);

                    dbCon.Execute(query, param, commandType: CommandType.StoredProcedure);
                }

                dbCon.Close();

                return true;
            }
            catch (SqlException)
            {
                if (dbCon.State == ConnectionState.Open)
                    dbCon.Close();
                
                return false;
            }
        }

        public bool Update(NotaFiscal notaFiscal)
        {
            //stored para atualizar
            throw new NotImplementedException();
        }
    }
}
