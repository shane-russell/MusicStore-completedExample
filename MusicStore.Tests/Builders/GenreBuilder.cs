using MusicStore.Data.DomainClasses;
using System;

namespace MusicStore.Tests.Builders
{
    public class GenreBuilder
    {
        private static Random Random = new Random();
        private readonly Genre _genre;

        public GenreBuilder() 
        {
            _genre = new Genre
            {
                Id = Random.Next(1, int.MaxValue),
                Name = Guid.NewGuid().ToString()
            };
        }

        public Genre Build() 
        {
            return _genre;
        }
    }
}
