using System;

namespace CitasMedicas.Models.Entities
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Dui { get; set; }
        public string Nit { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string TipoSangre { get; set; }
        public string AlergicoA { get; set; }
    }
}
