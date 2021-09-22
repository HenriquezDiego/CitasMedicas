using AutoMapper;
using CitasMedicas.Api.Inputs;
using CitasMedicas.DataAccess.Repositories;
using CitasMedicas.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository _repository;
        private readonly IMapper _mapper;

        public PacientesController(IPacienteRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(PacienteInput model)
        {
            var paciente = _mapper.Map<Paciente>(model);
            var (flag,id) = _repository.Insert(paciente);
            paciente.Id = id;
            if (flag) return new CreatedAtRouteResult(new{id},paciente);
            return BadRequest();
        }
    }
}
