using System;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Web.Services;

namespace MusicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public const string RockUrl = @"https://www.youtube.com/watch?v=v2AC41dglnM";
        private readonly IFileProvider _fileProvider;

        public HomeController(IFileProvider fileProvider) 
        {
            _fileProvider = fileProvider;
        }

        private IActionResult returnRouteInfo()
        {
            return CreateInfoOfContent();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return returnRouteInfo();
        }

        public IActionResult Details(int id)
        {
            return returnRouteInfo();
        }

        public IActionResult Search(string genre)
        {
            switch (genre.ToLower())
            {
                case "rock":
                    return RedirectPermanent(RockUrl);
                case "jazz":
                    return RedirectToAction("Index");
                case "metal":
                    return RedirectToAction("Details", new { id = new Random().Next() });
                case "classic":
                    return File(_fileProvider.GetFileBytes(@"wwwroot\css\site.css"),"text/css","site.css");
                default:
                    return CreateInfoOfContent();
            }
        }

        private ContentResult CreateInfoOfContent() 
        {
            var controllerName = ControllerContext.RouteData.Values["controller"];
            var actionName = ControllerContext.RouteData.Values["action"];
            var message = $"{controllerName}:{actionName}";

            var id = ControllerContext.RouteData.Values["id"];
            if (id != null)
            {
                message += $":{id}";
            }

            var genre = ControllerContext.RouteData.Values["genre"];
            if (genre != null)
            {
                message += $":{genre}";
            }

            return Content(message);
        }
    }
}
