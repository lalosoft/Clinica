using System;
using System.Collections.Generic;
using System.Net;
using AppClinicaFront.Models;
using Newtonsoft.Json;

namespace AppClinicaFront
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;
            string urlApiMedico = "https://localhost:44334/api/Medico/";
            string urlApiPaciente = "https://localhost:44334/api/Usuarios/";
            string urlApiConsulta = "https://localhost:44334/api/Consulta/";
            
            do
            {
                Console.WriteLine("****** Bienvenido a la Clinica *****");
                Console.WriteLine("1.- Agendar Cita");
                Console.WriteLine("2.- Visualizar Medicos");
                Console.WriteLine("3.- Ver Pacientes");
                Console.WriteLine("4.- Ver Consultas");
                Console.WriteLine("0.- Cerrar Sistema");
                Console.WriteLine("Seleccione una opcion: ");
                opcion = Convert.ToInt32(Console.ReadLine());
                
                switch (opcion)
                {
                    case 1:
                        int idMedico = 0;
                        int idPaciente = 0;
                        int dia = 0;
                        int mes = 0;
                        int year = 0;
                        int hora = 0;
                        
                        Console.WriteLine("Ingrese el ID del medico: ");
                        idMedico = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Ingrese el ID del paciente: ");
                        idPaciente = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Ingrese el dia de la cita: ");
                        dia = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Ingrese el mes de la cita: ");
                        mes = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Ingrese el año de la cita: ");
                        year = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Ingrese la hora de la cita: ");
                        hora = Convert.ToInt32(Console.ReadLine());

                        var fechaConsulta = new DateTime(year, mes, dia, hora, 0, 0);
                        var consulta = new Consulta
                        {
                            IdMedico = idMedico,
                            IdPaciente = idPaciente,
                            FechaConsulta = fechaConsulta
                        };

                        AddConsulta(consulta);
                        
                        break;

                    case 2:
                        var jsonMed = new WebClient().DownloadString(urlApiMedico);
                        var listMedicos = JsonConvert.DeserializeObject<List<Medico>>(jsonMed);
                        foreach (var med in listMedicos)
                        {
                            Console.WriteLine("Id: " + med.Id);
                            Console.WriteLine("Nombre: " + med.Nombre);
                            Console.WriteLine("Cedula: " + med.Cedula);
                            Console.WriteLine();
                        }
                        break;

                    case 3:
                        var jsonPac = new WebClient().DownloadString(urlApiPaciente);
                        var listPacientes = JsonConvert.DeserializeObject<List<Paciente>>(jsonPac);
                        foreach (var pac in listPacientes)
                        {
                            Console.WriteLine("Id: " + pac.Id);
                            Console.WriteLine("Nombre: " + pac.Nombre);
                            Console.WriteLine("Email: " + pac.Email);
                            Console.WriteLine();    
                        }
                        break;

                    case 4:
                        var jsonCons = new WebClient().DownloadString(urlApiConsulta);
                        var listCons = JsonConvert.DeserializeObject<List<Consulta>>(jsonCons);
                        foreach (var cons in listCons)
                        {
                            Console.WriteLine("Id: " + cons.Id);
                            Console.WriteLine("Medico: " + cons.NombreMedico);
                            Console.WriteLine("Paciente: " + cons.NombrePaciente);
                            Console.WriteLine("Fecha Consulta: " + cons.FechaConsulta);
                            Console.WriteLine();
                        }
                        break;
                }
            } while (opcion != 0);

        }

        public static async void AddConsulta(Consulta consulta)
        {
            string urlApiConsulta = "https://localhost:44334/api/Consulta/";
            var apiClient = new ApiClient(urlApiConsulta);
            var res = await apiClient.AddConsulta(consulta);
            var a = 5;
        }
    }
}
