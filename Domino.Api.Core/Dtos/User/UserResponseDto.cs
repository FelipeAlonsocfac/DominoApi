﻿namespace Domino.Api.Core.Dtos.User;

public class UserResponseDto
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Token { get; set; } = "";
    public DateTime ExpirationDate { get; set; }
}
