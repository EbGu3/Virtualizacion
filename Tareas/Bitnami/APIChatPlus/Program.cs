
using APIChatPlus.Data;
using APIChatPlus.Models;
using System.Reflection;
using YamlDotNet.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Entity;
using MDChatPlus.Models;


namespace APIChatPlus
{
    public class Program
    {


        public static void Main(string[] args)
        {
            // Todo: Get Variables
            var variablesEntorno = GetVariablesEntorno();

            if (string.IsNullOrEmpty(variablesEntorno.DBConfiguracion.DBPuerto))
                Logs.WriteDebug("Variable de entorno no definida. Nombre => DBPuerto");

            if (string.IsNullOrEmpty(variablesEntorno.DBConfiguracion.DBServidor))
                Logs.WriteDebug("Variable de entorno no definida. Nombre => DBServidor");

            if (string.IsNullOrEmpty(variablesEntorno.DBConfiguracion.DBUsuario))
                Logs.WriteDebug("Variable de entorno no definida. Nombre => DBUsuario");

            if (string.IsNullOrEmpty(variablesEntorno.DBConfiguracion.DBContrasenya))
                Logs.WriteDebug("Variable de entorno no definida. Nombre => DBContrasenya");

            if (string.IsNullOrEmpty(variablesEntorno.DBConfiguracion.DBNombre))
                Logs.WriteDebug("Variable de entorno no definida. Nombre => DBNombre");


            string corsConfiguracion = "AllowAll";
            var builder = WebApplication.CreateBuilder(args);

            try
            {
                string cadenaConexion = $"Server={variablesEntorno.DBConfiguracion.DBServidor};Port={variablesEntorno.DBConfiguracion.DBPuerto};Database={variablesEntorno.DBConfiguracion.DBNombre};User Id={variablesEntorno.DBConfiguracion.DBUsuario};Password={variablesEntorno.DBConfiguracion.DBContrasenya}";
                builder.Services.AddDbContext<ChatPlusContext>(options =>
                {
                    options.UseNpgsql(cadenaConexion);
                });
            }
            catch (Exception ex)
            {
                Logs.WriteDebug("No se logro hacer la conexion a la base de datos");
                Logs.WriteDebug($"Ex: {ex.Message}, St: {ex.StackTrace}, Ie: {ex.InnerException}");
                throw new Exception(ex.Message);
            }

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsConfiguracion, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(corsConfiguracion);
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static VariablesEntornoVM GetVariablesEntorno()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Logs.WriteDebug("🖋️ Cargando variables de entorno");

            string archivoConfiguracionProyecto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "ProyectConfig.yaml");
            if (!File.Exists(archivoConfiguracionProyecto))
                throw new Exception($"Configuraciones del proyecto no encontrados. Ruta = {archivoConfiguracionProyecto}");


            
            var yamlFile = File.ReadAllText(archivoConfiguracionProyecto, System.Text.Encoding.UTF8);
            var dataYaml = new DeserializerBuilder()
                                                    .IgnoreUnmatchedProperties()
                                                    .Build()   
                                                    .Deserialize<VariablesEntornoVM>(yamlFile);

            return dataYaml;
        }
    }
}