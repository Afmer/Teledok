using app.Architecture.Interfaces;
using app.Modules.DB;

namespace app.Modules.Services;

public class DbManagerService : IDBManager
{
    private ApplicationContext _context;
    public DbManagerService(ApplicationContext context)
    {
        _context = context;
    }
}