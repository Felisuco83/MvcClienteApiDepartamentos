using ApiEmpleadosOAuth.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcClienteApiDepartamentos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClienteApiDepartamentos.Controllers
{
    public class EmpleadosController : Controller
    {
        ServiceEmpleados ApiService;

        public EmpleadosController(ServiceEmpleados apiservice)
        {
            this.ApiService = apiservice;
        }
        [EmpleadoAuthorize]
        public async Task<IActionResult> PerfilEmpleado()
        {
            string token = HttpContext.Session.GetString("TOKEN");
            return View(await this.ApiService.GetPerfil(token));
        }

        [EmpleadoAuthorize]
        public async Task<IActionResult> Subordinados ()
        {
            string token = HttpContext.Session.GetString("TOKEN");
            return View(await this.ApiService.GetSubordinados(token));
        }
    }
}
