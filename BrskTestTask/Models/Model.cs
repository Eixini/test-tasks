using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrskTestTask.Models;

[Table("Models")]
public class Model
{
    [Key]
    public int ModelId { get; set; }
    [Required]
    public string? Name { get; set; }
    public bool Active { get; set; }

    [ForeignKey(nameof(BrandId))]
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }
}
