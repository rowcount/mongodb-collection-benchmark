using System;
using System.IO;
using MongodbCollectionBanchmark.Models;
using Bogus;
using System.Collections.Generic;
using Person = MongodbCollectionBanchmark.Models.Person;
using Document = MongodbCollectionBanchmark.Models.Document;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Bogus.DataSets;
using Newtonsoft.Json;

namespace MongodbCollectionBanchmark.Utils
{
//TODO [goncharov] сделать статическим. 
    public class DataGenerator
    {
        private readonly int _objectCount;
        private readonly Faker _faker;
        private readonly InMemoryDb _inMemoryDb;

        //TODO [goncharov] слешком много входных параметров, очивидно, что они объединяются в группы:
        //TODO [goncharov] {phoneData, documentData, productData, personData, legalEntityData} == InMemoryDb, facker, count
        //TODO [goncharov] count- очень непонятный параметр, если подумать как его правильно назвать, станет четче понятен его смысл.
        public DataGenerator (int objectCount, Faker faker, InMemoryDb inMemoryDb)
        {
            _objectCount = objectCount;
            _faker = faker ;
            _inMemoryDb = inMemoryDb;
        }

        //TODO [goncharov] логичнее назвать GenerateDocs или GenerateData.
        public void PrepareDocs()
        {
            Console.WriteLine("Preparing docs started");
            var sw = Stopwatch.StartNew();
            var userIds = new List<string>(_objectCount / 100);
            
            foreach ( var i in Enumerable.Range(0, _objectCount / 100))
            {
                var gender = _faker.PickRandom<Name.Gender>();
                
                var phones = new Faker<Phone>("ru")
                    .RuleFor(u => u.Number, f => f.Phone.PhoneNumber())
                    .Generate(3);
                _inMemoryDb.phoneData.AddRange(phones);

                var documents = new Faker<Document>("ru")
                    .RuleFor(u => u.Passport, f => f.Finance.Account())
                    .RuleFor(u => u.INN, f => f.Finance.Account())
                    .RuleFor(u => u.OGRN, f => f.Finance.Account())
                    .RuleFor(u => u.KPP, f => f.Finance.Account())
                    .RuleFor(u => u.SNILS, f => f.Finance.Account())
                    .Generate(1);
                _inMemoryDb.documentData.AddRange(documents);

                var products = new Faker<Product>("ru")
                    .RuleFor(u => u.Name, f => f.Commerce.Product())
                    .RuleFor(u => u.Code, f => f.Commerce.Ean13())
                    .RuleFor(u => u.Number, f => i)
                    .Generate(3);
                _inMemoryDb.productData.AddRange(products);

                var persons = new Faker<Person>("ru")
                    .RuleFor(u => u.PersonId, f => Guid.NewGuid())
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName(gender))
                    .RuleFor(u => u.LastName, f => f.Name.LastName(gender))
                    .RuleFor(u => u.Patronymic, f => f.Name.FirstName())
                    .RuleFor(u => u.FullName, f => f.Name.FirstName(gender)+ " " + f.Name.LastName(gender))
                    .RuleFor(u => u.Gender, f => f.Random.Bool())
                    .RuleFor(u => u.Address, f => f.Address.FullAddress())
                    .RuleFor(u => u.Phones, f => phones)
                    .RuleFor(u => u.Documents, f => documents)
                    .RuleFor(u => u.Products, f => products)
                    .RuleFor(u => u.DateModify, f => f.Date.RecentOffset())
                    .Generate(1);
                _inMemoryDb.personData.AddRange(persons);

                var legalEntities = new Faker<LegalEntity>("ru")
                    .RuleFor(u => u.LegalEntityId, f => Guid.NewGuid())
                    .RuleFor(u => u.Name, f => f.Company.CompanyName())
                    .RuleFor(u => u.Type, f => f.Company.CompanySuffix())                    
                    .RuleFor(u => u.FullName, f => f.Company.CompanyName())
                    .RuleFor(u => u.Founder, f => persons.First().PersonId)
                    .RuleFor(u => u.Address, f => f.Address.FullAddress())
                    .RuleFor(u => u.Phones, f =>phones)
                    .RuleFor(u => u.Documents, f => documents)
                    .RuleFor(u => u.Products, f => products)
                    .RuleFor(u => u.DateModify, f => f.Date.RecentOffset())
                    .Generate(1);
                _inMemoryDb.legalEntityData.AddRange(legalEntities);              
                
                
            };

            //TODO [goncharov] нужно дернуть sw.Stop(), а то как-то не хорошо, мне кажется.
            Console.WriteLine("done at " + sw.ElapsedMilliseconds + " ms");            
        }

        //TODO [goncharov] это не его зона ответственности. Лучше вынести отсюда.
         public void SaveData()
        {
            Console.Write("Docs saving started...........");
            var sw = Stopwatch.StartNew();
            File.WriteAllText(@"/temp/_inMemoryDb.personData.json", JsonConvert.SerializeObject(_inMemoryDb.personData));
            File.WriteAllText(@"/temp/_inMemoryDb.legalEntityData.json", JsonConvert.SerializeObject(_inMemoryDb.legalEntityData));
            //TODO [goncharov] нужно дернуть sw.Stop(), а то как-то не хорошо, мне кажется.
            Console.WriteLine("done at " + sw.ElapsedMilliseconds + " ms");
        }

    }
}
