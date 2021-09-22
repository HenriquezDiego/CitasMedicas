using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CitasMedicas.DataAccess.Core;
using CitasMedicas.Models.Entities;

namespace CitasMedicas.DataAccess.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly IConnectionService _connection;

        public CitaRepository(IConnectionService connection)
        {
            _connection = connection;
        }
        public Cita Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Cita> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public (bool, int) Insert(Cita entity)
        {
            using var cmd = new SqlCommand("AddCita", _connection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@pacienteId", SqlDbType.Int).Value = entity.PacienteId;
            cmd.Parameters.Add("@doctorId", SqlDbType.Int).Value = entity.DoctorId;
            cmd.Parameters.Add("@des", SqlDbType.VarChar).Value = entity.Descripcion;
            cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = entity.Fecha;
            cmd.Parameters.Add("@hora", SqlDbType.Time).Value = entity.Hora;

            var sqlCommand = new SqlCommand("select top(1) CitaId from Citas order by CitaId DESC",
                _connection.GetConnection())
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

        public bool Update(int id, Cita entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
