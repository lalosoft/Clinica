using AppClinicaBack.Conexiones;
using AppClinicaBack.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AppClinicaBack.BL
{
    public class ConsultaBL
    {
        public async Task<(int StatusCode, string Message)> AddConsultaAsync(Consulta consulta)
        {
            try
            {
                SqlConnection conexion = null;
                conexion = ConexionBD.getInstance().ConectarBD();
                SqlCommand cmd = new SqlCommand("AgregarConsulta", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paciente", consulta.IdPaciente);
                cmd.Parameters.AddWithValue("@medico", consulta.IdMedico);
                cmd.Parameters.AddWithValue("@fechaConsulta", consulta.FechaConsulta.Value);

                conexion.Open();
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
                conexion.Close();
                return (StatusCodes.Status200OK, "Consulta agregada correctamente");

            }
            catch (Exception ex)
            {
                return (StatusCodes.Status500InternalServerError, "No se pudo agregar la consulta");
            }
        }

        public async Task<(int StatusCode, List<Consulta> Consultas)> GetConsultasAsync()
        {
            try
            {
                SqlConnection conexion = null;
                var listConsultas = new List<Consulta>();

                conexion = ConexionBD.getInstance().ConectarBD();
                SqlCommand cmd = new SqlCommand("ObtenerConsultas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var consulta = new Consulta();
                    consulta.Id = Convert.ToInt32(reader["Id"]);
                    consulta.NombreMedico = $"{ reader["Nombre"].ToString()} { reader["AppPaterno"].ToString() } { reader["AppMaterno"].ToString() }";
                    consulta.NombrePaciente = $"{ reader["NombrePaciente"].ToString()} { reader["ApPaciente"].ToString() } { reader["AmPaciente"].ToString() }";
                    consulta.FechaConsulta = reader["FechaConsulta"] as DateTime?;

                    listConsultas.Add(consulta);
                }
                reader.Close();
                cmd.Dispose();
                conexion.Close();

                return (StatusCodes.Status200OK, listConsultas);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                return (StatusCodes.Status500InternalServerError, new List<Consulta>());
            }
        }
    }
}
