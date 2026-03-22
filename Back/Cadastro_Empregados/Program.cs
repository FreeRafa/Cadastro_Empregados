using Cadastro_Empregados.Modelo;
using Cadastro_Empregados.Repositorio;
using Cadastro_Empregados.Servico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cadastro_Empregados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-42RL6N1;Database=Cadastro_Empregados;User Id=sa;Password=135113rr;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Conexão bem-sucedida!");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro de SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro geral: " + ex.Message);
            }

            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("=== Menu do Empregado ===");
                Console.WriteLine("1 .Cadastrar empregado novo");
                Console.WriteLine("2 .Listar Cadastros");
                Console.WriteLine("3 .Atualizar cadastro");
                Console.WriteLine("4 .Deletar cadastro");
                Console.WriteLine("0 .Sair do Cadastro");
                Console.Write("Escolha uma opção: ");

                opcao = int.Parse(Console.ReadLine());

                var repositorio = new RepositorioEmpregados(connectionString);
                var servico = new ServicoEmpregados(repositorio);

                switch (opcao)
                {
                    case 1:

                        Empregados empregado = new Empregados();

                        Console.WriteLine("Cadastro:");

                        Console.WriteLine("Digite seu Nome:");
                        empregado.Nome = Console.ReadLine();

                        Console.WriteLine("Digite seu email:");
                        empregado.Email = Console.ReadLine();

                        Console.WriteLine("Digite seu numero de telefone:");
                        empregado.Telefone = Console.ReadLine();

                        Console.WriteLine("Usuário cadastrado com sucesso!");

                        servico.AdicionarEmpregado(empregado);

                        break;

                    case 2:

                        Console.WriteLine("Digite o ID do empregado:");
                        int idBusca = int.Parse(Console.ReadLine());

                        var empregados = servico.VerEmpregados(idBusca);

                        if (empregados != null)
                        {
                            Console.WriteLine("=== Empregado ===");
                            Console.WriteLine($"ID: {empregados.Id}");
                            Console.WriteLine($"Nome: {empregados.Nome}");
                            Console.WriteLine($"Email: {empregados.Email}");
                            Console.WriteLine($"Telefone: {empregados.Telefone}");
                        }
                        else
                        {
                            Console.WriteLine("Empregado não encontrado.");
                        }

                        break;

                    case 3:

                        Empregados empAtualizar = new Empregados();

                        Console.WriteLine("Digite o ID do empregado:");
                        empAtualizar.Id = int.Parse(Console.ReadLine());

                        Console.WriteLine("Novo Nome:");
                        empAtualizar.Nome = Console.ReadLine();

                        Console.WriteLine("Novo Email:");
                        empAtualizar.Email = Console.ReadLine();

                        Console.WriteLine("Novo Telefone:");
                        empAtualizar.Telefone = Console.ReadLine();

                        servico.AtualizarEmpregados(empAtualizar);

                        Console.WriteLine("Empregado atualizado com sucesso!");

                        break;

                    case 4:

                        Console.WriteLine("Digite o ID do empregado para deletar:");
                        int id = int.Parse(Console.ReadLine());

                        servico.DeletarEmpregados(id);

                        Console.WriteLine("Empregado deletado com sucesso!");

                        break;

                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }
    }
}
