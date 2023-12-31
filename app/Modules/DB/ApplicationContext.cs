using app.Modules.DB.DataModels;
using Microsoft.EntityFrameworkCore;

namespace app.Modules.DB;

public class ApplicationContext : DbContext
{
    public DbSet<Client> Clients {get; set;} = null!;
    public DbSet<Founder> Founders {get; set;} = null!;
    public DbSet<ClientFounderRelationship> ClientFounderRelationship {get; set;} = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    :base(options)
    {
        Database.EnsureCreated();
    }
}