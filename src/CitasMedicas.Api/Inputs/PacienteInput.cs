using System;

namespace CitasMedicas.Api.Inputs
{
    public class PacienteInput
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Genero { get; set; }
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
