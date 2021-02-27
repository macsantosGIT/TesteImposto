using Autofac;
using Imposto.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TesteImposto
{
    static class Program
    {
     
        public static IContainer Container; 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Container = Configure();
            Application.Run(new FormImposto(Container.Resolve<INotaFiscalRepository>()));
        }

        /// <summary>
        /// Configuração da Injeção de dependência
        /// </summary>
        /// <returns></returns>
        static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NotaFiscalRepository>().As<INotaFiscalRepository>();
            builder.RegisterType<FormImposto>();
            return builder.Build();
        }
    }
}
