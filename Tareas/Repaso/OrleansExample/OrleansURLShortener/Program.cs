using Orleans.Runtime;
using OrleansURLShortener.Interfaz;

namespace OrleansURLShortener
{
    public class Program
    {
        public static void Main(string[] args)
        {


            // Add services to the container.
            //builder.Services.AddControllers();
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();
            //app.UseAuthorization();
            //app.MapControllers();

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseOrleans(static siloBuilder =>
            {
                siloBuilder.UseLocalhostClustering();
                siloBuilder.AddMemoryGrainStorage("urls");
            });
            var app = builder.Build();

            // Ejemplo 1 ------------------------------------------
            /*
                app.MapGet("/", () => "Hello World!");
            */


            // Ejemplo 2
            app.MapGet("/", static () => "Welcome to the URL shortener, powered by Orleans!");

            app.MapGet("/shorten",
            static async (IGrainFactory grains, HttpRequest request, string url) =>
            {
                var host = $"{request.Scheme}://{request.Host.Value}";

                //Validar el url 
                if (string.IsNullOrWhiteSpace(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute) is false)
                    return Results.BadRequest($"""
                                                The URL query string is required and needs to be well formed.
                                                Consider, ${host}/shorten?url=https://www.microsoft.com.
                                              """);


                // Create a unique, short Id
                var shortenedRouteSegment = Guid.NewGuid().GetHashCode().ToString();

                // Create and persist a grain with the shortened ID and full URL
                var shorterGrain = grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);
                await shorterGrain.SetUrl(url);

                // Return the shortened URL for later use
                var resultBuilder = new UriBuilder(host)
                {
                    Path = $"/go/{shortenedRouteSegment}"
                };

                return Results.Ok(resultBuilder.Uri);
            });


            app.MapGet("/go/{shortenedRouteSegment:required}", 
                static async (IGrainFactory grains, string shortenedRouteSegment) =>
            {
                var shortenerGrain = grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);
                var url = await shortenerGrain.GetUrl();

                // Handles missing schemes, defaults to "http://".
                var redirectBuilder = new UriBuilder(url);

                return Results.Redirect(redirectBuilder.Uri.ToString());
            });

            app.Run();
        }
    }
}
