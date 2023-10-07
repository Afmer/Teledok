using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace app.Architecture.DataModel;

[PrimaryKey(nameof(Id))]
public class ClientFounderRelationship
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id {get; set;}
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string ClientId {get; set;} = null!;
    public Client Client {get; set;} = null!;
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string FounderId {get; set;} = null!;
    public Founder Founder {get; set;} = null!; 
}