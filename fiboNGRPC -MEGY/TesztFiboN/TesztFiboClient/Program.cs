using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Fibos;

namespace TesztFiboClient
{
    class Program
    {
        public static async Task Numbers(int mennyi)
        {
            Channel channel = new Channel("127.0.0.1:50052", Credentials.Insecure);
            var client = Fibonacci.NewClient(channel);
            //var reply = client.FiboN(new NumberN { Fib = mennyi });
            //SayHello(new HelloRequest { Name = user });

            using (var call = client.FiboN(new NumberN { Fib = mennyi }))
            {
                var responseStream = call.ResponseStream;
                StringBuilder result = new StringBuilder("A számok:");
                while (await responseStream.MoveNext())
                {
                    TheNumberS szam = responseStream.Current;
                    result.Append(szam + ", ");
                }
                Console.WriteLine(result.ToString());
            }

            channel.ShutdownAsync().Wait();

        }

        static void Main(string[] args)
        {
            int mennyi = int.Parse(Console.ReadLine());
            Numbers(mennyi);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();



        }
    }
}