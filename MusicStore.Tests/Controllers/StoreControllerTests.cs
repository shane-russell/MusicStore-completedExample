using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicStore.Data.DomainClasses;
using MusicStore.Data.Services;
using MusicStore.Tests.Builders;
using MusicStore.Web.Controllers;
using MusicStore.Web.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicStore.Tests
{
    public class StoreControllerTests
    {
        private Mock<IAlbumViewModelFactory> _albumViewModelFactoryMock;
        private Mock<IAlbumRepository> _albumRepositoryMock;
        private Mock<IGenreRepository> _genreRepositoryMock;
        private StoreController _storeController;

        [SetUp]
        public void Setup()
        {
            _albumViewModelFactoryMock = new Mock<IAlbumViewModelFactory>();
            _albumRepositoryMock = new Mock<IAlbumRepository>();
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _storeController = new StoreController(_genreRepositoryMock.Object, _albumRepositoryMock.Object, _albumViewModelFactoryMock.Object);
        }

        [Test]
        public void Index_ShowsListOfMusicGenres() 
        {
            var allGenres = new List<Genre>();
            _genreRepositoryMock.Setup(g => g.GetAll()).Returns(allGenres);


            var result = _storeController.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.SameAs(allGenres));

            _genreRepositoryMock.Verify(g => g.GetAll(), Times.Once);
        }

        [Test]
        public void Browse_ShowsAlbumsOfGenre() 
        {
            var genre = new GenreBuilder().Build();
            var albums = new List<Album>();

            _albumRepositoryMock.Setup(a => a.GetByGenre(It.IsAny<int>())).Returns(albums);
            _genreRepositoryMock.Setup(g => g.GetById(It.IsAny<int>())).Returns(genre);

            var result = _storeController.Browse(genre.Id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.SameAs(albums));

            _albumRepositoryMock.Verify(a => a.GetByGenre(genre.Id), Times.Once);
            _genreRepositoryMock.Verify(g => g.GetById(genre.Id), Times.Once);
        }

        [Test]
        public void Browse_InvalidGenreId_ReturnsNotFound() 
        {
            var genre = new GenreBuilder().Build();

            _albumRepositoryMock.Setup(a => a.GetByGenre(It.IsAny<int>())).Returns(() => null);

            var result = _storeController.Browse(genre.Id) as NotFoundResult;

            Assert.That(result, Is.Not.Null);

            _albumRepositoryMock.Verify(a => a.GetByGenre(genre.Id), Times.Once);
        }

        [Test]
        public void Details_ShowsDetailsOfAlbum() 
        {
            var album = new AlbumBuilder().Build();
            var genre = new GenreBuilder().Build();
            var albumViewModel = new AlbumViewModel();

            _albumRepositoryMock.Setup(a => a.GetById(It.IsAny<int>())).Returns(album);
            _genreRepositoryMock.Setup(g => g.GetById(It.IsAny<int>())).Returns(genre);
            _albumViewModelFactoryMock.Setup(f => f.Create(It.IsAny<Album>(), It.IsAny<Genre>())).Returns(albumViewModel);

            var result = _storeController.Details(album.Id) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.SameAs(albumViewModel));

            _albumRepositoryMock.Verify(a => a.GetById(album.Id), Times.Once);
            _genreRepositoryMock.Verify(g => g.GetById(album.GenreId), Times.Once);
            _albumViewModelFactoryMock.Verify(f => f.Create(album, genre), Times.Once);
        }

        [Test]
        public void Details_InvalidId_ReturnsNotFound() 
        {
            var genre = new GenreBuilder().Build();

            _albumRepositoryMock.Setup(a => a.GetById(It.IsAny<int>())).Returns(() => null);

            var result = _storeController.Details(genre.Id) as NotFoundResult;

            Assert.That(result, Is.Not.Null);

            _albumRepositoryMock.Verify(a => a.GetById(genre.Id), Times.Once);
        }

    }
}
