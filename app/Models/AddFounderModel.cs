using System.ComponentModel.DataAnnotations;

namespace app.Models;

public class AddFounderModel
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