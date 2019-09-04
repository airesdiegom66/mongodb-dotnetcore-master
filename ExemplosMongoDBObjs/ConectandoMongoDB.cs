using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExemplosMongoDBObjs
{
    class ConectandoMongoDB
    {
        public const string STRING_DE_CONEXAO = "mongodb://localhost:27017";
        public const string NOME_DA_BASE = "Biblioteca";
        public const string NOME_DA_COLECAO = "Livros";

        private static readonly IMongoClient _cliente;
        private static readonly IMongoDatabase _baseDeDados;

        static ConectandoMongoDB()
        {
            _cliente = new MongoClient(STRING_DE_CONEXAO);
            _baseDeDados = _cliente.GetDatabase(NOME_DA_BASE);
        }

        public IMongoClient Cliente
        {
            get { return _cliente; }
        }

        public IMongoCollection<Livro> Livros
        {
            get { return _baseDeDados.GetCollection<Livro>(NOME_DA_COLECAO); }

        }
    }
}
