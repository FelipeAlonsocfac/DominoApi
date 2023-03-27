using Domino.Api.Core.Dtos;
using Domino.Api.Core.Dtos.User;

namespace Domino.Api.Application.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Registers a user in the database.
    /// </summary>
    /// <param name="oUser">the user information to create</param>
    /// <returns>A <see cref="Task"/>&lt;<see cref="UserResponseDto"/>&gt; representing the asynchronous operation.</returns>
    Task<UserResponseDto?> SignUpAsync(CreateUserRequestDto oUser);

    /// <summary>
    /// Tries LogIn of a user in the database.<br/>
    /// If succesful, returns a jwtToken.
    /// </summary>
    /// <param name="oUser">the user information to create</param>
    /// <returns>A <see cref="Task"/>&lt;<see cref="UserResponseDto"/>&gt; representing the asynchronous operation.</returns>
    Task<UserResponseDto?> LogInAsync(UserLogInDto oUser);
}