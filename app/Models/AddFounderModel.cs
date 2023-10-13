using System.ComponentModel.DataAnnotations;

namespace app.Models;

public class AddFounderModel
{
    [Required(ErrorMessage = "Поле ИНН не может быть пустым")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Длина ИНН должна быть 10")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Поле должно содержать ровно 10 цифр.")]
    public string INN {get; set;} = null!;
    [Required(ErrorMessage = "Имя обязательно")]
    [MaxLength(50)]
    public string Name {get; set;} = null!;
    [Required(ErrorMessage = "Фамилия обязательна")]
    [MaxLength(50)]
    public string Surname {get; set;} = null!;
    [MaxLength(50)]
    [Required(ErrorMessage = "Отчество обязательно")]
    public string Patronymic {get; set;} = null!;
}