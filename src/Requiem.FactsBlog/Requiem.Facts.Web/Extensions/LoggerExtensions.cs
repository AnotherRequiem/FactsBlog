namespace Requiem.Facts.Web.Extensions;

static class EventIdentifiers
{
    public static readonly EventId DatabasesavingErrorId = new (70040001, "DatabaseSavingError");
    public static readonly EventId NotificationAddedId = new (70040002, "NotificationAdded");
}

public static class LoggerExtensions
{
    public static void NotificationAdded(this ILogger source, string entityName, Exception? exception = null)
    {
        NotificationAddedExecute(source, entityName, exception);
    }

    private static readonly Action<ILogger, string, Exception?> NotificationAddedExecute =
        LoggerMessage.Define<string>(LogLevel.Information, EventIdentifiers.NotificationAddedId, "New notification created: {subject}");
 
    public static void DatabaseSavingError(this ILogger source, string entityName, Exception? exception = null)
    {
        DatabaseSavingErrorExecute(source, entityName, exception);
    }

    private static readonly Action<ILogger, string, Exception?> DatabaseSavingErrorExecute =
        LoggerMessage.Define<string>(LogLevel.Error, EventIdentifiers.DatabasesavingErrorId, "{entityName}");
}