using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrskTestTask.Models;

[Table("Brands")]
public class Brand
{
    [Key]
    public int BrandId { get; set; }
    [Required]
    public string? Name { get; set; }
    public bool Active { get; set; }

    [InverseProperty(nameof(Model.Brand))]
    public ICollection<Model>? Models { get; set; }
}
