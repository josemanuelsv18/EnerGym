var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
<<<<<<< HEAD
        policy.AllowAnyOrigin()       // Permite cualquier origen (puedes restringirlo a un origen específico como 'http://localhost:3000')
              .AllowAnyMethod()       // Permite cualquier método HTTP (GET, POST, etc.)
              .AllowAnyHeader();      // Permite cualquier encabezado
=======
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
>>>>>>> b368856695b2535c0df99f2fb9b90e4e5f1042cd
    });
});

var app = builder.Build();

<<<<<<< HEAD
// Configurar la tubería de solicitudes HTTP.
app.UseCors("AllowAll"); // Aplicar la política de CORS
=======
// Configurar la tuber�a de solicitudes HTTP.
app.UseCors("AllowAll"); // Aplicar la pol�tica de CORS
>>>>>>> b368856695b2535c0df99f2fb9b90e4e5f1042cd

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();