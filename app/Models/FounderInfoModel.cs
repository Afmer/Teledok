namespace app.Models;

public class FounderInfoModel
{
    public readonly string INN;
    public readonly string Name;
    public readonly string Surname;
    public readonly string Patronymic;
    public readonly DateTime AddDateTime;
    public readonly DateTime UpdateDateTime;

    public FounderInfoModel(string inn, string name, string surname, string patronymic, DateTime addDateTime, DateTime updateDateTime)
    {
        INN = inn;
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        AddDateTime = addDateTime;
        UpdateDateTime = updateDateTime;
    }
}