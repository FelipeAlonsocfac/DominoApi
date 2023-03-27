using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Domino.Api.Configurations;

public static class KeyVaultConfiguration
{
    public static WebApplicationBuilder AddAzureKeyVault(this WebApplicationBuilder builder)
    {
        var azureCredential = new ClientSecretCredential(
        tenantId: builder.Configuration["AzureKeyVault:TenantId"],
        clientId: builder.Configuration["AzureKeyVault:ClientId"],
        clientSecret: builder.Configuration["AzureKeyVault:ClientSecret"]);

        var secretClient = new SecretClient(
            vaultUri: new Uri($"https://{builder.Configuration["AzureKeyVault:VaultName"]}.vault.azure.net/"),
            credential: azureCredential);

        builder.Configuration.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions() { ReloadInterval = TimeSpan.FromMinutes(5) });

        return builder;
    }
}
