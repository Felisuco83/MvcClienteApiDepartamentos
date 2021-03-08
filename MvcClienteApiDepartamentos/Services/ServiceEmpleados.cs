using ApiEmpleadosOAuth.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcClienteApiDepartamentos.Services
{
    
    public class ServiceEmpleados
    {
        private Uri UrlApi;
        private MediaTypeWithQualityHeaderValue Header;
        public ServiceEmpleados(string url)
        {
            this.UrlApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> GetToken(string username, string password)
        {
            using (HttpClient client = new HttpClient()) 
            {
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                LoginModel login = new LoginModel { UserName = username, Password = password };
                string json = JsonConvert.SerializeObject(login);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                string request = "auth/login";
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject jobject = JObject.Parse(data);
                    string token = jobject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<T> CallApi<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Accept.Clear();
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

        public async Task<T> CallApi<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
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

        public async Task<Empleado> BuscarEmpleadoAsync (int idempleado)
        {
            string request = "api/empleados" + idempleado;
            Empleado empleado = await this.CallApi<Empleado>(request);
            return empleado;
        }

        public async Task<Empleado> GetPerfil (string token)
        {
            string request = "api/empleados/perfilempleado";
            Empleado empleado = await this.CallApi<Empleado>(request, token);
            return empleado;
        }

        public async Task<List<Empleado>> GetSubordinados (string token)
        {
            string request = "api/empleados/subordinados";
            List<Empleado> empleados = await this.CallApi<List<Empleado>>(request, token);
            return empleados;
        }
    }
}
