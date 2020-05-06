using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using MongodbCollectionBanchmark.Models;
using Newtonsoft.Json;

namespace MongodbCollectionBanchmark.Utils
{
    public class DataSaver
    {
        private readonly InMemoryDb _inMemoryDb;

        public DataSaver(InMemoryDb inMemoryDb)
        {
            _inMemoryDb = inMemoryDb;
        }
        public void SerializeData()
        {
            Console.Write("Docs saving started...........");
            var sw = Stopwatch.StartNew();
            File.WriteAllText(@"/temp/_inMemoryDb.personData.json", JsonConvert.SerializeObject(_inMemoryDb.personData));
            File.WriteAllText(@"/temp/_inMemoryDb.legalEntityData.json", JsonConvert.SerializeObject(_inMemoryDb.legalEntityData));
            sw.Stop();
            Console.WriteLine("done at " + sw.ElapsedMilliseconds + " ms");
        }
    }
}