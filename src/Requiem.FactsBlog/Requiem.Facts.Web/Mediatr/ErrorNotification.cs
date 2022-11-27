using Requiem.Facts.Web.Mediatr.Base;

namespace Requiem.Facts.Web.Mediatr;

public class ErrorNotification : NotificationBase
{
    public ErrorNotification(string content, Exception? exception = null)
        : base("ERROR on xd.com", content, "requiem@gmail.com", "noreply@xd.com", exception)
    {

    }
}

public class ErrorNotificationHandler : NotificationHandlerBase<ErrorNotification>
{
    public ErrorNotificationHandler(IUnitOfWork unitOfWork, ILogger<ErrorNotification> logger) : base(unitOfWork, logger)
    {
    }
}   
