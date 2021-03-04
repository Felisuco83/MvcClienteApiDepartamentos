using Microsoft.AspNetCore.Mvc;
using MvcClienteApiDepartamentos.Services;
using NuGetDoctoresModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClienteApiDepartamentos.Controllers
{
    public class DoctoresController : Controller
    {
        ServiceDoctores ServiceApi;
        public DoctoresController(ServiceDoctores serviceapi)
        {
            this.ServiceApi = serviceapi;
        }
        public async Task<IActionResult> Index()
        {
            return View(await this.ServiceApi.GetDoctoresAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int incremento, int hospital)
        {
            await this.ServiceApi.IncrementarSalariosAsync(incremento, hospital);
            return RedirectToAction("ListDoctores");
        }


        public async Task<IActionResult> Details(int id)
        {
            return View(await this.ServiceApi.GetDoctorAsync(id));
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await this.ServiceApi.GetDoctorAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doc)
        {
            await this.ServiceApi.UpdateDoctorAsync(doc.IdDoctor,doc.Apellido,doc.Especialidad, doc.Hospital, doc.Salario);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doc)
        {
            await this.ServiceApi.InsertDoctorAsync(doc.IdDoctor, doc.Apellido, doc.Especialidad, doc.Hospital, doc.Salario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.ServiceApi.DeleteDoctorAsync(id);
            return RedirectToAction("Index");
        }


    }
}
