using Microsoft.EntityFrameworkCore;
using projectef.Models;
using System;

namespace projectef;

public class TareasContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("cf1830e8-5dab-4429-8a91-152c5540fce7"), Nombre = "Actividades pendientes", Peso = 20, Description = "asdasd" });
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("c6e5b805-815f-4037-940b-a1cae2056dbb"), Nombre = "Actividades personales", Peso = 50, Description = "asdasd" });
        modelBuilder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);

            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

            categoria.Property(p => p.Description).IsRequired(false);

            categoria.Property(p => p.Peso);

            categoria.HasData(categoriasInit);
        });


        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("1572fc88-12cf-4c56-b424-8236ff630006"), CategoriaId = Guid.Parse("cf1830e8-5dab-4429-8a91-152c5540fce7"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios publicos", FechaCreacion = DateTime.Now, Description = "asdasd" });
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("1572fc88-12cf-4c56-b424-8236ff630007"), CategoriaId = Guid.Parse("c6e5b805-815f-4037-940b-a1cae2056dbb"), PrioridadTarea = Prioridad.Alta, Titulo = "Terminar de ver Pelicula", FechaCreacion = DateTime.Now, Description = "asdasd" });

        modelBuilder.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);

            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(p => p.Description).IsRequired(false);

            tarea.Property(p => p.PrioridadTarea);

            tarea.Property(p => p.FechaCreacion);

            tarea.Ignore(p => p.Resumen);

            tarea.HasData(tareasInit);
        });
    }
}