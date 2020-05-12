using System;
using System.Diagnostics;
using System.IO;
using MongodbCollectionBanchmark.Models;
using Newtonsoft.Json;

namespace MongodbCollectionBanchmark.Utils.DataUtils
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