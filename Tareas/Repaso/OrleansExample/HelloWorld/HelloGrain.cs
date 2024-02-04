using HelloWorld.Interfaz;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public class HelloGrain : Grain, IHelloGrain
    {
        public Task<string> SayHello(string greeting)
            => Task.FromResult($"Hello, {greeting}");
    }
}
