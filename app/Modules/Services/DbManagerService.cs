using System.ComponentModel.DataAnnotations;
using app.Architecture.DataModel;
using app.Architecture.Interfaces;
using app.Modules.DB;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace app.Modules.Services;

public class DbManagerService : IDBManager
{
    private ApplicationContext _context;
    public DbManagerService(ApplicationContext context)
    {
        _context = context;
    }
    private async Task<(bool Success, Exception Exception)> ExecuteInTransaction(Func<Task> func)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await func.Invoke();
            await transaction.CommitAsync();
            return (true, null!);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return (false, e);
        }
    }
    public async Task<(bool Success, Exception? Exception)> AddFounder(Founder founder)
    {
        var result = await ExecuteInTransaction(async () => {
            var context = new ValidationContext(founder, null, null);
            var results = new List<ValidationResult>();
            if(Validator.TryValidateObject(founder, context, results, true))
            {
                await _context.Founders.AddAsync(founder);
                await _context.SaveChangesAsync();
            }
            else
            {
                string errorMessage = "";
                foreach(var error in results)
                {
                    errorMessage += error.ErrorMessage + '\n';
                }
                throw new Exception(errorMessage);
            }
        });
        return (result.Success, result.Exception);
    }
}