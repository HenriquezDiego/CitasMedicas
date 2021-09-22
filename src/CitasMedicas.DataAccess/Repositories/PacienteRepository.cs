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
            var sqlCommand = new SqlCommand($"select * from Pacientes where id={id}",
                _connection.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connection.Open();

            var paciente = new Paciente();

            using(var reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    paciente = new Paciente()
                    {
                        Id = int.Parse(reader[0]?.ToString() ?? "0"),
                        Nombre = reader[1].ToString(),
                        Apellido =  reader[2].ToString(),
                        Genero = bool.Parse(reader[3]?.ToString()??"0"),
                        FechaNacimiento = DateTime.Parse(reader[4].ToString() ?? "2021-2-18"),
                        Dui = reader[5].ToString(),
                        Nit = reader[6].ToString(),
                        Telefono = reader[7].ToString(),
                        Email = reader[8].ToString(),
                        Direccion = reader[9].ToString(),
                        AlergicoA = reader[10].ToString(),
                        TipoSangre = reader[11].ToString(),
                    };
                }
            }

            _connection.Close();
            return paciente;
        }

        public IEnumerable<Paciente> GetAll()
        {
            var sqlCommand = new SqlCommand("select * from Pacientes",
                _connection.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connection.Open();

            var pacientes = new List<Paciente>();
            using(var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var paciente = new Paciente()
                    {
                        Id = int.Parse(reader[0]?.ToString() ?? "0"),
                        Nombre = reader[1].ToString(),
                        Apellido =  reader[2].ToString(),
                        Genero = bool.Parse(reader[3]?.ToString()??"0"),
                        FechaNacimiento = DateTime.Parse(reader[4].ToString() ?? "2021-2-18"),
                        Dui = reader[5].ToString(),
                        Nit = reader[6].ToString(),
                        Telefono = reader[7].ToString(),
                        Email = reader[8].ToString(),
                        Direccion = reader[9].ToString(),
                        AlergicoA = reader[10].ToString(),
                        TipoSangre = reader[11].ToString(),
                    };
                    pacientes.Add(paciente);
                }
            }

            _connection.Close();
            return pacientes;
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

        public bool Update(int id, Paciente entity)
        {
            throw new NotImplementedException();
        }
    }
}
