using Requiem.Facts.Web.Data;
using Requiem.Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper;

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
        services.AddWebOptimizer(pipeline =>
        {
            pipeline.MinifyCssFiles("css/**/*.css");
        });
        services.AddControllersWithViews();

        // dependency injection
        services.AddTransient<IPagerTagHelperService, PagerTagHelperService>();
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
                pattern: "{controller=Facts}/{action=Index}/{tag:regex([a-z�-�])}/{search:regex([a-z�-�])}/{pageIndex:int?}");

            endpoints.MapControllerRoute(
                name: "index",
                pattern: "{controller=Facts}/{action=Index}/{tag:regex([a-z�-�])}/{pageIndex:int?}");

            endpoints.MapControllerRoute(
                name: "index",
                pattern: "{controller=Facts}/{action=Index}/{pageIndex:int?}");
          
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Facts}/{action=Index}/{id?}");


            endpoints.MapRazorPages();
        });
        
    }
}