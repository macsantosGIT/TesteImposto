using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Imposto.Core.Data
{
    public interface INotaFiscalRepository
    {
        IEnumerable<NotaFiscal> GerAllNotaFiscal();
        bool Insert(NotaFiscal notaFiscal);
        bool Update(NotaFiscal notaFiscal);
        bool Delete(int notaFiscalId);
    }
}
