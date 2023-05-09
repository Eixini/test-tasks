using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKprofileTaskAPI.Models;

public class UserState
{
    [Key]
    public long UserStateId { get; set; }
    [NotMapped]
    [ForeignKey("StateId")]
    public UserStateVariations Code { get; set; } = null!;
    public int StateId { get; set; }
    public string? Description { get; set;}
}