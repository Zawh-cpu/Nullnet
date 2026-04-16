using System.Reflection.Metadata;
using Database.Application.Abstractions.Persistence;
using Database.Application.Factories;
using Database.Infrastructure.Data;
using Database.Infrastructure.Repositories;
using Database.Presentation.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var envConn = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
Console.WriteLine(envConn);
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));

var connectionString = !string.IsNullOrWhiteSpace(envConn)
    ? envConn
    : builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleAssignmentRepository, RoleAssignmentRepository>();
builder.Services.AddScoped<IServerRepository, ServerRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();

builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(
        new OptionalFieldJsonConverterFactory());
});;

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] =
            context.HttpContext.TraceIdentifier;
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseReDoc(options => options.SpecUrl("/openapi/v1.json"));
app.MapScalarApiReference();
app.MapOpenApi();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler();

Console.WriteLine(Random.Shared.GetHexString(64));

app.Run();
