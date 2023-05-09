using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VKprofileTaskAPI.DbContexts;
using VKprofileTaskAPI.Models;

namespace VKprofileTaskAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskApiController : Controller
{
    private readonly ILogger<TaskApiController> _logger;
    private readonly UsersDbContext _dbContext;

    public TaskApiController(ILogger<TaskApiController> logger, UsersDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Метод возвращает список всех пользователей
    /// </summary>
    /// <returns>Список всех пользователей</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.Users
                               .Include(u => u.CurrentUserGroup)
                               .Include(u => u.CurrentUserState)
                               .ToListAsync();
    }

    /// <summary>
    /// Метод возвращает пользователя по ID
    /// </summary>
    /// <param name="id">ID, по которому нужно найти пользователя</param>
    /// <returns>Если пользователь найден, то возвращает объект, иначе статус "Не найдено"</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetUser([FromBody]int id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var getUser = await _dbContext.Users
            .Include(u => u.CurrentUserGroup)
            .Include(u => u.CurrentUserState)
            .FirstOrDefaultAsync(i => i.UserId == id);

        if(getUser == null)
        {
            return NotFound();
        }

        return Ok(getUser);
    }

    /// <summary>
    /// Метод для создания нового пользователя
    /// </summary>
    /// <param name="user">Введенные данные на стороне клиента</param>
    /// <returns>Статус операции</returns>
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> CreateUser([FromBody]NewUserModel user)
    {
        if(user == null)
        {
            return BadRequest();
        }

        var newUser = new User
        {
            Login = user.Login,
            CreatedData = DateTime.Now,
            CurrentUserGroup = new UserGroup
            {
                Code = new UserGroupVariations { Type = "User" },
                Description = "Роль обычного пользователя"
            },
            CurrentUserState = new UserState
            {
                Code = new UserStateVariations { Type = "Active" },
                Description = "При создании пользователя, он помечается как зарегистрированный"
            }
        };

        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();

        return Ok($"Пользователь {user.Login} добавлен.");
    }

    /// <summary>
    /// Метод для удаления пользователя
    /// На самом деле пользователь не удаляется из базы данных,
    /// а просто получает статус "Заблокировано"
    /// </summary>
    /// <param name="id">ID пользователя, которого нужно удалить</param>
    /// <returns>Статус операции</returns>
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _dbContext.Users
                             .Include(u => u.CurrentUserGroup)
                             .Include(u => u.CurrentUserState)
                             .FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null)
        {
            return NotFound();
        }


        user.CurrentUserState.Code =  new UserStateVariations { Type = "Blocked" };

        return Ok("Пользователь удален");
    }


}
