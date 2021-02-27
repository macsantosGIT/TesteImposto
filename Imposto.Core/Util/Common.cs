using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Util
{
    public class Common
    {
        public static List<String> ListaUF()
        {
            List<String> listaUF = new List<string>();
            listaUF.AddRange(new string[] {"AC", "AL", "AP", "AM", "BA", "CE", "DF", "GO", "ES", "MA",
                "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
                "RS", "RO", "RR", "SP", "SC", "SE", "TO"});
            return listaUF;
        }

        public static List<String> ListaUFSudeste()
        {
            List<String> listaUF = new List<string>();
            listaUF.AddRange(new string[] {"ES", "MG", "RJ", "SP"});
            return listaUF;
        }

        public static string Cfop(string ufOrigem, string ufDestino)
        {
            var cfop = "";
            if ((ufOrigem == "SP") && (ufDestino == "RJ"))
            {
                cfop = "6.000";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "PE"))
            {
                cfop = "6.001";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "MG"))
            {
                cfop = "6.002";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "PB"))
            {
                cfop = "6.003";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "PR"))
            {
                cfop = "6.004";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "PI"))
            {
                cfop = "6.005";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "RO"))
            {
                cfop = "6.006";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "SE"))
            {
                cfop = "6.007";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "TO"))
            {
                cfop = "6.008";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "SE"))
            {
                cfop = "6.009";
            }
            else if ((ufOrigem == "SP") && (ufDestino == "PA"))
            {
                cfop = "6.010";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "RJ"))
            {
                cfop = "6.000";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "PE"))
            {
                cfop = "6.001";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "MG"))
            {
                cfop = "6.002";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "PB"))
            {
                cfop = "6.003";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "PR"))
            {
                cfop = "6.004";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "PI"))
            {
                cfop = "6.005";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "RO"))
            {
                cfop = "6.006";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "SE"))
            {
                cfop = "6.007";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "TO"))
            {
                cfop = "6.008";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "SE"))
            {
                cfop = "6.009";
            }
            else if ((ufOrigem == "MG") && (ufDestino == "PA"))
            {
                cfop = "6.010";
            }
            return cfop;
        }

        public static string TipoIcms(string ufOrigem, string ufDestino, bool brinde)
        {
            var tipoIcms = "";

            if (ufDestino == ufOrigem || brinde)
            {
                tipoIcms = "60";
            }
            else
            {
                tipoIcms = "10";
            }

            return tipoIcms;
        }

        public static double AliquotaIcms(string ufOrigem, string ufDestino, bool brinde)
        {
            var aliquotaIcms = 0.0;

            if (ufDestino == ufOrigem || brinde)
            {
                aliquotaIcms = 0.18;
            }
            else
            {
                aliquotaIcms = 0.17;
            }
            return aliquotaIcms;
        }

        public static double AliquotaIpi(bool brinde)
        {
            var aliquotaIpi = 0.10;
            if (brinde)
                 aliquotaIpi = 0;

            return aliquotaIpi;
        }

        public static double DescontoItem(string ufDestino, double valor, double percDesconto)
        {
            var desctondo = (valor * (percDesconto / 100));

            var sudeste = Common.ListaUF().Contains(ufDestino);
            if (sudeste)
                desctondo = (valor * 0.10);

            return desctondo;
        }

        public static double BaseIcms(string cfop, double valor)
        {
            double baseIcms = valor;
            if (cfop == "6.009")
                baseIcms = valor * 0.90; //redução de base
            return baseIcms;
        }

    }
}
