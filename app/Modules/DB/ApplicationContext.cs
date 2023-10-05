using app.Architecture.DataModel;
using Microsoft.EntityFrameworkCore;

namespace app.Modules.DB;

public class ApplicationContext : DbContext
{
    public DbSet<Client> Clients {get; set;} = null!;
    public DbSet<Founder> Founders {get; set;} = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    :base(options)
    {
        Database.EnsureCreated();
    }
}