namespace VKprofileTaskAPI.Models;

public class User
{
    public long UserId { get; set; }
    public string Login { get; set; } = null!;
    public DateTime CreatedData { get; set; }
    public UserGroup CurrentUserGroup { get; set; } = null!;
    public UserState CurrentUserState { get; set; } = null!;
 
}
