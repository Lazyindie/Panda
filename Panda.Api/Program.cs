using Panda.Api.Configurations;
using Panda.Library.Class.Common;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorizationBuilder()
    // Add services to the container.
    .AddDefaultPolicy(CorsProfiles.Default, policy =>
    {
        // Do not need authentication, as used in a trusted environment
        // Here we could add some user profile requirements such as:
        //  - Require authenticated user => policy.RequireAuthenticatedUser();
        //  - Require specific roles => policy.RequireRole("Admin", "User");
        policy.RequireClaim("scope", "panda.api");
    });

// Register services for dependency injection.
ServiceConfiguration.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Enforce HTTPS in production.
else
{
    app.UseHsts();
}

// Add a default authorization policy.
app.UseCors(CorsProfiles.Default);

// Always use HTTPS.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
