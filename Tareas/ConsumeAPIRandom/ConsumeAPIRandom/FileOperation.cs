using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeAPIRandom
{
    public class FileOperation
    {
        public static Tuple<bool, string> CrearArchivo(string nombre, string extension = "txt")
        {
            if (extension.Contains('.'))
                extension = extension.Replace(".", "");

            var temp = Path.GetTempPath();
            var path = Path.Combine(temp, $"{nombre}.{extension}");

            using (FileStream fs = File.Create(path)) { fs.Close(); }
            var creado = File.Exists(path);
            
            return new Tuple<bool, string>(creado, path);
        }

        public static bool EscrbirArchivo(string texto, string pathFile)
        {
            try
            {
                using (StreamWriter sw = new(pathFile))
                {
                    sw.Write(texto);
                }
                return true;
            }
            catch { return false; }
        }
    }
}
