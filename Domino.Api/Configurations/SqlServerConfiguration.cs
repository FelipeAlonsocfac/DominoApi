using Domino.Api.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Domino.Api.Configurations;

public static class SqlServerConfiguration
{
    public static WebApplicationBuilder AddSqlServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DominoDBContext>(
            options => options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionString-Shared"))
            );

        return builder;
    }
}
