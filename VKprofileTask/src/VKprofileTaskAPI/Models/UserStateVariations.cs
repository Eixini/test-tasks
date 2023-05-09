using System.ComponentModel.DataAnnotations;

namespace VKprofileTaskAPI.Models;

public class UserStateVariations
{
    [Key]
    public int CodeId { get; set; }
    public string Type { get; set; }
}
