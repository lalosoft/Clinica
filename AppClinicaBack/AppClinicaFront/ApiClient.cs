using AppClinicaFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AppClinicaFront
{
    public class ApiClient : HttpClient
    {
        public ApiClient(string rootUrl)
        {
            BaseAddress = new Uri(rootUrl);
        }

        public async Task<(HttpStatusCode StatusCode, string Message)> AddConsulta(Consulta consulta)
        {
            var response = await this.PostAsJsonAsync("AddConsulta", consulta);
            return (response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
