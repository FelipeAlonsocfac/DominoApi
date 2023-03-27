using Domino.Api.Application.Interfaces;
using Domino.Api.Application.UseCases;
using Domino.Api.Core.Dtos;
using Domino.Api.Core.Dtos.User;
using Domino.Api.Core.Validators;
using Domino.Api.Infrastructure.DataAccess;
using Domino.Api.Infrastructure.Interfaces;
using Domino.Api.Infrastructure.Repository;
using FluentValidation;

namespace Domino.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependenceInjectionConfiguration(this IServiceCollection services)
    {
        #region Infrastructure
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRedisCache, RedisCache>();
        #endregion

        #region Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDominoService, DominoService>();
        #endregion

        #region Validators
        services.AddTransient<IValidator<CreateUserRequestDto>, CreateUserValidator>();
        services.AddTransient<IValidator<UserLogInDto>, UserLogInValidator>();
        #endregion
        return services;
    }
}