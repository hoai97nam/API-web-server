using Microsoft.Extensions.Logging;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class InMemoryRepository: IRepository
    {
        private List<Genre> _genre;
        private readonly ILogger<InMemoryRepository> _logger;
        public InMemoryRepository(ILogger<InMemoryRepository> logger)
        {
            _genre = new List<Genre>()
            {
                new Genre(){Id = 1, Name ="Comedy"},
                new Genre(){Id = 2, Name ="Action"}
            };
            this._logger = logger;
        }
        public async Task<List<Genre>> GetAllGenres()
        {
            _logger.LogInformation("executing all genres");
            await Task.Delay(1);
            return _genre;
        }
        public Genre GetGenreById(int id)
        {
            return _genre.FirstOrDefault(x => x.Id == id);
        }
        public void AddGenre(Genre genre)
        {
            genre.Id = _genre.Max(x => x.Id) + 1;
            _genre.Add(genre);
        }
    }
}
