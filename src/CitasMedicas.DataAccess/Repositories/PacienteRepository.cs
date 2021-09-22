using CitasMedicas.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CitasMedicas.DataAccess.Core;

namespace CitasMedicas.DataAccess.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly IConnectionService _connection;

        public PacienteRepository(IConnectionService connection)
        {
            _connection = connection;
        }
        public Paciente Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Paciente> GetAll()
        {
            throw new NotImplementedException();
        }

        public (bool,int) Insert(Paciente entity)
        {
            using var cmd = new SqlCommand("AddPaciente", _connection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = entity.Nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar).Value = entity.Apellido;
            cmd.Parameters.Add("@genero", SqlDbType.Bit).Value = entity.Genero;
            cmd.Parameters.Add("@fechadenacimiento", SqlDbType.DateTime).Value = entity.FechaNacimiento;
            cmd.Parameters.Add("@dui", SqlDbType.VarChar).Value = entity.Dui;
            cmd.Parameters.Add("@nit", SqlDbType.VarChar).Value = entity.Nit;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = entity.Telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = entity.Email;
            cmd.Parameters.Add("@dir", SqlDbType.VarChar).Value = entity.TipoSangre;
            cmd.Parameters.Add("@TipoSangre", SqlDbType.VarChar).Value = entity.TipoSangre;
            cmd.Parameters.Add("@alergico", SqlDbType.VarChar).Value = entity.AlergicoA;

            var sqlCommand = new SqlCommand("select top(1) Id from Pacientes order by id DESC", _connection.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connection.Open();
            var lastId = 0;
            using(var reader = sqlCommand.ExecuteReader())
            {
                if (!reader.Read()) return (cmd.ExecuteNonQuery() > 0, lastId);
                lastId = int.Parse(reader[0].ToString()??"0");
                lastId++;
            }
            return (cmd.ExecuteNonQuery() > 0,lastId);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Paciente entity)
        {
            throw new NotImplementedException();
        }
    }
}
