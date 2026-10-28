using Cadastro_Empregados.Apresentacao;
using Cadastro_Empregados.DataConnec;
using Cadastro_Empregados.Repositorio;
using Cadastro_Empregados.Servico;
using System;

namespace Cadastro_Empregados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Testa a ligação uma vez, usando a ÚNICA connection string (em SqlConnec)
            try
            {
                using (var conn = SqlConnec.GetConnection())
                {
                    Console.WriteLine("Conexão bem-sucedida!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao ligar à base de dados: " + ex.Message);
                return;
            }

            // Composição das dependências (feita uma só vez, fora do loop)
            var repositorioSetores = new RepositorioSetores();
            var repositorioEmpregados = new RepositorioEmpregados();

            var servicoSetores = new ServicoSetores(repositorioSetores);
            var servicoEmpregados = new ServicoEmpregados(repositorioEmpregados, repositorioSetores);

            var menu = new Menu(servicoEmpregados, servicoSetores);
            menu.Executar();
        }
    }
}