using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
namespace Service.Crypto.Domain
{
    public class CryptoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  ID { get; set; }
        [BsonElement("Key")]
        [JsonProperty("Key")]
        public string Key { get; set; }
        [BsonElement("data")]
        [JsonProperty("Data")]
        public string data { get; set; }
    }
}