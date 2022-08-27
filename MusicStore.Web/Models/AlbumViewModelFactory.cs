using MusicStore.Data.DomainClasses;
using System;

namespace MusicStore.Web.Models
{
    public class AlbumViewModelFactory : IAlbumViewModelFactory
    {
        public AlbumViewModel Create(Album album, Genre genre)
        {
            if (album == null || genre == null || album.GenreId != genre.Id) 
            {
                throw new ArgumentException();
            }

            return new AlbumViewModel
            {
                Title = album.Title,
                Artist = album.Artist,
                Genre = genre.Name
            };
        }
    }
}
