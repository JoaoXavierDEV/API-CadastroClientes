using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using XPTO.Domain.Entities;
using XPTO.Infrastructure.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("xpto-database");
    options.UseSeeding((x, _) =>
    {
        var existeDados = x.Set<Cliente>().Any();

        if (!existeDados)
        {
            x.Set<Cliente>().AddRange(DbInitializer.Clientes);
            x.SaveChanges();
        }

    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
