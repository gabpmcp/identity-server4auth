using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

var builder = WebApplication.CreateBuilder(args);

// Configura IdentityServer con lambdas
builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    ])
    .AddInMemoryApiScopes(
    [
        new ApiScope("api1", "My API")
    ])
    .AddInMemoryClients(
    [
        new Client
        {
            ClientId = "client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { "api1" }
        }
    ])
    .AddTestUsers(
    [
        new TestUser { SubjectId = "1", Username = "alice", Password = "password" },
        new TestUser { SubjectId = "2", Username = "bob", Password = "password" }
    ]);

var app = builder.Build();

app.UseIdentityServer();
app.Run();
