using Domino.Api.Application.Interfaces;
using Domino.Api.Core.Dtos;
using Domino.Api.Core.Dtos.User;
using Domino.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Domino.Api.Controllers;

[ApiVersion("1.0")]
[Route("/v{version:apiVersion}/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// This method Registers a new user in the database.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("Register")]
    [UserFilter]
    [ProducesResponseType(typeof(UserResponseDto), 200)]
    public async Task<IActionResult> SignUp([FromBody] CreateUserRequestDto model)
    {
        var result = await _userService.SignUpAsync(model);
        
        return Ok(result);
    }

    /// <summary>
    /// This method tries to login a user in database.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [UserFilter]
    [ProducesResponseType(typeof(UserResponseDto), 200)]
    public async Task<IActionResult> LogIn([FromBody] UserLogInDto user)
    {
        var result = await _userService.LogInAsync(user);

        return Ok(result);
    }
}
