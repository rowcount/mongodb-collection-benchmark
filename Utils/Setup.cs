using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using MongodbCollectionBanchmark.Models;
using Person = MongodbCollectionBanchmark.Models.Person;

namespace MongodbCollectionBanchmark.Utils
{
    public class Setup
    {
        private const int Count = 100000;
        private readonly List<Phone> _phoneOpData;
        private readonly List<Document> _documentOpData;
        private readonly List<Product> _productOpData;
        private readonly List<LegalEntity> _legalEntityOpData;
        private readonly List<Person> _personOpData;       
        private readonly Faker _faker = new Faker("ru");
        private Preparer _preparer;

        public void GenerateData()
        {
            _preparer = new Preparer(Count, _faker,_phoneOpData,_documentOpData,_productOpData,_personOpData,_legalEntityOpData);
            
            Console.WriteLine("Start generate data");
            _preparer.PrepareDocs();
            _preparer.SaveData();
        }
    }
}