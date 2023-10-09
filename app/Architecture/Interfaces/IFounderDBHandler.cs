using app.Architecture.DataModel;
using app.Architecture.Enums;
using app.Models;

namespace app.Architecture.Interfaces;

public interface IFounderDBHandler
{
    public Task<(AddFounderStatus Status, Exception? Exception)> AddFounder(AddFounderModel model);
}