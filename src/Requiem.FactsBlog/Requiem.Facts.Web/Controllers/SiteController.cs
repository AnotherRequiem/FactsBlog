﻿using Microsoft.AspNetCore.Mvc;
using Requiem.Facts.Web.Models;
using System.Diagnostics;
using Requiem.Facts.Web.Mediatr;

namespace Requiem.Facts.Web.Controllers
{
    public class SiteController : Controller
    {
        private readonly IMediator _mediator;
        public SiteController(IMediator mediator)
        {
            _mediator= mediator;
        }
        public IActionResult Index(int? pageIndex, string tag, string search)
        {
            ViewData["Index"] = pageIndex;
            ViewData["Tag"] = tag;
            ViewData["Search"] = search;

            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            await _mediator.Publish(new ErrorNotification("Privacy test for notification"), HttpContext.RequestAborted);
            await _mediator.Publish(new FeedbackNotification("Helloyoursiteiscoolgood"), HttpContext.RequestAborted);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}