using RestSharp;
using Newtonsoft.Json;
using ConsumeAPIRandom;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("... Consumiendo API Random 😎...");

        //  TODO: Consumir API
        var apiURL = "http://www.randomnumberapi.com";
        RestClientOptions options = new (apiURL)
        {
            MaxTimeout = -1,
        };
        var client = new RestClient(options);
        var request = new RestRequest("/api/v1.0/random", Method.Get);
        
        //  TODO: Obtener la data
        RestResponse response = client.Execute(request);
        var content = response.Content;
        if (content == null ) {
            Console.WriteLine($"❌ No hay conexion al servicio. URL = {apiURL}/api/v1.0/random");
            return;
        }
        
        List<int>? numbers  = JsonConvert.DeserializeObject<List<int>>(content);
        if (numbers == null )
        {
            Console.WriteLine("🥲 Respuesta no valida del servicio.");
            return;
        }

        //  TODO: Crear Archivo
        Console.WriteLine("> Creando archivo 🚗...");
        var respuestaCrear = FileOperation.CrearArchivo(numbers[0].ToString());
        if (!respuestaCrear.Item1)
        {
            Console.WriteLine($"❌ Archivo no creado. Ruta = {respuestaCrear.Item2}");
            return;
        }

        //  TODO: Escribir archivo
        Console.WriteLine("> Agregando informacion 🖋...");
        var texto = "EBER GUERRA 😎 1136617 🐟 IVANNA 🥰...";
        var respuestaEscribir = FileOperation.EscrbirArchivo(texto, respuestaCrear.Item2);
        if (!respuestaEscribir)
        {
            Console.WriteLine($"❌ Texto no agregado. Ruta = {respuestaCrear.Item2}");
            return;
        }

        Console.WriteLine($"> Informacion Agregada 💫. Ruta = {respuestaCrear.Item2}...");
        Console.ReadKey();
    }
}