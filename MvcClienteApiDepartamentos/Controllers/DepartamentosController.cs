using Microsoft.AspNetCore.Mvc;
using MvcClienteApiDepartamentos.Models;
using MvcClienteApiDepartamentos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClienteApiDepartamentos.Controllers
{
    public class DepartamentosController : Controller
    {
        ServiceDepartamentos ServiceApi;
        public DepartamentosController(ServiceDepartamentos serviceapi)
        {
            this.ServiceApi = serviceapi;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListDepartamentos()
        {
            return View(await this.ServiceApi.GetDepartamentosAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await this.ServiceApi.BuscarDepartamentoAsync(id));
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await this.ServiceApi.BuscarDepartamentoAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Departamento dept)
        {
            await this.ServiceApi.ModificarDepartamentoAsync(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return RedirectToAction("ListDepartamentos");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Departamento dept)
        {
            await this.ServiceApi.InsertarDepartamentoAsync(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return RedirectToAction("ListDepartamentos");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.ServiceApi.DeleteDepartamentoAsync(id);
            return RedirectToAction("ListDepartamentos");
        }

        public IActionResult ApiClienteAjax()
        {
            return View();
        }

    }
}
