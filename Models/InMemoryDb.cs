using System.Collections.Generic;

namespace MongodbCollectionBanchmark.Models
{
    public class InMemoryDb
    {
        public List<Phone> phoneData {get;} = new List<Phone>();
        public List<Document> documentData {get;} = new List<Document>(); 
        public List<Product> productData {get; } = new List<Product>();
        public List<Person> personData {get; } = new List<Person>();
        public List<LegalEntity> legalEntityData {get; } = new List<LegalEntity>();
    }
}