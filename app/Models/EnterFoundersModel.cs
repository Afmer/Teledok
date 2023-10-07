using System.ComponentModel.DataAnnotations;
using app.Architecture.ValidationAttributes;
namespace app.Models;
public class EnterFoundersModel
{
    [Required]
    public AddClientModel Client {get; set;} = null!;
    [Required]
    [ArrayStringLength(10, ErrorMessage = "ИНН должен быть длиной 10")]
    public string[] FounderINNs {get; set;} = null!;
}