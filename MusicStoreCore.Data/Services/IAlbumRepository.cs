using MusicStore.Data.DomainClasses;
using System.Collections.Generic;

namespace MusicStore.Data.Services
{
    public interface IAlbumRepository
    {
        IReadOnlyList<Album> GetByGenre(int genreId);
        Album GetById(int id);
    }
}
