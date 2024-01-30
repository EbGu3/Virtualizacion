using System.Diagnostics.CodeAnalysis;

namespace APIChatPlus.Models
{
    public class VariablesEntornoVM
    {
        public DBConfiguracionVM DBConfiguracion { get; set; }

        public VariablesEntornoVM()
        {
            DBConfiguracion = new ();
        }
    }

    public class DBConfiguracionVM
    {
        public string DBUsuario     { get; set; } 
        public string DBContrasenya { get; set; }
        public string DBServidor    { get; set; }
        public string DBPuerto      { get; set; }
        public string DBNombre      { get; set; }

        public DBConfiguracionVM ()
        {
            DBUsuario     = string.Empty;
            DBContrasenya = string.Empty;        
            DBServidor    = string.Empty;
            DBPuerto      = string.Empty;
            DBNombre      = string.Empty;
        }
    }
}
