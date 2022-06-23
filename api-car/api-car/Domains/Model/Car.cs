using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace api_car.Domains.Model
{
    public class Car
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)]
        public string CarId { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
