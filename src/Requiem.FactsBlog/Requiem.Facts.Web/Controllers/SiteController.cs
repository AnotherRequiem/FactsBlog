using Microsoft.AspNetCore.Mvc;
using Requiem.Facts.Web.Models;
using System.Diagnostics;

namespace Requiem.Facts.Web.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index(int? pageIndex, string tag, string search)
        {
            ViewData["Index"] = pageIndex;
            ViewData["Tag"] = tag;
            ViewData["Search"] = search;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}