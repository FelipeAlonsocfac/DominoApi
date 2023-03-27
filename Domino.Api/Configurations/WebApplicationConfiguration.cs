namespace Domino.Api.Configurations;

public static class WebApplicationConfiguration
{
    public static WebApplication AddWebApplicationConfiguration(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        app.UsePathBase("/domino");

        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors("AllowAll");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
