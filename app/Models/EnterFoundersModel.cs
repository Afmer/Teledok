using System.ComponentModel.DataAnnotations;
using app.Architecture.ValidationAttributes;
namespace app.Models;
public class EnterFoundersModel
{
    [Required]
    public AddClientModel Client {get; set;} = null!;
    [Required]
    [ArrayINNLength]
    public string[] FounderINNs {get; set;} = null!;
}