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
    public class UsuariosBL
    {
        public async Task<(int StatusCode, List<Paciente> Pacientes)> GetPacientes()
        {
            try
            {
                SqlConnection conexion = null;
                var listPacientes = new List<Paciente>();

                conexion = ConexionBD.getInstance().ConectarBD();
                SqlCommand cmd = new SqlCommand("ObtenerPacientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var paciente = new Paciente();
                    paciente.Id = Convert.ToInt32(reader["Id"]);
                    paciente.Nombre = $"{ reader["Nombre"].ToString()} { reader["AppPaterno"].ToString() } { reader["AppMaterno"].ToString() }";
                    paciente.Email = reader["Email"].ToString();
                    paciente.Telefono = reader["Telefono"].ToString();
                    paciente.Direccion = reader["Direccion"].ToString();

                    listPacientes.Add(paciente);
                }
                reader.Close();
                cmd.Dispose();
                conexion.Close();

                return (StatusCodes.Status200OK, listPacientes);
            }
            catch(Exception ex)
            {
                var x = ex.Message;
                return (StatusCodes.Status500InternalServerError, new List<Paciente>());
            }
        }

        public async Task<(int StatusCode, string Message)> AddPacienteAsync(Paciente paciente)
        {
            try
            {
                SqlConnection conexion = null;
                conexion = ConexionBD.getInstance().ConectarBD();
                SqlCommand cmd = new SqlCommand("AgregarPaciente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", paciente.Nombre);
                cmd.Parameters.AddWithValue("@apellidoPaterno", paciente.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", paciente.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@email", paciente.Email);
                cmd.Parameters.AddWithValue("@telefono", paciente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", paciente.Direccion);

                conexion.Open();
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
                conexion.Close();
                return (StatusCodes.Status200OK, "Paciente agregado correctamente");
                
            }
            catch (Exception ex)
            {
                return (StatusCodes.Status500InternalServerError, "No se pudo agregar el paciente");
            }
        }
    }
}
