using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CitasMedicas.DataAccess.Core;
using CitasMedicas.Models.Entities;

namespace CitasMedicas.DataAccess.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IConnectionService _connection;

        public DoctorRepository(IConnectionService connection)
        {
            _connection = connection;
        }
        public Doctor Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Doctor> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public (bool, int) Insert(Doctor entity)
        {
            using var cmd = new SqlCommand("AddDoctor", _connection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = entity.Nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar).Value = entity.Apellido;
            cmd.Parameters.Add("@fechadenacimiento", SqlDbType.DateTime).Value = entity.FechaNacimiento;
            cmd.Parameters.Add("@dui", SqlDbType.VarChar).Value = entity.Dui;
            cmd.Parameters.Add("@nit", SqlDbType.VarChar).Value = entity.Nit;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = entity.Telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = entity.Email;

            var sqlCommand = new SqlCommand("select top(1) Id from Doctores order by id DESC", _connection.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connection.Open();

            var flag = cmd.ExecuteNonQuery() > 0;

            int lastId;
            using(var reader = sqlCommand.ExecuteReader())
            {
                if (!reader.Read()){}
                    lastId = int.Parse(reader[0].ToString()??"0");
                lastId++;
            }
            _connection.Close();
            return (flag,lastId);
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int id, Doctor entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
