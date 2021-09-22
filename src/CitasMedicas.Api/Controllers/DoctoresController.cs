using AutoMapper;
using CitasMedicas.Api.Inputs;
using CitasMedicas.DataAccess.Repositories;
using CitasMedicas.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctoresController : ControllerBase
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;

        public DoctoresController(IDoctorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _repository.GetAll();
            if (doctors == null) return BadRequest();
            return Ok(doctors);
        }

        [HttpPost]
        public IActionResult Post(DoctorInput model)
        {
            var doctor = _mapper.Map<Doctor>(model);
            var (flag, id) = _repository.Insert(doctor);
            doctor.Id = id-1;
            if (flag) return new CreatedAtRouteResult(new {doctor.Id }, doctor);
            return BadRequest();
        }
    }
}
