using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços necessários
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PipeClinic API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Habilitar o Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PipeClinic API v1");
        c.RoutePrefix = ""; // Define a rota raiz para o Swagger UI
    });
}


// Redirecionamento HTTPS e Autorização
app.UseHttpsRedirection();
app.UseAuthorization();

// Mapeamento dos Controllers
app.MapControllers();

app.Run();
