using Cadastro_Empregados.Apresentacao;
using Cadastro_Empregados.Repositorio;
using Cadastro_Empregados.Servico;

namespace Cadastro_Empregados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString =
                "Server=(localdb)\\MSSQLLocalDB;Database=Cadastro_Empregados;Trusted_Connection=True;TrustServerCertificate=True;";

            var repositorio = new RepositorioEmpregados(connectionString);
            var servico = new ServicoEmpregados(repositorio);
            var menu = new Menu(servico);

            menu.Executar();
        }
    }
}