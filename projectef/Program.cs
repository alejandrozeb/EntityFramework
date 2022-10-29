using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;
using projectef.Models;

var builder = WebApplication.CreateBuilder(args);


// builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria" + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria).Where(p => p.PrioridadTarea == projectef.Models.Prioridad.Alta));
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await dbContext.AddAsync(tarea);
    // await dbContext.Tareas.AddAsync(tarea);
    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{
    var tareaActual = dbContext.Tareas.Find(id);

    if (tareaActual != null)
    {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Description = tarea.Description;
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) =>
{
    var tareaActual = dbContext.Tareas.Find(id);

    if (tareaActual != null)
    {
        dbContext.Tareas.Remove(tareaActual);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();
});

app.Run();

/* 
 * dotnet new web
 dotnet add package Microsoft.EntityFrameworkCore --version 6.0.3
dotnet tool install --local dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.5
dotnet ef migrations add InitialCreate
dotnet ef database update
 */