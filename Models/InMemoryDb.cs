using System.Collections.Generic;

namespace MongodbCollectionBanchmark.Models
{
    public class InMemoryDb
    {
        public List<Phone> phoneData {get; set;}
        public List<Document> documentData {get; set;}
        public List<Product> productData {get; set;}
        public List<Person> personData {get; set;}
        public List<LegalEntity> legalEntityData {get; set;}
    }
}