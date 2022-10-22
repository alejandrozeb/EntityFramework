namespace projectef.Models;

public class Tarea
{
    public Guid TareaId {get;set;}
    public Guid CategoriaId {get;set;}
    public string Titulo {get;set;}
    public string Description {get;set;}
    public Prioridad PrioridadTarea {get;set;}
    public DateTime FechaCreacion {get;set;}

    public virtual Categoria Categoria {get;set;}
}

public enum Prioridad
{
    Baja,
    Media,
    Alta
}