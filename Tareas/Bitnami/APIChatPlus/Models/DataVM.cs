using System.ComponentModel.DataAnnotations;

namespace APIChatPlus.Models
{
    public class SchemaUserVM
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public long Id { get; set; }
    }

    public class InformacionRespuestaVM
    {
        public int CodigoEstado { get; set; }
        public string Excepcion { get; set; }
        public List<string> Adicionales { get; set; }
        public bool OcurrioError { get; set; }

        public InformacionRespuestaVM()
        {
            CodigoEstado = 0;
            Excepcion = string.Empty;
            Adicionales = new List<string>();
            OcurrioError = false;
        }
    }

    public class RepuestaPeticionVM<T>
    {
        public T Contenido { get; set; }
        public string Descripcion { get; set; }
        public InformacionRespuestaVM InformacionPeticion { get; set; }

        public RepuestaPeticionVM()
        {
            Descripcion = string.Empty;
            InformacionPeticion = new InformacionRespuestaVM();
        }
    }

}
