using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKprofileTaskAPI.Models;

public class UserGroup
{
    [Key]
    public long UserGroupId { get; set; }
    [NotMapped]
    [ForeignKey("GroupId")]
    public UserGroupVariations Code { get; set; } = null!;
    public int GroupId { get; set; }
    public string? Description { get; set; }
}
