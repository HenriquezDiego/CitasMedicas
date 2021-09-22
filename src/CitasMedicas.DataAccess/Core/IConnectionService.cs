using System.Data.SqlClient;

namespace CitasMedicas.DataAccess.Core
{
    public interface IConnectionService
    {
        SqlConnection GetConnection();
        void Open();
        void Close();
    }
}
