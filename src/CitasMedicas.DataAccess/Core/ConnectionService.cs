using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CitasMedicas.DataAccess.Core
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConfiguration _configuration;
        public readonly SqlConnection SqlConnection = new();

        public ConnectionService (IConfiguration configuration)
        {
            _configuration = configuration;
            SqlConnection.ConnectionString=configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return SqlConnection;
        }

        public void Open()
        {
            try
            {
                SqlConnection.Open();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Close()
        {
            SqlConnection.Close();
        }
    }
}
