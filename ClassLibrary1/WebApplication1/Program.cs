using ClassLibrary1;
using WebApplication1.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
ClassLibrary1.Class1 con = new ClassLibrary1.Class1(); 
con.connect(true);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWebSockets();                            // Aceptar WebSockets
app.Map("/ws", b => {                           //Mapping the ws route
    b.UseMiddleware<MiControladorDeWebSockets>();  // Controlador para WebSockets
});

app.MapControllers();

app.Run();
