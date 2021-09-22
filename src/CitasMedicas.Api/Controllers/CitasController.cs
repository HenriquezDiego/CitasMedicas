using System;
using System.Linq;
using AutoMapper;
using CitasMedicas.Api.Inputs;
using CitasMedicas.DataAccess.Repositories;
using CitasMedicas.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaRepository _repository;
        private readonly IMapper _mapper;

        public CitasController(ICitaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var citas = _repository.GetAll();
            if (citas == null) return BadRequest();
            return Ok(citas);
        }

        [HttpPost]
        public IActionResult Post(CitaInput model)
        {
            var cita = _mapper.Map<Cita>(model);

            var query = _repository.GetAll().Where(c =>
                cita.Fecha == c.Fecha && TimeSpan.Parse(cita.Hora) == TimeSpan.Parse(c.Hora) &&
                cita.DoctorId == c.DoctorId && c.EstadoCitaId != 3);
            if (query.Any()) return BadRequest("Fecha no disponible");
            var (flag, id) = _repository.Insert(cita);
            cita.CitaId = id - 1;
            if (flag) return new CreatedAtRouteResult(new {cita.CitaId},cita);
            return BadRequest();
        }
    }
}
