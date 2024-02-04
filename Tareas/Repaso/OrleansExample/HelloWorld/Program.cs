using HelloWorld.Interfaz;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;

using var host = new HostBuilder()
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering();
    }).Build();

await host.StartAsync();

// Obtener el grando de la 
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

// Obtener la referencia del grano
var friend = grainFactory.GetGrain<IHelloGrain>("friend");

// Llamar al grano
var result = await friend.SayHello("Hola mi preciosa Ivanna");
Console.WriteLine(result);

Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

await host.StopAsync();

