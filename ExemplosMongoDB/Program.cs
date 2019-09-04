using System;
using System.Threading;

namespace ExemplosMongoDB
{
    class Program
    {
        //Programação assíncrona é exigida pela documentação do MongoDB.
        static void Main(string[] args)
        {
            MainSync(args);
            Console.WriteLine("Pressione ENTER");
            Console.ReadLine();
        }

        private static void MainSync(string[] args)
        {
            Console.WriteLine("Esperando 10 segundos ....");
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("Esperei 10 segundos ....");
        }
    }
}

