using Microsoft.AspNetCore.Mvc;
using MusicStore.Data.DomainClasses;
using MusicStore.Data.Services;
using MusicStore.Web.Models;
using System.Collections.Generic;

namespace MusicStore.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IAlbumViewModelFactory _albumViewModelFactory;

        public StoreController(IGenreRepository genreRepository, IAlbumRepository albumRepository, IAlbumViewModelFactory albumViewModelFactory) 
        {
            _genreRepository = genreRepository;
            _albumRepository = albumRepository;
            _albumViewModelFactory = albumViewModelFactory;
        }

        public IActionResult Index()
        {
            var result = _genreRepository.GetAll();
            return View(result);
        }

        public IActionResult Browse(int genreId)
        {
            IReadOnlyList<Album> albums = _albumRepository.GetByGenre(genreId);

            if (albums == null) 
            {
                return NotFound();
            }

            ViewBag.Genre = _genreRepository.GetById(genreId).Name;

            return View(albums);
        }

        public IActionResult Details(int id)
        {
            Album album = _albumRepository.GetById(id);

            if (album == null) {
                return NotFound();
            }

            Genre genre = _genreRepository.GetById(album.GenreId);

            AlbumViewModel model = _albumViewModelFactory.Create(album, genre);

            return View(model);
        }
    }
}
