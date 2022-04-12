using System;
using System.Data.SqlClient;

namespace AppClinicaBack.Conexiones
{
    public class ConexionBD
    {
        string cadenaConexion = "Server=ServerBD; DataBase=Clinica; Trusted_Connection=true;";

        private static ConexionBD conexion = null;
        private ConexionBD() { }

        public static ConexionBD getInstance()
        {
            if(conexion == null)
            {
                conexion = new ConexionBD();
            }
            return conexion;
        }
        
        public SqlConnection ConectarBD()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = cadenaConexion;
            return connection;
        }

    }
}
