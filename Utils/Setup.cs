using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using MongodbCollectionBanchmark.Models;
using Person = MongodbCollectionBanchmark.Models.Person;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongodbCollectionBanchmark.Utils
{
    //TODO [goncharov] честно говоря, класс сумбурный. Непонятно как с ним работать - API очень сумбурное.
    //TODO [goncharov] логичнее сделать вот такой API:
    /*
        Task GenerateInitialScriptsAsync(string path);
        Task InitDb(IMongoDatabase db);
        Task CleanDb(IMongoDatabase db);
        Task FillDb(IMongoDatabase db);
        Task FillDbByScripts(IMongoDatabase db, string path);    
    */
    public class Setup
    {
        private IMongoCollection<LegalEntity> _mngLegalEntity;
        private IMongoCollection<Person> _mngPerson;
        private IMongoCollection<BsonDocument> _client;
        private IMongoDatabase _database;
        private const int Count = 100000;
        
        //TODO [goncharov] Эти поля логично вынести в отдельный класс InMemoryDb vvvvvvv
        private readonly List<Phone> _phoneData = new List<Phone>(3);
        private readonly List<Document> _documentData = new List<Document>(1);
        private readonly List<Product> _productData = new List<Product>();
        private readonly List<LegalEntity> _legalEntityData = new List<LegalEntity>(Count);
        private readonly List<Person> _personData = new List<Person>(Count);       
        //TODO [goncharov] Эти поля логично вынести в отдельный класс InMemoryDb ^^^^^^^^
        
        //TODO [goncharov] Мне кажется фэйкер- это зона ответственности Preparer (который надо переименовать в дата-генератор)
        private readonly Faker _faker = new Faker("ru");
        private Preparer _preparer;

        //TODO [goncharov] лучше переименовать GenerateInitialScripts
        //TODO [goncharov] в качестве входного, имеет смысл, передавать путь к папке, где мы хотим сохранить скрипты базы.
        public void GenerateData()
        {
            _preparer = new Preparer(Count, _faker,_phoneData,_documentData,_productData,_personData,_legalEntityData);
            
            Console.WriteLine("Start generate data");
            _preparer.PrepareDocs();
            _preparer.SaveData();
        }

        private void GlobalCleanup()
        {
            var sw = Stopwatch.StartNew();
            Console.Write("Start delete Blog Rows..........");
            CleanMongoDb();
            Console.WriteLine("done at " + sw.ElapsedMilliseconds + " ms.");
        }

        private void CleanMongoDb()
        {
            _database.DropCollection("person");
            _database.DropCollection("legalEntity");
            _database.DropCollection("client");
        }

        [Obsolete]
        private void OpenMongodb()
        {
            _mngPerson = _database.GetCollection<Person>("person");
            _mngLegalEntity = _database.GetCollection<LegalEntity>("legalEntity");
            _client = _database.GetCollection<BsonDocument>("client");
            _mngPerson.Indexes.CreateOne(new JsonIndexKeysDefinition<Person>("{\"Phones.Number\" : 1 }"));//           
            _mngLegalEntity.Indexes.CreateOne(new JsonIndexKeysDefinition<LegalEntity>("{\"Phones.Number\" : 1 }"));//
            _client.Indexes.CreateOne(new JsonIndexKeysDefinition<BsonDocument>("{\"Phones.Number\" : 1 }"));//           
            Console.WriteLine("Opened mongo connection");
        }


    }
}
