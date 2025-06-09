using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using KiraApi2;
using KiraShopApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KiraApiDbContext>(options =>
    options.UseMySql("server=localhost;user id=root;password=;database=kira2db",
        ServerVersion.AutoDetect("server=localhost;user id=root;password=;database=kira2db")
    )
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Registrar o serviço de autenticação
builder.Services.AddScoped<AuthService>();

// Adicionando configuração de CORS para permitir requisições do front-end
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Porta padrão do Vite para desenvolvimento
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usando a política de CORS
app.UseCors("AllowVueApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

