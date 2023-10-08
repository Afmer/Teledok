using app.Architecture.Enums;
using app.Models;

namespace app.Architecture.Interfaces;

public interface IClientDBHandler
{
    public Task<(AddClientStatus Status, Exception? Exception)> AddClient(AddClientModel model);
}