using MusicStore.Tests.Builders;
using MusicStore.Web.Models;
using NUnit.Framework;
using System;

namespace MusicStore.Tests.Models
{
    [TestFixture]
    public class AlbumViewModelFactoryTests
    {
        private AlbumViewModelFactory _albumViewModelFactory;

        [SetUp]
        public void Setup() 
        {
            _albumViewModelFactory = new AlbumViewModelFactory();
        }

        [Test]
        public void Create_ValidAlbumAndValidGenre_CorrectlyMapped() 
        {
            var genre = new GenreBuilder().Build();
            var album = new AlbumBuilder().WithGenreId(genre.Id).Build();

            var result = _albumViewModelFactory.Create(album, genre);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Artist, Is.EqualTo(album.Artist));
            Assert.That(result.Genre, Is.EqualTo(genre.Name));
            Assert.That(result.Title, Is.EqualTo(album.Title));
        }

        [Test]
        public void Create_MissingGenre_TrowsException() 
        {
            var album = new AlbumBuilder().Build();
            Assert.That(() => _albumViewModelFactory.Create(album, null), Throws.ArgumentException);
        }

        [Test]
        public void Create_MissingAlbum_TrowsException()
        {
            var genre = new GenreBuilder().Build();
            Assert.That(() => _albumViewModelFactory.Create(null, genre), Throws.ArgumentException);
        }

        [Test]
        public void Create_MismatchBetweenAlbumAndGenre_TrowsException()
        {
            var genre = new GenreBuilder().Build();
            var album = new AlbumBuilder().WithGenreId(genre.Id + 1).Build();

            Assert.That(() => _albumViewModelFactory.Create(album, genre), Throws.ArgumentException);
        }

    }
}
