var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()       // Permite cualquier origen (puedes restringirlo a un origen específico como 'http://localhost:3000')
              .AllowAnyMethod()       // Permite cualquier método HTTP (GET, POST, etc.)
              .AllowAnyHeader();      // Permite cualquier encabezado
    });
});

var app = builder.Build();

// Configurar la tubería de solicitudes HTTP.
app.UseCors("AllowAll"); // Aplicar la política de CORS

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();