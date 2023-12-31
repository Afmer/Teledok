using System.ComponentModel.DataAnnotations;
using app.Architecture.Enums;
using app.Architecture.Interfaces;
using app.Models;
using app.Modules.DB;
using app.Modules.DB.DataModels;

namespace app.Modules.Services;

public class DbManagerService : IDBManager
{
    private ApplicationContext _context;
    public DbManagerService(ApplicationContext context)
    {
        _context = context;
    }
    private async Task<(bool Success, Exception? Exception)> ExecuteInTransaction(Func<Task> func)
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
    public async Task<(AddFounderStatus Status, Exception? Exception)> AddFounder(AddFounderModel model)
    {
        AddFounderStatus status = AddFounderStatus.Success;
        var result = await ExecuteInTransaction(async () => {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            if(Validator.TryValidateObject(model, context, results, true))
            {
                bool isInnExists = _context.Founders.Where(x => x.INN == model.INN).Count() != 0;
                if(isInnExists)
                {
                    status = AddFounderStatus.INNExistsError;
                    throw new Exception("Inn exists");
                }
                var time = DateTime.UtcNow;
                var founder = new Founder()
                {
                    AddDateTime = time,
                    UpdateDateTime = time,
                    Patronymic = model.Patronymic,
                    Surname = model.Surname,
                    Name = model.Name,
                    INN = model.INN
                };
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
        if(status == AddFounderStatus.Success && !result.Success)
            return (AddFounderStatus.UnknownError, result.Exception);
        else
            return (status, result.Exception);
    }

    public async Task<(AddClientStatus Status, Exception? Exception)> AddClient(AddClientModel model)
    {
        AddClientStatus status = AddClientStatus.Success;
        var result = await ExecuteInTransaction(async () => {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            if(Validator.TryValidateObject(model, context, results, true))
            {
                bool isINNExists = _context.Clients.Where(x => x.INN == model.ClientINN).Count() != 0;
                if(isINNExists)
                {
                    status = AddClientStatus.INNExistsError;
                    throw new Exception("INN exists");
                }
                if(model.ClientType == ClientType.LegalEntity)
                {
                    if(model.FounderINNs != null)
                    {
                        var foundersCount = _context.Founders
                            .Where(entity => model.FounderINNs.Contains(entity.INN))
                            .Count();
                        if(foundersCount != model.FounderINNs.Length)
                        {
                            status = AddClientStatus.FounderINNsError;
                            throw new Exception("Some founders were not found");
                        }
                    }
                    else
                    {
                        status = AddClientStatus.InvalidFormError;
                        throw new Exception("Founder's INN not found in form");
                    }
                }
                var addDateTime = DateTime.UtcNow;
                var client = new Client()
                {
                    INN = model.ClientINN,
                    Name = model.ClientName,
                    Type = model.ClientType,
                    AddDateTime = addDateTime,
                    UpdateDateTime = addDateTime
                };
                await _context.Clients.AddAsync(client);
                if(model.ClientType == ClientType.LegalEntity && model.FounderINNs != null)
                {
                    var clientFounderRelationships = new ClientFounderRelationship[model.FounderINNs.Length];
                    for(int i = 0; i < clientFounderRelationships.Length; i++)
                    {
                        clientFounderRelationships[i] = new ClientFounderRelationship(){
                            ClientId = model.ClientINN,
                            FounderId = model.FounderINNs[i]
                        };
                    }
                    await _context.ClientFounderRelationship.AddRangeAsync(clientFounderRelationships);
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                string errorMessage = "";
                foreach(var error in results)
                {
                    errorMessage += error.ErrorMessage + '\n';
                }
                status = AddClientStatus.InvalidFormError;
                throw new Exception(errorMessage);
            }
        });
        if(!result.Success && status == AddClientStatus.Success)
            return (AddClientStatus.UnknownError, result.Exception);
        else
            return (status, result.Exception);
    }
    public bool[] IsFounderINNsExists(string[] founderINNs)
    {
        var result = Enumerable.Repeat(false, founderINNs.Length).ToArray();
        var dict = new Dictionary<string, int>();
        for(int i = 0; i < founderINNs.Length; i++)
            dict.Add(founderINNs[i], i);
        var existINNs = _context.Founders
            .Where(x => founderINNs
            .Contains(x.INN))
            .Select(x => x.INN)
            .ToArray();
        foreach(var inn in existINNs)
            result[dict[inn]] = true;
        return result;
    }

    public ClientInfoModel? GetClient(string inn)
    {
        var client = _context.Clients.FirstOrDefault(x => x.INN == inn);
        if(client == null)
            return null;
        var founders = _context.ClientFounderRelationship
            .Where(x => x.ClientId == client.INN)
            .Select(x => x.Founder)
            .ToArray();
        FounderInfoModel[]? convertedFounders = null;
        if(founders.Length != 0)
        {
            convertedFounders = new FounderInfoModel[founders.Length];
            for(int i = 0; i < convertedFounders.Length; i++)
            {
                convertedFounders[i] = new(
                    founders[i].INN,
                    founders[i].Name,
                    founders[i].Surname,
                    founders[i].Patronymic,
                    founders[i].AddDateTime,
                    founders[i].UpdateDateTime
                );
            }
        }
        var model = new ClientInfoModel(
            client.INN,
            client.Name,
            client.Type,
            client.AddDateTime,
            client.UpdateDateTime,
            convertedFounders
        );
        return model;
    }

    public ShowClientModel GetAllClients()
    {
        var model = new ShowClientModel();
        var clients = _context.Clients.Select(x => new {x.INN, x.Name}).ToArray();
        model.Clients = new ClientBriefInfo[clients.Length];
        for(int i = 0; i < model.Clients.Length; i++)
        {
            model.Clients[i] = new ClientBriefInfo(
                clients[i].INN,
                clients[i].Name
            );
        }
        return model;
    }
}