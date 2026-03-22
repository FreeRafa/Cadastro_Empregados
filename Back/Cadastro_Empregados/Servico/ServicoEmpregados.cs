using Cadastro_Empregados.Modelo;
using Cadastro_Empregados.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Empregados.Servico
{
    public class ServicoEmpregados
    {
        private readonly RepositorioEmpregados _repositorioEmpregados;

        //Metodo construtor
        public ServicoEmpregados(RepositorioEmpregados repositorioEmpregados)
        {
            _repositorioEmpregados = repositorioEmpregados;
        }

        //Regras de negocio
        public void AdicionarEmpregado(Empregados empregado)
        {
            if (string.IsNullOrWhiteSpace(empregado.Nome))
                throw new Exception("Nome nao pode ser nulo!");

            if (string.IsNullOrWhiteSpace(empregado.Email))
                throw new Exception("Email nao pode ser vazio!");

            _repositorioEmpregados.AdicionarEmpregado(empregado);

        }

        public Empregados VerEmpregados(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id invalido");

            return _repositorioEmpregados.VerEmpregados(id);
        }

        public void AtualizarEmpregados(Empregados empregados) 
        {
            if (empregados.Id <= 0)
                throw new ArgumentException("Id invalido");

            if (string.IsNullOrWhiteSpace(empregados.Nome))
                throw new Exception("Employee name must be required.");

            if (string.IsNullOrWhiteSpace(empregados.Email))
                throw new Exception("O email e obrigatorio");

            _repositorioEmpregados.AtualizarEmpregados(empregados);
        }

        public void DeletarEmpregados(int Id) 
        {
            if (Id <= 0)
                throw new ArgumentException("Id invalido");

            _repositorioEmpregados.DeletarEmpregados(Id);
        }
    }
}
