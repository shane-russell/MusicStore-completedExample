using Microsoft.AspNetCore.Mvc;
using MusicStore.Web.Models;

namespace MusicStore.Web.ViewComponents
{
    public class AlbumOfTheDay:ViewComponent
    {
        private readonly AlbumViewModel _album;

        public AlbumOfTheDay() 
        {
            _album = new AlbumViewModel()
            {
                Artist = "A popular artist",
                Genre = "Some genre",
                Title = "Some title"
            };
        }

        public IViewComponentResult Invoke() 
        {
            return View(_album);
        }
    }
}
