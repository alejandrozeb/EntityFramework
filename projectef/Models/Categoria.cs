namespace projectef.Models;
using System.ComponentModel.DataAnnotations;
public class Categoria
{
    public Guid CategoriaId {get;set;}

    public string Nombre {get;set;}

    public string Description {get;set;}

    public virtual ICollection<Tarea> Tareas {get;set;}
}