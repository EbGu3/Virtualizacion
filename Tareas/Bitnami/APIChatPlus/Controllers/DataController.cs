using APIChatPlus.Models;
using MDChatPlus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace APIChatPlus.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        ChatPlusContext _context;

        public DataController(ChatPlusContext context)
        {
            this._context = context;
        }

        [HttpGet("GetUsers")]
        public ActionResult<RepuestaPeticionVM<List<SchemaUserVM>>> GetUsers()
        {

            RepuestaPeticionVM<SchemaUserVM> repuesta = new();
            try
            {
                var usuarios = _context.Usuarios
                                            .Select(usuarioRegistro => new SchemaUserVM {
                                                 Email = usuarioRegistro.Email,
                                                 Id = usuarioRegistro.Id,
                                                 Nombre = usuarioRegistro.Nombre
                                            })
                                            .ToList();

                return Ok(new RepuestaPeticionVM<List<SchemaUserVM>> {
                    Contenido = usuarios,
                    Descripcion = "Informacion basica del usuario, obtenida",
                    InformacionPeticion = new InformacionRespuestaVM()
                    {
                        Adicionales = new List<string>(),
                        CodigoEstado = Ok().StatusCode,
                        Excepcion = string.Empty,
                        OcurrioError = false,
                    }
                });
            } 
            catch (Exception ex)
            {
                return BadRequest(new RepuestaPeticionVM<List<SchemaUserVM>>
                {
                    //Contenido = new List<SchemaUserVM>(),
                    Descripcion = "Informacion basica del usuario, no obtenida",
                    InformacionPeticion = new InformacionRespuestaVM()
                    {
                        Adicionales = new List<string>(),
                        CodigoEstado = Ok().StatusCode,
                        Excepcion = ex.Message,
                        OcurrioError = true,
                    }
                });
            }
        }
    }
}
