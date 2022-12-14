namespace Requiem.Facts.Web.Data;

public class Notification: Auditable
{
    public Notification(string subject, string content, string addressFrom, string addressTo)
    {
        Subject = subject;
        Content = content;
        AddressFrom = addressFrom;
        AddressTo = addressTo;
    }
    
    public string Subject { get; set; }

    public string Content { get; set; }

    public bool IsCompleted { get; set;}

    public string AddressFrom { get; set; }

    public string AddressTo { get; set; }

}