using System.ComponentModel.DataAnnotations;

namespace VKprofileTaskAPI.Models;

public class UserGroupVariations
{
    [Key]
    public int? CodeId { get; set; }
    public string Type { get; set; }
}
