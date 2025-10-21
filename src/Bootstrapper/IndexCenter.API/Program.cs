using Carter;
using Serilog;
using Shared.Exceptions.Handler;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

// Add services to the container

builder.Services.AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

app.UseCatalogModule();
app.UseBasketModule();
app.UseOrderingModule();

app.Run();