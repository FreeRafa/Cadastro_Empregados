using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Cadastro_Empregados.Modelo;

namespace Cadastro_Empregados.Repositorio
{
    public class RepositorioSetores
    {
        private readonly string _connectionString;

        public RepositorioSetores(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AdicionarSetor(Setores setor) // CREATE
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Setores (Nome) VALUES (@Nome)";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = setor.Nome;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Setores> ListarSetores() // READ (todos)
        {
            var lista = new List<Setores>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT SetorId, Nome FROM Setores";
                using (var cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Setores
                            {
                                SetorId = (int)reader["SetorId"],
                                Nome = reader["Nome"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public Setores VerSetor(int setorId) // READ (por id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT SetorId, Nome FROM Setores WHERE SetorId = @SetorId";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@SetorId", SqlDbType.Int).Value = setorId;

                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Setores
                            {
                                SetorId = (int)reader["SetorId"],
                                Nome = reader["Nome"].ToString()
                            };
                        }
                    }
                }
            }

            return null; // não encontrou
        }

        public void AtualizarSetor(Setores setor) // UPDATE
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Setores SET Nome = @Nome WHERE SetorId = @SetorId";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@SetorId", SqlDbType.Int).Value = setor.SetorId;
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = setor.Nome;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletarSetor(int setorId) // DELETE
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Setores WHERE SetorId = @SetorId";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@SetorId", SqlDbType.Int).Value = setorId;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}