using System.ComponentModel.DataAnnotations;
using app.Architecture.Enums;
using Microsoft.EntityFrameworkCore;

namespace app.Architecture.DataModel;
[PrimaryKey(nameof(INN))]
public class Client
{
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string INN {get; set;} = null!;
    [Required]
    [MaxLength(100)]
    public string Name {get; set;} = null!;
    [Required]
    public ClientType Type {get; set;}
    [Required]
    public DateTime AddDateTime {get; set;}
    [Required]
    public DateTime UpdateDateTime {get; set;}
}