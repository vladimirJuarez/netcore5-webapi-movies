using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Entidades;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace back_end.Repositories
{
    public class RepositoryCache: IRepository
    {
        private readonly List<Genre> _genres;
        public RepositoryCache()
        {
            _genres = new List<Genre>
            {
                new Genre {Id = 1, Name = "Romantic"},
                new Genre {Id = 2, Name = "Action"},
                new Genre {Id = 3, Name = "Comedy"},
                new Genre{Id = 4, Name = "Adventure"}
            };
        }

        public List<Genre> Get()
        {
            return _genres;
        }

        public async Task<Genre> GetById(int id)
        {
            await Task.Delay(1);
            return _genres.FirstOrDefault(x => x.Id == id);
        }
        
    }
}