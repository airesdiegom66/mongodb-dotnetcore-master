using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace exemplosMongoNET
{
    class ConectandoMongoDB
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
            Console.WriteLine();
            Console.WriteLine(" Pressione Enter");
            Console.ReadLine();
        }
        static async Task MainAsync(string[] args)
        {

            //var conexaoAeroporto = new conectandoMongoDBGeo();
            //var listaAeroportos = await conexaoAeroporto.Airports.Find(new BsonDocument()).ToListAsync();
            //Console.WriteLine("Listando todos os aeroportos");

            //foreach (var doc in listaAeroportos)
            //{
            //    Console.WriteLine(doc.name);
            //}
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");

            //var ponto = new GeoJson2DGeographicCoordinates(-118.325258, 34.103212);
            //var localizacao = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(ponto);

            //var construtor = Builders<Aeroporto>.Filter;
            //var filtro_builder = construtor.NearSphere(x => x.loc, localizacao, 100000);
            //listaAeroportos = await conexaoAeroporto.Airports.Find(filtro_builder).ToListAsync();

            //Console.WriteLine("Listando todos os aeroportos proximo de mim");
            //foreach (var doc in listaAeroportos)
            //{
            //    Console.WriteLine(doc.name);
            //}
            //Console.WriteLine("");
        }
    }
}
