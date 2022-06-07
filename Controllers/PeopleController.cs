using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PeopleController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<PersonDTO>>> Get()
        {
            var people = await _context.People.ToListAsync();
            return _mapper.Map<List<PersonDTO>>(people);
        }
        [HttpGet("{id}", Name = "getPerson")]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            var person = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return new NoContentResult();
            }
            return _mapper.Map<PersonDTO>(person);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PersonCreationDTO personCreationDTO)
        {
            var person = _mapper.Map<Person>(personCreationDTO);
            _context.Add(person);
            await _context.SaveChangesAsync();
            var personDTO = _mapper.Map<PersonDTO>(person);
            return new CreatedAtRouteResult("getPerson", new { id = person.Id }, personDTO);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(int id, [FromForm] PersonCreationDTO personCreationDTO)
        //{
        //    var personDB = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
        //    if(personDB == null)
        //    {
        //        return new NotFoundResult();
        //    }
        //    personDB = _mapper.Map(personCreationDTO, personDB);
        //    if(personCreationDTO.Picture != null)
        //    {
        //        using(var memoryStream = new MemoryStream())
        //        {
        //            await personCreationDTO.Picture.CopyToAsync(memoryStream);
        //            var content = memoryStream.ToArray();
        //            var extension = Path.GetExtension(personCreationDTO.Picture.FileName);
        //            personDB.Picture = await fileStorageService.SaveFile
        //        }
        //    }
        //}

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PersonPatchDTO> patchDocument)
        {
            if(patchDocument == null)
            {
                return new BadRequestResult();
            }
            var enityFromDB = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
            if (enityFromDB == null)
            {
                return new NotFoundResult();
            }
            var entityDTO = _mapper.Map<PersonPatchDTO>(enityFromDB);
            patchDocument.ApplyTo(entityDTO, ModelState);
            var isValid = TryValidateModel(entityDTO);
            if (!isValid)
            {
                return new BadRequestResult();
            }
            _mapper.Map(entityDTO, enityFromDB);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}
