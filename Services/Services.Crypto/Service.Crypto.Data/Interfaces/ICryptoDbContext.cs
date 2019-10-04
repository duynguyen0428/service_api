using System;
using Service.Crypto.Domain;
using MongoDB.Driver;
namespace Service.Crypto.Data
{
    public interface ICryptoDbContext
    {
         IMongoCollection<CryptoEntity> CryptoCol {get;}
    }
}