using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExemplosMongoDB
{
    class ProgramAcessandoMongoDB
    {
        //Programação assíncrona é exigida pela documentação do MongoDB.
        static void Main(string[] args)
        {   
            Task T = ManipulaDocumentosMongoDB(args);
            Console.WriteLine("Pressione ENTER");
            Console.ReadLine();
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

            //acesso ao servidor mongodb
            string stringConexao = "mongodb://localhost:27017";
            IMongoClient cliente = new MongoClient(stringConexao);

            // acesso ao banco de dados
            //Se não existir o database, ele irá criar
            IMongoDatabase bancoDados = cliente.GetDatabase("Biblioteca");

            // acesso a coleção
            //se a coleção não existir, ele irá criar
            IMongoCollection<BsonDocument> colecao = bancoDados.GetCollection<BsonDocument>("Livros");

            //incluindo documento
            await colecao.InsertOneAsync(doc);

            Console.WriteLine("Documento Incluido");
        }
    }
}
