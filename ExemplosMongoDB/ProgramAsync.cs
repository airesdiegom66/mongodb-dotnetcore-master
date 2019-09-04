using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExemplosMongoDB
{
    class ProgramAsync
    {
        //Programação assíncrona é exigida pela documentação do MongoDB.
        static void Main(string[] args)
        {
            //Task T = MainAsync(args);
            Task T2 = ManipulaDocumentosMongoDB(args);

            Console.WriteLine("Pressione ENTER");
            Console.ReadLine();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Esperando 10 segundos ....");
            await Task.Delay(10000);
            Console.WriteLine("Esperei 10 segundos ....");
        }

        private static async Task ManipulaDocumentosMongoDB(string[] args)
        {
            //{
            //    "Título":"Guerra dos Tronos",
            //    "Autor":"George R R Martin",
            //    "Ano":1999,
            //    "Páginas":856
            //    "Assunto": [
            //        "Fantasia",
            //        "Ação"]
            //}

            //BsonDocument: tipo json para mongo
            var doc = new BsonDocument
            {
                {"Título", "Guerra dos Tronos"},
                {"Autor", "George R R Martin"},
                {"Ano", "1999"},
                {"Páginas", "856"}
            };

            var assuntoArray = new BsonArray();
            assuntoArray.Add("Fantasia");
            assuntoArray.Add("Ação");
            doc.Add("Assunto", assuntoArray);

            Console.WriteLine(doc);
        }

    }
}
