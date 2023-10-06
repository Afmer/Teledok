using System.ComponentModel.DataAnnotations;
using app.Architecture.DataModel;
using app.Architecture.Enums;

namespace app.Models;

public class AddClientModel
{
    [Required]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Длина ИНН должна быть 10")]
    [Display(Name = "ИНН Клиента")]
    public string ClientINN {get; set;} = null!;
    [Required]
    [MaxLength(100)]
    public string ClientName {get; set;} = null!;
    [Required]
    public ClientType ClientType {get; set;}
    public FounderInfo[] Founders {get; set;} = null!;
}

public class FounderInfo
{
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string INN {get; set;} = null!;
    [Required]
    [MaxLength(50)]
    public string Name {get; set;} = null!;
    [Required]
    [MaxLength(50)]
    public string Surname {get; set;} = null!;
    [MaxLength(50)]
    public string Patronymic {get; set;} = null!;
}