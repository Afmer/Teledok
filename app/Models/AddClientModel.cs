using System.ComponentModel.DataAnnotations;
using app.Architecture.Enums;
using app.Architecture.ValidationAttributes;

namespace app.Models;

public class AddClientModel
{
    [Required(ErrorMessage = "Поле ИНН не может быть пустым")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Длина ИНН должна быть 10")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Поле должно содержать ровно 10 цифр.")]
    [Display(Name = "ИНН Клиента")]
    public string ClientINN {get; set;} = null!;
    [Required(ErrorMessage = "Поле клиента не может быть пустым")]
    [MaxLength(100)]
    public string ClientName {get; set;} = null!;
    [Required]
    public ClientType ClientType {get; set;}
    [Range(0, int.MaxValue, ErrorMessage = "Значение должно быть неотрицательным.")]
    public int? FoundersCount {get; set;}
    [ArrayINNLength]
    public string[]? FounderINNs {get; set;} = null!;
    
}