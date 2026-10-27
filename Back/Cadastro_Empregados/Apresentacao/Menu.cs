using System;
using System.Data.SqlClient;
using Cadastro_Empregados.Modelo;
using Cadastro_Empregados.Servico;

namespace Cadastro_Empregados.Apresentacao
{
    public class Menu
    {
        private readonly ServicoEmpregados _servicoEmpregados;
        private readonly ServicoSetores _servicoSetores;

        public Menu(ServicoEmpregados servicoEmpregados, ServicoSetores servicoSetores)
        {
            _servicoEmpregados = servicoEmpregados;
            _servicoSetores = servicoSetores;
        }

        // ===== MENU PRINCIPAL =====
        public void Executar()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine();
                Console.WriteLine("=== Menu Principal ===");
                Console.WriteLine("1 .Empregados");
                Console.WriteLine("2 .Setores");
                Console.WriteLine("0 .Sair");
                opcao = LerInteiro("Escolha uma opção: ");

                switch (opcao)
                {
                    case 1: MenuEmpregados(); break;
                    case 2: MenuSetores(); break;
                    case 0: Console.WriteLine("A sair..."); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        // ===== SUBMENU EMPREGADOS =====
        private void MenuEmpregados()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine();
                Console.WriteLine("=== Empregados ===");
                Console.WriteLine("1 .Cadastrar empregado novo");
                Console.WriteLine("2 .Consultar cadastro");
                Console.WriteLine("3 .Atualizar cadastro");
                Console.WriteLine("4 .Deletar cadastro");
                Console.WriteLine("0 .Voltar");
                opcao = LerInteiro("Escolha uma opção: ");

                switch (opcao)
                {
                    case 1: ExecutarSeguro(CadastrarEmpregado); break;
                    case 2: ExecutarSeguro(ConsultarEmpregado); break;
                    case 3: ExecutarSeguro(AtualizarEmpregado); break;
                    case 4: ExecutarSeguro(DeletarEmpregado); break;
                    case 0: break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        // ===== SUBMENU SETORES =====
        private void MenuSetores()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine();
                Console.WriteLine("=== Setores ===");
                Console.WriteLine("1 .Cadastrar setor novo");
                Console.WriteLine("2 .Listar setores");
                Console.WriteLine("3 .Atualizar setor");
                Console.WriteLine("4 .Deletar setor");
                Console.WriteLine("0 .Voltar");
                opcao = LerInteiro("Escolha uma opção: ");

                switch (opcao)
                {
                    case 1: ExecutarSeguro(CadastrarSetor); break;
                    case 2: ExecutarSeguro(() => MostrarSetores()); break;
                    case 3: ExecutarSeguro(AtualizarSetor); break;
                    case 4: ExecutarSeguro(DeletarSetor); break;
                    case 0: break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        // ===== EMPREGADOS =====
        private void CadastrarEmpregado()
        {
            var empregado = new Empregados();

            Console.WriteLine("Digite o nome:");
            empregado.Nome = Console.ReadLine();

            Console.WriteLine("Digite o email:");
            empregado.Email = Console.ReadLine();

            Console.WriteLine("Digite o número de telefone:");
            empregado.Telefone = Console.ReadLine();

            _servicoEmpregados.AdicionarEmpregado(empregado);
            Console.WriteLine("Empregado cadastrado com sucesso!");
        }

        private void ConsultarEmpregado()
        {
            if (!MostrarEmpregados())
                return;

            int id = LerInteiro("Digite o ID do empregado: ");
            var empregado = _servicoEmpregados.VerEmpregados(id);

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

        private bool MostrarEmpregados()
        {
            var lista = _servicoEmpregados.ListarEmpregados();

            if (lista.Count == 0)
            {
                Console.WriteLine("Não existem empregados cadastrados.");
                return false;
            }

            Console.WriteLine("=== Empregados cadastrados ===");
            foreach (var e in lista)
                Console.WriteLine($"ID: {e.Id} | Nome: {e.Nome}");

            return true;
        }

        private void AtualizarEmpregado()
        {
            var empregado = new Empregados();
            empregado.Id = LerInteiro("Digite o ID do empregado: ");

            Console.WriteLine("Novo nome:");
            empregado.Nome = Console.ReadLine();

            Console.WriteLine("Novo email:");
            empregado.Email = Console.ReadLine();

            Console.WriteLine("Novo telefone:");
            empregado.Telefone = Console.ReadLine();

            _servicoEmpregados.AtualizarEmpregados(empregado);
            Console.WriteLine("Empregado atualizado com sucesso!");
        }

        private void DeletarEmpregado()
        {
            int id = LerInteiro("Digite o ID do empregado para deletar: ");

            _servicoEmpregados.DeletarEmpregados(id);
            Console.WriteLine("Empregado deletado com sucesso!");
        }

        // ===== SETORES =====
        private void CadastrarSetor()
        {
            var setor = new Setores();

            Console.WriteLine("Digite o nome do setor:");
            setor.Nome = Console.ReadLine();

            _servicoSetores.AdicionarSetor(setor);
            Console.WriteLine("Setor cadastrado com sucesso!");
        }

        private bool MostrarSetores()
        {
            var lista = _servicoSetores.ListarSetores();

            if (lista.Count == 0)
            {
                Console.WriteLine("Não existem setores cadastrados.");
                return false;
            }

            Console.WriteLine("=== Setores cadastrados ===");
            foreach (var s in lista)
                Console.WriteLine($"ID: {s.SetorId} | Nome: {s.Nome}");

            return true;
        }

        private void AtualizarSetor()
        {
            if (!MostrarSetores())
                return;

            var setor = new Setores();
            setor.SetorId = LerInteiro("Digite o ID do setor: ");

            Console.WriteLine("Novo nome:");
            setor.Nome = Console.ReadLine();

            _servicoSetores.AtualizarSetor(setor);
            Console.WriteLine("Setor atualizado com sucesso!");
        }

        private void DeletarSetor()
        {
            if (!MostrarSetores())
                return;

            int id = LerInteiro("Digite o ID do setor para deletar: ");

            try
            {
                _servicoSetores.DeletarSetor(id);
                Console.WriteLine("Setor deletado com sucesso!");
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                // 547 = violação de chave estrangeira
                Console.WriteLine("Não é possível apagar um setor que ainda tem empregados.");
            }
        }

        // ===== AUXILIARES =====
        private void ExecutarSeguro(Action acao)
        {
            try
            {
                acao();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
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