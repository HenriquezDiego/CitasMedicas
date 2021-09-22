using System;
using AutoMapper;
using CitasMedicas.Api.Inputs;
using CitasMedicas.Models.Entities;

namespace CitasMedicas.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DateTime, string>()
                .ConvertUsing(x => x.ToString(@"dd/MM/yyyy"));

            CreateMap<PacienteInput, Paciente>();
            CreateMap<DoctorInput, Doctor>();
            CreateMap<CitaInput, Cita>();
        }
    }
}
