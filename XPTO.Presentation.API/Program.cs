using XPTO.Application;
using XPTO.Domain;
using XPTO.Infrastructure;
using XPTO.Presentation.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLayerDomain();
builder.Services.AddLayerApplication();
builder.Services.AddLayerInfrastructure();

builder.AddWebApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddWebApiConfig();

var app = builder.Build();

app.UseApiConfig();

app.UseSwaggerConfiguration();

app.Run();
