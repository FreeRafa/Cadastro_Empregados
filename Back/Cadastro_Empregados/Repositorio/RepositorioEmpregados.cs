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
        private readonly string _connectionString;

        public RepositorioEmpregados(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection CriarConexao()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void AdicionarEmpregado(Empregados empregado) //CREATE
        {
            const string query =
                "INSERT INTO Empregados (Nome, Email, Telefone) VALUES (@Nome, @Email, @Telefone)";

            using (var connection = CriarConexao())
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = empregado.Nome;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = empregado.Email;
                cmd.Parameters.Add("@Telefone", SqlDbType.NVarChar, 30).Value = empregado.Telefone;
                cmd.ExecuteNonQuery();
            }
        }
        public Empregados VerEmpregados(int id)
        {
            const string query =
                "SELECT Id, Nome, Email, Telefone FROM Empregados WHERE Id = @Id";

            using (var connection = CriarConexao())
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
            return null;
        }

        public void AtualizarEmpregados(Empregados empregados)
        {
            const string query =
                "UPDATE Empregados SET Nome = @Nome, Email = @Email, Telefone = @Telefone WHERE Id = @Id";

            using (var connection = CriarConexao())
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = empregados.Id;
                cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = empregados.Nome;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = empregados.Email;
                cmd.Parameters.Add("@Telefone", SqlDbType.NVarChar, 30).Value = empregados.Telefone;
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletarEmpregados(int id)
        {
            const string query = "DELETE FROM Empregados WHERE Id = @Id";

            using (var connection = CriarConexao())
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
