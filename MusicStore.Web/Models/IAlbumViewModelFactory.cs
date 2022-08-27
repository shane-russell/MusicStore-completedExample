using MusicStore.Data.DomainClasses;

namespace MusicStore.Web.Models { 
    public interface IAlbumViewModelFactory
    {
        AlbumViewModel Create(Album album, Genre genre);
    }
}
