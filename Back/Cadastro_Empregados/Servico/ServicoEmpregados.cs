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

        public void VerEmpregados(int Id) 
        {
            if(Id <= 0)
                throw new Exception("Id innexistente");

            _repositorioEmpregados.VerEmpregados(Id);
        }

        public void AtualizarEmpregados() 
        {

        }
    }
}
