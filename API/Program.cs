using Bussines;
using DataAccess;
using DataAcess;
using Microsoft.EntityFrameworkCore;
using Shared;
using FluentValidation;
using Entities.DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:5173",
                    "https://localhost:5173"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

// Registrando Services e Repositórios
builder.Services.AddScoped<IAvisoService, AvisoService>();
builder.Services.AddScoped<IAvisoRepository, AvisoRepository>();

// Registrando validadores do FluentValidation
builder.Services.AddScoped<CreateAvisoValidator>();
builder.Services.AddScoped<UpdateAvisoValidator>();
builder.Services.AddScoped<GetAvisoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// CORS deve vir AQUI
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
