﻿using MusicStore.Data.Services;
using MusicStore.Data.DomainClasses;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Data
{
    public class GenreDummyRepository : IGenreRepository
    {
        private static List<Genre> _genres = new List<Genre>
        {
            new Genre 
            { 
                Id = 1,
                Name = "Metal"
            },
            new Genre
            {
                Id = 2,
                Name = "Pop"
            },
            new Genre
            {
                Id = 3,
                Name = "Jazz"
            }
        };

        public IReadOnlyList<Genre> GetAll()
        {
            return _genres;
        }

        public Genre GetById(int genreId)
        {
            return _genres.FirstOrDefault(g => g.Id == genreId);
        }
    }
}
