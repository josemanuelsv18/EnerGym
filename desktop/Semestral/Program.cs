var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurar la tuber�a de solicitudes HTTP.
app.UseCors("AllowAll"); // Aplicar la pol�tica de CORS

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
