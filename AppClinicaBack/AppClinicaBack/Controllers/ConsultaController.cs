using AppClinicaBack.BL;
using AppClinicaBack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppClinicaBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ConsultaController : ControllerBase
    {
        private readonly ConsultaBL consultaBL;

        public ConsultaController(ConsultaBL bl)
        {
            consultaBL = bl;
        }

        [HttpPost("AddConsulta")]
        public async Task<IActionResult> AddConsulta(Consulta consulta)
        {
            var res = await consultaBL.AddConsultaAsync(consulta);
            return StatusCode(res.StatusCode, res.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsultas()
        {
            var res = await consultaBL.GetConsultasAsync();
            return StatusCode(res.StatusCode, res.Consultas);
        }
    }
}
