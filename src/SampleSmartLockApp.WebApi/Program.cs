var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorizationBuilder();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

// Add Layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

// Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();


builder.Services.AddScoped<IAuthorizationHandler, TestAuthorizeAttributeHandler>();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddControllers();


var app = builder.Build();
SeedInitializer.Initialize(app).GetAwaiter();

app.MapGroup("/api/Account")
    .MapIdentityApi<ApplicationUser>()
    .WithTags("Account");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();