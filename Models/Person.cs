using System;
using System.Collections.Generic;

namespace MongodbCollectionBanchmark.Models
{
    public class Person
    {
        public Guid PersonId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Patronymic {get; set;}
        public string FullName {get; set;}
        public bool Gender {get; set;}
        public string Address {get; set;}
        public List<Phone> Phones {get; set;}
        public List<Document> Documents {get; set;}
        public DateTimeOffset DateModify {get; set;}

    }
}