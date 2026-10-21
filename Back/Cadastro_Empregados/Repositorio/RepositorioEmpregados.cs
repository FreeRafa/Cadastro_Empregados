using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastro_Empregados.DataConnec;
using Cadastro_Empregados.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace Cadastro_Empregados.Repositorio
{
    public class RepositorioEmpregados
    {
        private readonly string _ConnectionString;

        public RepositorioEmpregados(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void AdicionarEmpregado(Empregados empregado) //CREATE
        {
            using (var connection = SqlConnec.GetConnection())
            {
                string query = "INSERT INTO Empregados (Nome, Email, Telefone) VALUES (@Nome, @Email, @Telefone)";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = empregado.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = empregado.Email;
                    cmd.Parameters.Add("@Telefone", SqlDbType.NVarChar, 30).Value = empregado.Telefone;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Empregados> ListarEmpregados() //READ (todos)
        {
            const string query = "SELECT Id, Nome, Email, Telefone FROM Empregados ORDER BY Id";

            var lista = new List<Empregados>();

            using (var connection = SqlConnec.GetConnection())
            using (var cmd = new SqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Empregados
                    {
                        Id = (int)reader["Id"],
                        Nome = reader["Nome"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefone = reader["Telefone"].ToString()
                    });
                }
            }

            return lista;
        }

        public Empregados VerEmpregados(int id) //READ (um)
        {
            const string query = "SELECT Id, Nome, Email, Telefone FROM Empregados WHERE Id = @Id";

            using (var connection = SqlConnec.GetConnection())
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Empregados
                        {
                            Id = (int)reader["Id"],
                            Nome = reader["Nome"].ToString(),
                            Email = reader["Email"].ToString(),
                            Telefone = reader["Telefone"].ToString()
                        };
                    }
                }
            }

            return null; // não encontrou
        }

        public void AtualizarEmpregados(Empregados empregados) //ATUALIZAR
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                String query = @"UPDATE Empregados SET Nome = @nome, Email = @Email, Telefone = @Telefone WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = empregados.Id;
                cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = empregados.Nome;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = empregados.Email;
                cmd.Parameters.Add("@Telefone", SqlDbType.NVarChar, 30).Value = empregados.Telefone;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletarEmpregados(int Id) //DELETAR
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "Delete FROM Empregados WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}