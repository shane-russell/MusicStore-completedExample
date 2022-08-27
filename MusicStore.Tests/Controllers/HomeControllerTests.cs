using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using MusicStore.Web.Controllers;
using MusicStore.Web.Services;
using NUnit.Framework;

namespace MusicStore.Tests
{
    public class HomeControllerTests
    {
        private HomeController _controller;
        private string _routeControllerName;
        private string _routeActionName;
        private byte[] _fileBytes;
        private Mock<IFileProvider> _fileProviderMock;

        [SetUp]
        public void Setup()
        {
            _fileBytes = Guid.NewGuid().ToByteArray();
            _fileProviderMock = new Mock<IFileProvider>();
            _fileProviderMock.Setup(provider => provider.GetFileBytes(It.IsAny<string>())).Returns(_fileBytes);
            _controller = new HomeController(_fileProviderMock.Object);
            SetRandomRouteDataForController();
        }

        [Test]
        public void Index_ReturnsDefaultView()
        {
            var result = _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.Null);
        }

        [Test]
        public void About_ReturnsContentContainingControllerNameAndActionName()
        {
            contentContainsControllerNameAndActionName(_controller.About());
        }

        [Test]
        public void Details_ReturnsContentContainingControllerNameActionNameAndParamName()
        {
            var random = new Random();
            var id = random.Next(1, int.MaxValue);
            _controller.RouteData.Values["id"] = id;

            var result = _controller.Details(id) as ContentResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo($"{_routeControllerName}:{_routeActionName}:{id}"));
        }

        [Test]
        public void Search_Rock_PermanentRedirect() 
        {
            var result = _controller.Search("Rock") as RedirectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Permanent, Is.True);
            Assert.That(result.Url, Is.EqualTo(HomeController.RockUrl));
        }

        [Test]
        public void Search_Jazz_RedirectToIndexAction() 
        {
            var result = _controller.Search("Jazz") as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Permanent, Is.False);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void Search_Metal_RedirectToDetailsActionWithARandomId() 
        {
            var result = _controller.Search("Metal") as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Permanent, Is.False);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(Convert.ToInt32(result.RouteValues["id"]), Is.GreaterThan(0));
        }

        [Test]
        public void Search_Classic_ContentOfSiteCssFile() 
        {
            var result = _controller.Search("Classic") as FileContentResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ContentType, Is.EqualTo("text/css"));
            Assert.That(result.FileContents, Is.EqualTo(_fileBytes));
            Assert.That(result.FileDownloadName, Is.EqualTo("site.css"));
            _fileProviderMock.Verify(p => p.GetFileBytes(@"wwwroot\css\site.css"), Times.Once);
        }

        [Test]
        public void Search_UnknownGenre_ReturnsContentContainingControllerNameAndGenreParamater() 
        {
            var genre = Guid.NewGuid().ToString();

            _controller.RouteData.Values["genre"] = genre;

            var result = _controller.Search(genre) as ContentResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo($"{_routeControllerName}:{_routeActionName}:{genre}"));
        }

        private void SetRandomRouteDataForController()
        {
            var routeData = new RouteData();

            _routeControllerName = Guid.NewGuid().ToString();
            routeData.Values["controller"] = _routeControllerName;

            _routeActionName = Guid.NewGuid().ToString();
            routeData.Values["action"] = _routeActionName;

            _controller.ControllerContext = new ControllerContext
            {
                RouteData = routeData
            };
        }

        private void contentContainsControllerNameAndActionName(IActionResult actionResult)
        {
            var result = actionResult as ContentResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo($"{_routeControllerName}:{_routeActionName}"));
        }
    }
}
