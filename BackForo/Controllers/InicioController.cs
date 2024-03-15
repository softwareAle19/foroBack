using BackForo.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using BackForo.Models;


namespace BackForo.Controllers
{
    [EnableCors("ReglasCors")] //Habilitar los cors 

    [Route("api/[controller]")]
    [ApiController]
    public class InicioController : ControllerBase
    {
        private readonly ConexionBD conexionBD;

        public InicioController(ConexionBD conexionBD)
        {
            this.conexionBD = conexionBD;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UsuarioE obj)
        {
            try
            {
                using (var conexion = conexionBD.AbrirConexion())
                {
                    var cmd = new SqlCommand("sp_ValidoIngreso", conexion);
                    cmd.Parameters.AddWithValue("email", obj.email);
                    cmd.Parameters.AddWithValue("password", obj.password);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var resultado = cmd.ExecuteScalar();
                    if (resultado != null && (int)resultado > 0)
                    {
                        // Usuario autenticado correctamente
                        return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario autenticado correctamente" });
                    }
                    else
                    {
                        // Credenciales incorrectas
                        return StatusCode(StatusCodes.Status401Unauthorized, new { mensaje = "Credenciales incorrectas" });
                    }
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
