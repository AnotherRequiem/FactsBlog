using Requiem.Facts.Web.Mediatr.Base;

namespace Requiem.Facts.Web.Mediatr;

public class FeedbackNotification : NotificationBase
{
    public FeedbackNotification(string content, Exception? exception = null)
        : base("FEEDBACK from xd.com", content, "requiem@gmail.com", "noreply@xd.com", exception)
    {

    }
}

public class FeedbackNotificationHandler : NotificationHandlerBase<FeedbackNotification>
{
    public FeedbackNotificationHandler(IUnitOfWork unitOfWork, ILogger<FeedbackNotification> logger)
        : base(unitOfWork, logger)
    {
        
    }
}