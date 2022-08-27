using MusicStore.Data.DomainClasses;
using System.Collections.Generic;

namespace MusicStore.Data.Services
{
    public interface IGenreRepository
    {
        IReadOnlyList<Genre> GetAll();

        Genre GetById(int genreId);
    }
}
