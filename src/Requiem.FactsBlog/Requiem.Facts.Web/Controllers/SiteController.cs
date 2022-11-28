using Requiem.Facts.Web.Models;
using Requiem.Facts.Web.Mediatr;

namespace Requiem.Facts.Web.Controllers;

public class SiteController : Controller
{
    private readonly IMediator _mediator;
    public SiteController(IMediator mediator)
    {
        _mediator= mediator;
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