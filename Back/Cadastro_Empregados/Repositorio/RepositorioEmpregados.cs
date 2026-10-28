using Cadastro_Empregados.DataConnec;
using Cadastro_Empregados.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Cadastro_Empregados.Repositorio
{
    public class RepositorioEmpregados
    {
        // O JOIN traz o nome do setor junto com os dados do empregado
        private const string SelectBase = @"
            SELECT e.Id, e.Nome, e.Email, e.Telefone, e.SetorId, s.Nome AS NomeSetor
            FROM Empregados e
            INNER JOIN Setores s ON s.Id = e.SetorId";

        public void Adicionar(Empregado empregado) // CREATE
        {
            using (var connection = SqlConnec.GetConnection())
            {
                string query = @"INSERT INTO Empregados (Nome, Email, Telefone, SetorId)
                                 VALUES (@Nome, @Email, @Telefone, @SetorId)";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = empregado.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = empregado.Email;
                    cmd.Parameters.Add("@Telefone", SqlDbType.NVarChar, 30).Value =
                        string.IsNullOrWhiteSpace(empregado.Telefone) ? (object)DBNull.Value : empregado.Telefone;
                    cmd.Parameters.Add("@SetorId", SqlDbType.Int).Value = empregado.SetorId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Empregado> ListarTodos() // READ (lista)
        {
            var empregados = new List<Empregado>();

            using (var connection = SqlConnec.GetConnection())
            {
                using (var cmd = new SqlCommand(SelectBase + " ORDER BY e.Id", connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        empregados.Add(Mapear(reader));
                }
            }

            return empregados;
        }

        public Empregado ObterPorId(int id) // READ (um)
        {
            using (var connection = SqlConnec.GetConnection())
            {
                using (var cmd = new SqlCommand(SelectBase + " WHERE e.Id = @Id", connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return Mapear(reader);
                    }
                }
            }

            return null;
        }

        public void Atualizar(Empregado empregado) // UPDATE
        {
            using (var connection = SqlConnec.GetConnection())
            {
                string query = @"UPDATE Empregados
                                 SET Nome = @Nome, Email = @Email, Telefone = @Telefone, SetorId = @SetorId
                                 WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = empregado.Id;
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = empregado.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = empregado.Email;
                    cmd.Parameters.Add("@Telefone", SqlDbType.NVarChar, 30).Value =
                        string.IsNullOrWhiteSpace(empregado.Telefone) ? (object)DBNull.Value : empregado.Telefone;
                    cmd.Parameters.Add("@SetorId", SqlDbType.Int).Value = empregado.SetorId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id) // DELETE
        {
            using (var connection = SqlConnec.GetConnection())
            {
                using (var cmd = new SqlCommand("DELETE FROM Empregados WHERE Id = @Id", connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Converte uma linha do reader num objeto Empregado
        private Empregado Mapear(SqlDataReader reader)
        {
            int ordTelefone = reader.GetOrdinal("Telefone");

            return new Empregado
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Telefone = reader.IsDBNull(ordTelefone) ? null : reader.GetString(ordTelefone),
                SetorId = reader.GetInt32(reader.GetOrdinal("SetorId")),
                Setor = new Setor
                {
                    Id = reader.GetInt32(reader.GetOrdinal("SetorId")),
                    Nome = reader.GetString(reader.GetOrdinal("NomeSetor"))
                }
            };
        }
    }
}