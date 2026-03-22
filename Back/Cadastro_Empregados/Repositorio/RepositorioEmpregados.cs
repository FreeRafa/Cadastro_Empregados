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
                    cmd.Parameters.Add("@Telefone", SqlDbType.DateTime, 30).Value = empregado.Telefone;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void VerEmpregados(int Id) //READ
        {
            using (var connection = SqlConnec.GetConnection())
            {
                string query = "SELECT Id, Nome, Email, Telefone FROM Empregados";
                using (var cmd = new SqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, " +
                                $"Nome: {reader["Nome"]}, " +
                                $"Email: {reader["Email"]}, " +
                                $"Telefone: {reader["Telefone"]}");
                        }
                    }
                }
            }
        }
        public void AtualizarEmpregados(Empregados empregados) //ATUALIZAR
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                String query = @"UPDATE Empregados SET Nome = @nome, Email = @Email, Telefone = @Telefone WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, connection);

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
