using AutoMapper;
using BCrypt.Net;
using Domino.Api.Application.Interfaces;
using Domino.Api.Core.Dtos;
using Domino.Api.Core.Dtos.User;
using Domino.Api.Core.Entities;
using Domino.Api.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domino.Api.Application.UseCases;

public class UserService : IUserService
{
    private readonly IRepository<UserTable> _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(IRepository<UserTable> userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }
    public async Task<UserResponseDto?> SignUpAsync(CreateUserRequestDto oUser)
    {
        UserTable? userTask = await _userRepository.GetFirst(x => x.Email == oUser.Email);
        if (userTask != null) return null;

        oUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(oUser.Password, HashType.SHA256, 12);
        var aa = oUser.Password.Length;
        UserTable newUser = _mapper.Map<UserTable>(oUser);

        await _userRepository.InsertAsync(newUser);

        DateTime expiration = DateTime.UtcNow.AddHours(1);
        UserResponseDto result = _mapper.Map<UserResponseDto>(newUser);

        result.Token = BuildToken(result.Email, expiration);
        result.ExpirationDate = expiration;

        return result;
    }

    public async Task<UserResponseDto?> LogInAsync(UserLogInDto oUser)
    {
        UserTable? user = await _userRepository.GetFirst(x => x.Email == oUser.Email);
        if (user == null) return null;

        bool isValidPassword = BCrypt.Net.BCrypt.EnhancedVerify(oUser.Password, user.Password, HashType.SHA256);

        if (!isValidPassword) return null;

        DateTime expiration = DateTime.UtcNow.AddHours(1);
        UserResponseDto result = _mapper.Map<UserResponseDto>(user);
        
        result.Token = BuildToken(user.Email, expiration);
        result.ExpirationDate = expiration;
        
        return result;
    }

    private string BuildToken(string uniqueName, DateTime expiration) //TODO: wrap it into token service
    {
        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, uniqueName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        SymmetricSecurityKey key = new (Encoding.UTF8.GetBytes(_configuration["JWTSecret"]!));
        SigningCredentials creds = new (key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
           issuer: "domino-test-api.azurewebsites.net",
           audience: "domino-test-api.azurewebsites.net",
           claims: claims,
           expires: expiration,
           signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
