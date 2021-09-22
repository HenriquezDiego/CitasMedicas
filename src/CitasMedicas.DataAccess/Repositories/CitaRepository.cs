using CitasMedicas.DataAccess.Core;
using CitasMedicas.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Cita> GetAll()
        {
            var sqlCommand = new SqlCommand("SELECT [CitaId]\r\n      ,[PacienteId]\r\n      ,[DoctorId]\r\n      ,[Descripcion]\r\n      ,[Fecha]\r\n      ,[Hora]\r\n      ,[EstadoCitaId]\r\n\t  ,p.Nombre\r\n\t  ,p.Apellido\r\n\t  ,e.Nombre\r\n\t  ,d.Nombre as DoctorNombre\r\n\t  ,d.Apellido as DoctorApellido\r\n  FROM [CitasMedicas].[dbo].[Citas] C\r\n  INNER JOIN Pacientes P ON c.PacienteId = p.Id\r\n  INNER JOIN Doctores D ON c.DoctorId = d.Id\r\n  INNER JOIN Estados  E on c.EstadoCitaId = e.Id",
                _connection.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connection.Open();

            var citas = new List<Cita>();
            using(var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cita = new Cita
                    {
                        CitaId = int.Parse(reader[0]?.ToString() ?? "0"),
                        PacienteId = int.Parse(reader[1]?.ToString() ?? "0"),
                        DoctorId =  int.Parse(reader[2]?.ToString() ?? "0"),
                        Descripcion = reader[3].ToString(),
                        Fecha = DateTime.Parse(reader[4].ToString() ?? "2021-2-18"),
                        Hora = reader[5]?.ToString()??"00:00:00",
                        EstadoCitaId = int.Parse(reader[6]?.ToString() ?? "0"),
                        Paciente = reader[7]+" "+reader[8],
                        Estado = reader[9].ToString(),
                        Doctor = reader[10]+" "+reader[11],
                    };
                    citas.Add(cita);
                }
            }

            _connection.Close();
            return citas;
        }

        public (bool, int) Insert(Cita entity)
        {
            using var cmd = new SqlCommand("AddCita", _connection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@pacienteId", SqlDbType.Int).Value = entity.PacienteId;
            cmd.Parameters.Add("@doctorId", SqlDbType.Int).Value = entity.DoctorId;
            cmd.Parameters.Add("@des", SqlDbType.NVarChar).Value = entity.Descripcion;
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
            throw new NotImplementedException();
        }

        public bool Update(int id, Cita entity)
        {
            throw new NotImplementedException();
        }
    }
}
