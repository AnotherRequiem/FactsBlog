namespace Requiem.Facts.Web.Infrastructure;
public static class AppData
{
    public const string AdministratorRoleName = "Administrator";
    public const string UserRoleName = "User";
    public static IEnumerable<string> Roles
    {
        get
        {
            yield return AdministratorRoleName;
            yield return UserRoleName;
        }
    }
}