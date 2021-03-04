using MvcClienteApiDepartamentos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcClienteApiDepartamentos.Services
{
    public class ServiceDepartamentos
    {
        Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceDepartamentos(string url)
        {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApi<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
           string request = "api/departamentos";
            return await this.CallApi<List<Departamento>>(request);
        }

        public async Task<Departamento> BuscarDepartamentoAsync(int id)
        {
           string request = "api/departamentos/" + id;
            return await this.CallApi<Departamento>(request);
        }

        public async Task DeleteDepartamentoAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos/" + id;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task InsertarDepartamentoAsync (int id, string nombre, string localidad)
        {
            Departamento dept = new Departamento();
            dept.IdDepartamento = id;
            dept.Nombre = nombre;
            dept.Localidad = localidad;
            string json = JsonConvert.SerializeObject(dept);
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                StringContent content = new StringContent(json,Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task ModificarDepartamentoAsync(int id, string nombre, string localidad)
        {
            Departamento dept = new Departamento();
            dept.IdDepartamento = id;
            dept.Nombre = nombre;
            dept.Localidad = localidad;
            string json = JsonConvert.SerializeObject(dept);
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }
    }
}
