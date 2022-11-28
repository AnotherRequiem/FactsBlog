using Calabonga.AspNetCore.Controllers.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Requiem.Facts.Web.Data;
using Serilog;
using Serilog.Events;

namespace Requiem.Facts.Web;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public void ConfigureServices(IServiceCollection services)
	{
        var connectionString = Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException
            ("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddUnitOfWork<ApplicationDbContext>();
        services.AddDefaultIdentity<IdentityUser> (options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAutoMapper(typeof(Startup));
        services.AddCommandAndQueries(typeof(Startup).Assembly);
        services.AddWebOptimizer();
        services.AddControllersWithViews(); 
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Site/Error");
            
            app.UseHsts();
        }

        app.UseWebOptimizer();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "index",
                pattern: "{controller=Site}/{action=Index}/{tag:regex([a-z¿-ﬂ])}/{search:regex([a-z¿-ﬂ])}/{pageIndex:int?}");

            endpoints.MapControllerRoute(
                name: "index",
                pattern: "{controller=Site}/{action=Index}/{tag:regex([a-z¿-ﬂ])}/{pageIndex:int?}");

            endpoints.MapControllerRoute(
                name: "index",
                pattern: "{controller=Site}/{action=Index}/{pageIndex:int?}");
          
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Site}/{action=Index}/{id?}");


            endpoints.MapRazorPages();
        });
        
    }
}