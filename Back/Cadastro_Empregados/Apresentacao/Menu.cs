using Cadastro_Empregados.Modelo;
using Cadastro_Empregados.Servico;
using System;

namespace Cadastro_Empregados.Apresentacao
{
    internal class Menu
    {
        private readonly ServicoEmpregados _servicoEmpregados;
        private readonly ServicoSetores _servicoSetores;

        public Menu(ServicoEmpregados servicoEmpregados, ServicoSetores servicoSetores)
        {
            _servicoEmpregados = servicoEmpregados;
            _servicoSetores = servicoSetores;
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
                        case 2: ListarEmpregados(); break;
                        case 3: ConsultarPorId(); break;
                        case 4: Atualizar(); break;
                        case 5: Deletar(); break;
                        case 6: ListarSetores(); break;
                        case 0: Console.WriteLine("A sair..."); break;
                        default: Console.WriteLine("Opção inválida!"); break;
                    }
                }
                catch (ArgumentException ex)   // erros de validação do serviço
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                catch (Exception ex)           // qualquer outro erro (ex.: base de dados)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        private void MostrarOpcoes()
        {
            Console.WriteLine("=== Menu do Empregado ===");
            Console.WriteLine("1. Cadastrar empregado novo");
            Console.WriteLine("2. Listar empregados");
            Console.WriteLine("3. Consultar empregado por ID");
            Console.WriteLine("4. Atualizar cadastro");
            Console.WriteLine("5. Deletar cadastro");
            Console.WriteLine("6. Listar setores");
            Console.WriteLine("0. Sair");
        }

        private void Cadastrar()
        {
            var empregado = new Empregado();

            Console.WriteLine("--- Cadastro ---");
            Console.Write("Nome: ");
            empregado.Nome = Console.ReadLine();
            Console.Write("Email: ");
            empregado.Email = Console.ReadLine();
            Console.Write("Telefone: ");
            empregado.Telefone = Console.ReadLine();

            ListarSetores();
            empregado.SetorId = LerInteiro("ID do setor: ");

            _servicoEmpregados.Adicionar(empregado);
            Console.WriteLine("Empregado cadastrado com sucesso!");
        }

        private void ListarEmpregados()
        {
            var empregados = _servicoEmpregados.ListarTodos();

            if (empregados.Count == 0)
            {
                Console.WriteLine("Nenhum empregado cadastrado.");
                return;
            }

            Console.WriteLine("=== Empregados ===");
            foreach (var e in empregados)
                MostrarEmpregado(e);
        }

        private void ConsultarPorId()
        {
            int id = LerInteiro("ID do empregado: ");
            var empregado = _servicoEmpregados.ObterPorId(id);

            if (empregado == null)
            {
                Console.WriteLine("Empregado não encontrado.");
                return;
            }

            MostrarEmpregado(empregado);
        }

        private void Atualizar()
        {
            var empregado = new Empregado();

            empregado.Id = LerInteiro("ID do empregado a atualizar: ");
            Console.Write("Novo nome: ");
            empregado.Nome = Console.ReadLine();
            Console.Write("Novo email: ");
            empregado.Email = Console.ReadLine();
            Console.Write("Novo telefone: ");
            empregado.Telefone = Console.ReadLine();

            ListarSetores();
            empregado.SetorId = LerInteiro("Novo ID do setor: ");

            _servicoEmpregados.Atualizar(empregado);
            Console.WriteLine("Empregado atualizado com sucesso!");
        }

        private void Deletar()
        {
            int id = LerInteiro("ID do empregado a deletar: ");

            _servicoEmpregados.Deletar(id);
            Console.WriteLine("Empregado deletado com sucesso!");
        }

        private void ListarSetores()
        {
            Console.WriteLine("--- Setores ---");
            foreach (var s in _servicoSetores.ListarTodos())
                Console.WriteLine($"{s.Id} - {s.Nome}");
        }

        private void MostrarEmpregado(Empregado e)
        {
            Console.WriteLine($"ID: {e.Id} | Nome: {e.Nome} | Email: {e.Email} | " +
                              $"Telefone: {e.Telefone ?? "-"} | Setor: {e.Setor.Nome}");
        }

        // Repete a pergunta até o utilizador digitar um número válido
        private int LerInteiro(string mensagem)
        {
            int valor;
            Console.Write(mensagem);

            while (!int.TryParse(Console.ReadLine(), out valor))
                Console.Write("Valor inválido. " + mensagem);

            return valor;
        }
    }
}