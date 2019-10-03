using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Service.Crypto.Domain
{
    public class CryptoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid ID { get; set; }
        [BsonElement("PIN")]
        [JsonProperty("PIN")]
        public int PIN { get; set; }
        [BsonElement("mobile_no")]
        [JsonProperty("Mobile_No")]
        public string mobile_no { get; set; }
        [BsonElement("data")]
        [JsonProperty("Data")]
        public string data { get; set; }
    }
}