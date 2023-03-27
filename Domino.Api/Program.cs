using Domino.Api.Configurations;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddAzureKeyVault();
builder.AddSqlServer();

builder.Services.AddAuthenticationConfiguration(builder.Configuration["JWTSecret"]!);
builder.Services.AddDependenceInjectionConfiguration();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddApiVersioning();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddStackExchangeRedisCache(setupAction =>
{
    setupAction.Configuration = builder.Configuration["RedisConnection"]!;
});

builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy(
        name: "AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    }
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.AddWebApplicationConfiguration();

app.Run();