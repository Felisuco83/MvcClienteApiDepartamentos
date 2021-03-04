using Newtonsoft.Json;
using NuGetDoctoresModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcClienteApiDepartamentos.Services
{
    public class ServiceDoctores
    {
        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceDoctores(string url)
        {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApi<T> (string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                } else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string request = "api/doctores";
            return await this.CallApi<List<Doctor>>(request);
        }

        public async Task<Doctor> GetDoctorAsync(int id)
        {
            string request = "api/doctores";
            return await this.CallApi<Doctor>(request);
        }

        private string GetDoctorJson(int id, string apellido, string especialidad, int hospital, int salario)
        {
            Doctor doc = new Doctor();
            doc.IdDoctor = id;
            doc.Apellido = apellido;
            doc.Especialidad = especialidad;
            doc.Hospital = hospital;
            doc.Salario = salario;
            return JsonConvert.SerializeObject(doc);
        }

        public async Task InsertDoctorAsync( int id, string apellido, string especialidad, int hospital, int salario)
        {
            string doc = this.GetDoctorJson(id, apellido, especialidad, hospital, salario);
            using(HttpClient client = new HttpClient())
            {
                string request = "api/doctores";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                StringContent content = new StringContent(doc, Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDoctorAsync (int id, string apellido, string especialidad, int hospital, int salario)
        {
            string doc = this.GetDoctorJson(id, apellido, especialidad, hospital, salario);
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctores";
                //PODRIAMOS PONER LUEGO URL + REQUEST POR EJEMPLO Y NO USAR EL BASEADDRESS
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                StringContent content = new StringContent(doc, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }

        public async Task DeleteDoctorAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctores/" + id;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.DeleteAsync(request);
            }
        }

        public async Task IncrementarSalariosAsync(int incremento, int hospital)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctores/IncrementarSalarios/"+ incremento + "/" + hospital;
                //PODRIAMOS PONER LUEGO URL + REQUEST POR EJEMPLO Y NO USAR EL BASEADDRESS
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.PutAsync(request, new StringContent(""));
            }
        }
    }
}
