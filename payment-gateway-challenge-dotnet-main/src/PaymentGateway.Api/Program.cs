using FluentValidation;

using PaymentGateway.Api.Configuration;
using PaymentGateway.Api.ExceptionHandlers;
using PaymentGateway.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection(nameof(AppConfig)));

// I'd usually add Interfaces to the services and register them that way.
// However, with a small application like this it wouldn't give any benefit.
builder
    .Services
    .AddSingleton(TimeProvider.System)
    .AddSingleton<PaymentsRepository>()
    .AddSingleton<AcquiringBank>();

builder.Services.AddHttpClient<AcquiringBank>();

builder.Services
    .AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services
    .AddExceptionHandler<BadGatewayExceptionHandler>()
    .AddExceptionHandler<HttpRequestExceptionHandler>()
    .AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
