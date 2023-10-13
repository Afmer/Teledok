using app.Architecture.Enums;

namespace app.Models;

public class ClientInfoModel
{
    public readonly string INN;
    public readonly string Name;
    public readonly ClientType Type;
    public readonly DateTime AddDateTime;
    public readonly DateTime UpdateDateTime;
    public readonly FounderInfoModel[]? FounderINNs;
    public ClientInfoModel(string inn, string name, ClientType type, DateTime addTime, DateTime updateTime, FounderInfoModel[]? founderINNs = null)
    {
        if(type == ClientType.LegalEntity && (founderINNs == null || founderINNs.Length == 0))
            throw new Exception("Legal entity must be with founder(s)");
        INN = inn;
        Name = name;
        Type = type;
        AddDateTime = addTime;
        UpdateDateTime = updateTime;
        FounderINNs = founderINNs;
    }
}