using Cadastro_Empregados.Modelo;
using Cadastro_Empregados.Repositorio;
using System.Collections.Generic;

namespace Cadastro_Empregados.Servico
{
    public class ServicoSetores
    {
        private readonly RepositorioSetores _repositorioSetores;

        public ServicoSetores(RepositorioSetores repositorioSetores)
        {
            _repositorioSetores = repositorioSetores;
        }

        public List<Setor> ListarTodos()
        {
            return _repositorioSetores.ListarTodos();
        }
    }
}