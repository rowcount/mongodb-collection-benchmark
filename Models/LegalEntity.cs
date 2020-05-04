using System;
using System.Collections.Generic;

namespace MongodbCollectionBanchmark.Models
{
    public class LegalEntity
    {
        public Guid LegalEntityId {get; set;}
        public string Name {get; set;}
        public string Type {get; set;}
        public string FullName {get; set;}
        public Guid Founder {get; set;}
        public string Address {get; set;}
        public List<Phone> Phones {get; set;}
        public List<Document> Documents {get; set;}
        public DateTimeOffset DateModify {get; set;}

    }
}