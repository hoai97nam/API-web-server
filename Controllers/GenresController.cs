using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
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
        private readonly IRepository repository;
        private readonly ILogger<GenresController> _logger;

        public GenresController(IRepository repository, ILogger<GenresController> logger)
        {
            this.repository = repository;
            this._logger = logger;
        }
        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/allgenres")]
        //[ResponseCache(Duration =60)]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            _logger.LogInformation($"Getting all genres");
            return await repository.GetAllGenres();
        }
        [HttpGet("{Id:int}", Name = "getGenre")]
        public ActionResult<Genre> Get(int id, string param2)
        {
            _logger.LogDebug($"Get by id method executing ...");
            var genre = repository.GetGenreById(id);
            if (genre == null)
            {
                _logger.LogWarning($"Genre with Id {id} not found");
                //throw new ApplicationException();
                //return new NotFoundResult();
            }
            return genre;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
            repository.AddGenre(genre);
            return new CreatedAtRouteResult("getGenre", new { Id = genre.Id }, genre);
        }
    }
}
