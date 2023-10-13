namespace app.Models;

public class ClientBriefInfo
{
    public readonly string INN;
    public readonly string Name;
    public ClientBriefInfo(string inn, string name)
    {
        INN = inn;
        Name = name;
    }
}