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
    public class MedicoBL
    {
        public async Task<(int StatusCode, List<Medico> Medicos)> GetMedicosAsync()
        {
            try
            {
                SqlConnection conexion = null;
                var listPacientes = new List<Medico>();

                conexion = ConexionBD.getInstance().ConectarBD();
                SqlCommand cmd = new SqlCommand("ObtenerMedicos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var paciente = new Medico();
                    paciente.Id = Convert.ToInt32(reader["Id"]);
                    paciente.Nombre = $"{ reader["Nombre"].ToString()} { reader["AppPaterno"].ToString() } { reader["AppMaterno"].ToString() }";
                    paciente.Email = reader["Email"].ToString();
                    paciente.Telefono = reader["Telefono"].ToString();
                    paciente.Cedula = reader["CedulaProfesional"].ToString();

                    listPacientes.Add(paciente);
                }
                reader.Close();
                cmd.Dispose();
                conexion.Close();

                return (StatusCodes.Status200OK, listPacientes);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                return (StatusCodes.Status500InternalServerError, new List<Medico>());
            }
        }

        public async Task<(int StatusCode, string Message)> AddMedicoAsync(Medico medico)
        {
            try
            {
                SqlConnection conexion = null;
                conexion = ConexionBD.getInstance().ConectarBD();
                SqlCommand cmd = new SqlCommand("AgregarMedico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", medico.Nombre);
                cmd.Parameters.AddWithValue("@apellidoPaterno", medico.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", medico.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@email", medico.Email);
                cmd.Parameters.AddWithValue("@telefono", medico.Telefono);
                cmd.Parameters.AddWithValue("@cedula", medico.Cedula);

                conexion.Open();
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
                conexion.Close();
                return (StatusCodes.Status200OK, "Medico agregado correctamente");

            }
            catch (Exception ex)
            {
                return (StatusCodes.Status500InternalServerError, "No se pudo agregar el medico");
            }
        }
    }
}
