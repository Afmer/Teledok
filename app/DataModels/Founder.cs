using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace app.DataModel;
[PrimaryKey(nameof(INN))]
public class Founder
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
    [Required]
    public DateTime AddDateTime {get; set;}
    [Required]
    public DateTime UpdateDateTime {get; set;}
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string LegalEntityId {get; set;} = null!;
    public Client LegalEntity {get; set;} = null!;
}