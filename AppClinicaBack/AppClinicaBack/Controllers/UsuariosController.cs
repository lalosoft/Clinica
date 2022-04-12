using AppClinicaBack.BL;
using AppClinicaBack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppClinicaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosBL usuariosBL;

        public UsuariosController(UsuariosBL bl)
        {
            usuariosBL = bl;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPacientes() 
        {
            var res = await usuariosBL.GetPacientes();
            return StatusCode(res.StatusCode, res.Pacientes);
        }

        [HttpPost]
        public async Task<IActionResult> AddPaciente(Paciente paciente)
        {
            var res = await usuariosBL.AddPacienteAsync(paciente);
            return StatusCode(res.StatusCode, res.Message);
        }
    }
}
