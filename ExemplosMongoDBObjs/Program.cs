using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExemplosMongoDBObjs
{
    //http://json2csharp.com/
    class Program
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
        static void Main(string[] args)
        {
            //Task T = InsertManyAsync(args);
            //Task T = ListAsync(args);
            //Task T = FiltroAsync(args);
            //Task T = FiltroBuilderClassAsync(args);
            //Task T = ReplaceOneAsync(args);
            //Task T = UpdateManyAsync(args);
            Task T = DeleteManyAsync(args);
            //Console.WriteLine("Pressione ENTER");
            Console.ReadLine();
        }

        private static async Task InsertOneConnecManualAsync(string[] args)
        {
            Livro livro = new Livro();
            livro.Titulo = "Sob a redoma";
            livro.Autor = "Stephen King";
            livro.Ano = 2012;
            livro.Paginas = 679;
            List<string> listaAssuntos = new List<string>();
            listaAssuntos.Add("Ficção Científica");
            listaAssuntos.Add("Terror");
            listaAssuntos.Add("Ação");
            livro.Assunto = listaAssuntos;

            // acesso ao servidor do MongoDB
            string stringConexao = "mongodb://localhost:27017";
            IMongoClient cliente = new MongoClient(stringConexao);

            // acesso ao banco de dados
            IMongoDatabase bancoDados = cliente.GetDatabase("Biblioteca");

            // acesso a coleção
            //IMongoCollection<Livro> colecao = bancoDados.GetCollection<Livro>("Livros");
            var colecao = bancoDados.GetCollection<Livro>("Livros");

            //incluindo documento
            await colecao.InsertOneAsync(livro);
        }

        private static async Task InsertOneAsync(string[] args)
        {
            var conexaoBiblioteca = new ConectandoMongoDB();

            Livro livro = new Livro();
            livro = Livro.IncluiValoresLivro("Dom Casmurro", "Machado de Assis", 1923, 188, "Romance, Literatura  Brasileira");

            await conexaoBiblioteca.Livros.InsertOneAsync(livro);

            Livro livro2 = new Livro();
            livro2 = Livro.IncluiValoresLivro("A Arte da Ficção", "David Lodge", 2002, 230, "Didático, Auto Ajuda");
            await conexaoBiblioteca.Livros.InsertOneAsync(livro2);

            Console.WriteLine("Documento Incluido");
        }


        static async Task InsertManyAsync(string[] args)
        {

            var conexaoBiblioteca = new ConectandoMongoDB();

            List<Livro> livros = new List<Livro>();
            livros.Add(Livro.IncluiValoresLivro("A Dança com os Dragões", "George R R Martin", 2011, 934, "Fantasia, Ação"));
            livros.Add(Livro.IncluiValoresLivro("A Tormenta das Espadas", "George R R Martin", 2006, 1276, "Fantasia, Ação"));
            livros.Add(Livro.IncluiValoresLivro("Memórias Póstumas de Brás Cubas", "Machado de Assis", 1915, 267, "Literatura Brasileira"));
            livros.Add(Livro.IncluiValoresLivro("Star Trek Portal do Tempo", "Crispin A C", 2002, 321, "Fantasia, Ação"));
            livros.Add(Livro.IncluiValoresLivro("Star Trek Enigmas", "Dedopolus Tim", 2006, 195, "Ficção Científica, Ação"));
            livros.Add(Livro.IncluiValoresLivro("Emília no Pais da Gramática", "Monteiro Lobato", 1936, 230, "Infantil, Literatura Brasileira, Didático"));
            livros.Add(Livro.IncluiValoresLivro("Chapelzinho Amarelo", "Chico Buarque", 2008, 123, "Infantil, Literatura Brasileira"));
            livros.Add(Livro.IncluiValoresLivro("20000 Léguas Submarinas", "Julio Verne", 1894, 256, "Ficção Científica, Ação"));
            livros.Add(Livro.IncluiValoresLivro("Primeiros Passos na Matemática", "Mantin Ibanez", 2014, 190, "Didático, Infantil"));
            livros.Add(Livro.IncluiValoresLivro("Saúde e Sabor", "Yeomans Matthew", 2012, 245, "Culinária, Didático"));
            livros.Add(Livro.IncluiValoresLivro("Goldfinger", "Iam Fleming", 1956, 267, "Espionagem, Ação"));
            livros.Add(Livro.IncluiValoresLivro("Da Rússia com Amor", "Iam Fleming", 1966, 245, "Espionagem, Ação"));
            livros.Add(Livro.IncluiValoresLivro("O Senhor dos Aneis", "J R R Token", 1948, 1956, "Fantasia, Ação"));

            await conexaoBiblioteca.Livros.InsertManyAsync(livros);

            Console.WriteLine("Documento Incluido");
        }

        static async Task ListAsync(string[] args)
        {
            var conexaoBiblioteca = new ConectandoMongoDB();
            Console.WriteLine("Listando Documentos");

            //Alternativa correta! Através do método Find, passando como critério de busca um BsonDocument, 
            //significa que não estamos usando nenhum critério de busca, buscando assim todos os documentos da coleção.
            var listaLivros = await conexaoBiblioteca.Livros.Find(new BsonDocument()).ToListAsync();

            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }
            Console.WriteLine("Fim da Lista");
        }

        static async Task FiltroAsync(string[] args)
        {
            var conexaoBiblioteca = new ConectandoMongoDB();
            Console.WriteLine("Listando Documentos");

            var filtro = new BsonDocument
            {
                {"Autor", "Machado de Assis"}
            };

            var listaLivros = await conexaoBiblioteca.Livros.Find(filtro).ToListAsync();

            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }
            Console.WriteLine("Fim da Lista");
        }

        static async Task FiltroBuilderClassAsync(string[] args)
        {
            var conexaoBiblioteca = new ConectandoMongoDB();
            Console.WriteLine("\nListando Documentos Autor = Machado de Assis - Classe\n");

            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Autor, "Machado de Assis");

            var listaLivros = await conexaoBiblioteca.Livros.Find(condicao).ToListAsync();

            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }

            Console.WriteLine("\nListando Documentos ano publicacao seja maior ou igual a 1999 e que tenham mais de 300 paginas\n");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Gte(x => x.Ano, 1999) & construtor.Gte(x => x.Paginas, 300);

            listaLivros = await conexaoBiblioteca.Livros.Find(condicao).ToListAsync();
            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }

            Console.WriteLine("\nListando Documentos somente de ficção científica\n");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.AnyEq(x => x.Assunto, "Ficção Científica");

            listaLivros = await conexaoBiblioteca.Livros.Find(condicao).ToListAsync();
            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }

            Console.WriteLine("\nListando Documentos mais de 100 páginas ordenados\n");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Gt(x => x.Paginas, 100);

            listaLivros = await conexaoBiblioteca.Livros.Find(condicao).SortBy(x => x.Titulo).Limit(5).ToListAsync();
            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }

            Console.WriteLine("Fim da Lista");            
            Console.WriteLine("");
        }

        static async Task ReplaceOneAsync(string[] args)
        {
            var conexaoBiblioteca = new ConectandoMongoDB();
            Console.WriteLine("Listar e alterar o livro Saúde e Sabor");

            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Titulo, "Saúde e Sabor");

            var listaLivros = await conexaoBiblioteca.Livros.Find(condicao).SortBy(x => x.Titulo).Limit(5).ToListAsync();

            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
                
                //Alterando dados
                doc.Ano = 2000;
                doc.Paginas = 900;

                await conexaoBiblioteca.Livros.ReplaceOneAsync(condicao, doc);
            }
            Console.WriteLine("Fim da Lista");

            Console.WriteLine("Listar o livro Saúde e Sabor");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Eq(x => x.Titulo, "Saúde e Sabor");

            listaLivros = await conexaoBiblioteca.Livros.Find(condicao).SortBy(x => x.Titulo).Limit(5).ToListAsync();
            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }
            Console.WriteLine("Fim da Lista");
        }

        static async Task UpdateManyAsync(string[] args)
        {
            var conexaoBiblioteca = new ConectandoMongoDB();

            Console.WriteLine("\nListar os livros de George R R Martin\n");
            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Autor, "M. Assis");

            var listaLivros = await conexaoBiblioteca.Livros.Find(condicao).SortBy(x => x.Titulo).Limit(5).ToListAsync();
            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }
            Console.WriteLine("Fim da Lista\n\n");

            var construtorAlteracao = Builders<Livro>.Update;
            var condicaoAlteracao = construtorAlteracao.Set(x => x.Autor, "George R. R. Martin");
            await conexaoBiblioteca.Livros.UpdateManyAsync(condicao, condicaoAlteracao);

            Console.WriteLine("Registro Alterado\n\n");

            condicao = construtor.Eq(x => x.Autor, "George R. R. Martin");
            listaLivros = await conexaoBiblioteca.Livros.Find(condicao).SortBy(x => x.Titulo).ToListAsync();
            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }
            Console.WriteLine("Fim da Lista\n\n");
        }


        static async Task DeleteManyAsync(string[] args)
        {
            var conexaoBiblioteca = new ConectandoMongoDB();

            Console.WriteLine("Buscar os livros de Crispin A C");
            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Autor, "Crispin A C");

            var listaLivros = await conexaoBiblioteca.Livros.Find(condicao).ToListAsync();

            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }
            Console.WriteLine("Fim da Lista");

            Console.WriteLine("Excluindo os livros");
            await conexaoBiblioteca.Livros.DeleteManyAsync(condicao);

            Console.WriteLine("Buscar os livros de Crispin A C");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Eq(x => x.Autor, "Crispin A C");

            listaLivros = await conexaoBiblioteca.Livros.Find(condicao).ToListAsync();
            foreach (var doc in listaLivros)
            {
                Console.WriteLine(doc.ToJson<Livro>());
            }
            Console.WriteLine("Fim da Lista\n");
        }
    }
}