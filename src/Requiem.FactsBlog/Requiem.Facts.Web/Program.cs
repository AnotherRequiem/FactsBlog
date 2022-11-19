namespace Requiem.Facts.Web;

public class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}