using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Interfaz
{
    internal interface IHelloGrain : IGrainWithStringKey
    {
        Task<string> SayHello(string greeting);
    }
}
