using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

public class PersonaModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var request = bindingContext.HttpContext.Request;

        var nombre = request.Form["Nombre"].ToString();
        var cedula = request.Form["Cedula"].ToString();
        var edadTexto = request.Form["Edad"].ToString();

        int.TryParse(edadTexto, out int edad);

        var persona = new Persona
        {
            Nombre = nombre,
            Cedula = cedula,
            Edad = edad,
            Username = nombre.Replace(" ", "-").ToLower()
        };

        bindingContext.Result = ModelBindingResult.Success(persona);
        return Task.CompletedTask;
    }
}
