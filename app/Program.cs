using app.Architecture.Interfaces;
using app.Modules.DB;
using app.Modules.Services;
using Microsoft.EntityFrameworkCore;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContextPool<ApplicationContext>(options => options
                .UseNpgsql(
                    builder.Configuration.GetConnectionString("PostgreSQLConnectionString")
                ));
        RegisterDependency(builder);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Client}/{action=Add}/{id?}");

        app.Run();
    }

    private static void RegisterDependency(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDBManager, DbManagerService>();
        builder.Services.AddScoped<IFounderDBHandler, DbManagerService>();
    }
}
