using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Empregados.DataConnec
{
    public class SqlConnec
    {
        private static readonly string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=Cadastro_Empregados;Trusted_Connection=True;TrustServerCertificate=True;";


        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

    }
}
