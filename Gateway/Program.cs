using Gateway.Presentation.Api.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();


builder.Services.AddControllers();

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