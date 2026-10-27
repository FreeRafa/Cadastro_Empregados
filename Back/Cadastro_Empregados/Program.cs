using Cadastro_Empregados.Apresentacao;
using Cadastro_Empregados.Repositorio;
using Cadastro_Empregados.Servico;

namespace Cadastro_Empregados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Cadastro_Empregados;Trusted_Connection=True;TrustServerCertificate=True;";

            // Empregados
            var repositorioEmpregados = new RepositorioEmpregados(connectionString);
            var servicoEmpregados = new ServicoEmpregados(repositorioEmpregados);

            // Setores
            var repositorioSetores = new RepositorioSetores(connectionString);
            var servicoSetores = new ServicoSetores(repositorioSetores);

            // Menu recebe os dois serviços
            var menu = new Menu(servicoEmpregados, servicoSetores);

            menu.Executar();
        }
    }
}