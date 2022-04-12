using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClinicaFront.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public DateTime? FechaConsulta { get; set; }
        public string Diagnostico { get; set; }
        public string NombreMedico { get; set; }
        public string NombrePaciente { get; set; }
    }
}
