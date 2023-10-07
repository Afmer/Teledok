using app.Architecture.DataModel;

namespace app.Architecture.Interfaces;

public interface IFounderDBHandler
{
    public Task<(bool Success, Exception? Exception)> AddFounder(Founder founder);
}