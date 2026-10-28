using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Empregados.Modelo
{

    public class Empregado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public int SetorId { get; set; }   // chave estrangeira
        public Setor Setor { get; set; }   // propriedade de navegação (preenchida pelo JOIN)
    }
}


