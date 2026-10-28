using Cadastro_Empregados.Modelo;
using Cadastro_Empregados.Repositorio;
using System;
using System.Collections.Generic;

namespace Cadastro_Empregados.Servico
{
    public class ServicoEmpregados
    {
        private readonly RepositorioEmpregados _repositorioEmpregados;
        private readonly RepositorioSetores _repositorioSetores;

        public ServicoEmpregados(RepositorioEmpregados repositorioEmpregados,
                                 RepositorioSetores repositorioSetores)
        {
            _repositorioEmpregados = repositorioEmpregados;
            _repositorioSetores = repositorioSetores;
        }

        public void Adicionar(Empregado empregado)
        {
            Validar(empregado);
            _repositorioEmpregados.Adicionar(empregado);
        }

        public List<Empregado> ListarTodos()
        {
            return _repositorioEmpregados.ListarTodos();
        }

        public Empregado ObterPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido.");

            return _repositorioEmpregados.ObterPorId(id);
        }

        public void Atualizar(Empregado empregado)
        {
            if (empregado.Id <= 0)
                throw new ArgumentException("Id inválido.");

            Validar(empregado);
            _repositorioEmpregados.Atualizar(empregado);
        }

        public void Deletar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido.");

            _repositorioEmpregados.Deletar(id);
        }

        // Regras de negócio comuns ao cadastro e à atualização
        private void Validar(Empregado empregado)
        {
            if (string.IsNullOrWhiteSpace(empregado.Nome))
                throw new ArgumentException("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(empregado.Email))
                throw new ArgumentException("O email é obrigatório.");

            if (_repositorioSetores.ObterPorId(empregado.SetorId) == null)
                throw new ArgumentException("O setor escolhido não existe.");
        }
    }
}