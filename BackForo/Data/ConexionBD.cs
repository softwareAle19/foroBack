using System.Data;
using System.Data.SqlClient;

namespace BackForo.Data
{
    public class ConexionBD
    {
        private readonly string cadenaSQL;

        public ConexionBD(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        public SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaSQL);
            conexion.Open();
            return conexion;
        }

        public void CerrarConexion(SqlConnection conexion)
        {
            if (conexion != null && conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}
