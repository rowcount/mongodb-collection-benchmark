using System;
using MongodbCollectionBanchmark.Utils;

namespace MongodbCollectionBanchmark
{
    class Program
    {
        private static Setup _setup = new Setup();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _setup.GenerateData();
        }
    }
}
