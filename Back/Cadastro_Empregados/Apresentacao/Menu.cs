using System;
using Cadastro_Empregados.Modelo;
using Cadastro_Empregados.Servico;

namespace Cadastro_Empregados.Apresentacao
{
    public class Menu
    {
        private readonly ServicoEmpregados _servico;

        public Menu(ServicoEmpregados servico)
        {
            _servico = servico;
        }

        public void Executar()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                MostrarOpcoes();
                opcao = LerInteiro("Escolha uma opção: ");

                try
                {
                    switch (opcao)
                    {
                        case 1: Cadastrar(); break;
                        case 2: Consultar(); break;
                        case 3: Atualizar(); break;
                        case 4: Deletar(); break;
                        case 0: break;
                        default: Console.WriteLine("Opção inválida!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }

        private void MostrarOpcoes()
        {
            Console.WriteLine("=== Menu do Empregado ===");
            Console.WriteLine("1 .Cadastrar empregado novo");
            Console.WriteLine("2 .Consultar cadastro");
            Console.WriteLine("3 .Atualizar cadastro");
            Console.WriteLine("4 .Deletar cadastro");
            Console.WriteLine("0 .Sair do Cadastro");
        }

        private void Cadastrar()
        {
            var empregado = new Empregados();

            Console.WriteLine("Digite o nome:");
            empregado.Nome = Console.ReadLine();

            Console.WriteLine("Digite o email:");
            empregado.Email = Console.ReadLine();

            Console.WriteLine("Digite o número de telefone:");
            empregado.Telefone = Console.ReadLine();

            _servico.AdicionarEmpregado(empregado);
            Console.WriteLine("Empregado cadastrado com sucesso!");
        }

        private void Consultar()
        {
            int id = LerInteiro("Digite o ID do empregado: ");
            var empregado = _servico.VerEmpregados(id);

            if (empregado == null)
            {
                Console.WriteLine("Empregado não encontrado.");
                return;
            }

            Console.WriteLine("=== Empregado ===");
            Console.WriteLine($"ID: {empregado.Id}");
            Console.WriteLine($"Nome: {empregado.Nome}");
            Console.WriteLine($"Email: {empregado.Email}");
            Console.WriteLine($"Telefone: {empregado.Telefone}");
        }

        private void Atualizar()
        {
            var empregado = new Empregados();
            empregado.Id = LerInteiro("Digite o ID do empregado: ");

            Console.WriteLine("Novo nome:");
            empregado.Nome = Console.ReadLine();

            Console.WriteLine("Novo email:");
            empregado.Email = Console.ReadLine();

            Console.WriteLine("Novo telefone:");
            empregado.Telefone = Console.ReadLine();

            _servico.AtualizarEmpregados(empregado);
            Console.WriteLine("Empregado atualizado com sucesso!");
        }

        private void Deletar()
        {
            int id = LerInteiro("Digite o ID do empregado para deletar: ");

            _servico.DeletarEmpregados(id);
            Console.WriteLine("Empregado deletado com sucesso!");
        }

        private int LerInteiro(string mensagem)
        {
            int valor;
            Console.Write(mensagem);

            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. Tente novamente: ");
            }

            return valor;
        }
    }
}