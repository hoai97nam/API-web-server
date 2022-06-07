using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Filters;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController
    {
        //private readonly IRepository repository;
        private readonly ILogger<GenresController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(ILogger<GenresController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this._logger = logger;
            this._context = context;
            this._mapper = mapper;
        }
        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/allgenres")]
        //[ResponseCache(Duration =60)]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            _logger.LogInformation($"Getting all genres");
            var genres =  await _context.Genres.AsNoTracking().ToListAsync();
            var genreDTOs = _mapper.Map<List<GenreDTO>>(genres);
            return genreDTOs;
        }
        [HttpGet("{Id:int}", Name = "getGenre")]
        public async Task<ActionResult<GenreDTO>> Get(int Id)
        {
            _logger.LogDebug($"Get by id method executing ...");
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == Id);
            if (genre == null)
            {
                _logger.LogWarning($"Genre with Id {Id} not found");
                //throw new ApplicationException();
                //return new NotFoundResult();
            }
            var genreDTO = _mapper.Map<GenreDTO>(genre);
            return genreDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreation)
        {
            var genre = _mapper.Map<Genre>(genreCreation);
            _context.Add(genre);
            await _context.SaveChangesAsync();
            var genreDTO = _mapper.Map<GenreDTO>(genre);
            return new CreatedAtRouteResult("getGenre", new { Id = genreDTO.Id }, genreDTO);
        }
        [HttpPut("{id}")]
        public async Task< ActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreation)
        {
            var genre = _mapper.Map<Genre>(genreCreation);
            genre.Id = id;
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _context.Genres.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return new NotFoundResult();
            }
            _context.Remove(new Genre() { Id = id });
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}
