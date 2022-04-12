using AppClinicaBack.BL;
using AppClinicaBack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppClinicaBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class MedicoController : ControllerBase
    {
        private readonly MedicoBL medicoBL;

        public MedicoController(MedicoBL bl)
        {
            medicoBL = bl;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicos()
        {
            var res = await medicoBL.GetMedicosAsync();
            return StatusCode(res.StatusCode, res.Medicos);
        }

        [HttpPost]
        public async Task<IActionResult> AddMedico(Medico medico)
        {
            var res = await medicoBL.AddMedicoAsync(medico);
            return StatusCode(res.StatusCode, res.Message);
        }
    }
}
