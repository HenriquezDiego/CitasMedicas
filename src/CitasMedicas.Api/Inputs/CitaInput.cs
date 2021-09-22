using System;

namespace CitasMedicas.Api.Inputs
{
    public class CitaInput
    {
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
    }
}
