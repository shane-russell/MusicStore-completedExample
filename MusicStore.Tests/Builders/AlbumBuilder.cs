using MusicStore.Data.DomainClasses;
using System;

namespace MusicStore.Tests.Builders
{
    public class AlbumBuilder
    {
        private static Random Random = new Random();
        private readonly Album _album;

        public AlbumBuilder()
        {
            _album = new Album
            {
                Id = Random.Next(1, int.MaxValue),
                Title = Guid.NewGuid().ToString(),
                Artist = Guid.NewGuid().ToString(),
                GenreId = Random.Next(1, int.MaxValue)
            };
        }

        public Album Build()
        {
            return _album;
        }

        public AlbumBuilder WithGenreId(int id) 
        {
            _album.GenreId = id;
            return this;
        }
    }
}
