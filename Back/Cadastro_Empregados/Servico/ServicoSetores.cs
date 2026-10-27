using System;
using System.Collections.Generic;
using Cadastro_Empregados.Modelo;
using Cadastro_Empregados.Repositorio;

namespace Cadastro_Empregados.Servico
{
    public class ServicoSetores
    {
        private readonly RepositorioSetores _repositorioSetores;

        public ServicoSetores(RepositorioSetores repositorioSetores)
        {
            _repositorioSetores = repositorioSetores;
        }

        public void AdicionarSetor(Setores setor)
        {
            if (string.IsNullOrWhiteSpace(setor.Nome))
                throw new Exception("O nome do setor é obrigatório!");

            _repositorioSetores.AdicionarSetor(setor);
        }

        public List<Setores> ListarSetores()
        {
            return _repositorioSetores.ListarSetores();
        }

        public Setores VerSetor(int setorId)
        {
            if (setorId <= 0)
                throw new ArgumentException("Id inválido");

            return _repositorioSetores.VerSetor(setorId);
        }

        public void AtualizarSetor(Setores setor)
        {
            if (setor.SetorId <= 0)
                throw new ArgumentException("Id inválido");

            if (string.IsNullOrWhiteSpace(setor.Nome))
                throw new Exception("O nome do setor é obrigatório!");

            _repositorioSetores.AtualizarSetor(setor);
        }

        public void DeletarSetor(int setorId)
        {
            if (setorId <= 0)
                throw new ArgumentException("Id inválido");

            _repositorioSetores.DeletarSetor(setorId);
        }
    }
}