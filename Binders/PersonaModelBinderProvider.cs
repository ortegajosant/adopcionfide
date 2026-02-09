using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class PersonaModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(Persona))
        {
            return new PersonaModelBinder();
        }

        return null;
    }
}
