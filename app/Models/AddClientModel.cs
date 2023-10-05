using System.ComponentModel.DataAnnotations;
using app.Architecture.DataModel;

namespace app.Models;

public class AddClientModel
{
    [Required]
    public Client Client {get; set;} = null!;
    public Founder[] Founders {get; set;} = null!;
}