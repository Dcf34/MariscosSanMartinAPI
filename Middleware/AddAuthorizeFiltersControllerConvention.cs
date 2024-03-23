using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace MariscosSanMartinAPI.Middleware
{
    public class AddAuthorizeFiltersControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            //Se verifica que el controller Autenticacion quede expuesto sin autenticación alguna
            //Se verifica que si es ambiente de QA, tampoco se aplique autenticación
            if (!controller.ControllerName.Contains("Autenticacion") && JsonConfiguration.ObtenerAmbiente() != "QA")
            {
                controller.Filters.Add(new AuthorizeFilter());
            }
        }
    }
}
