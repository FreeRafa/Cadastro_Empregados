using Cadastro_Empregados.DataConnec;
using Cadastro_Empregados.Modelo;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cadastro_Empregados.Repositorio
{
    public class RepositorioSetores
    {
        public List<Setor> ListarTodos()
        {
            var setores = new List<Setor>();

            using (var connection = SqlConnec.GetConnection())
            {
                string query = "SELECT Id, Nome FROM Setores ORDER BY Id";

                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        setores.Add(new Setor
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1)
                        });
                    }
                }
            }

            return setores;
        }

        public Setor ObterPorId(int id)
        {
            using (var connection = SqlConnec.GetConnection())
            {
                string query = "SELECT Id, Nome FROM Setores WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Setor
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}