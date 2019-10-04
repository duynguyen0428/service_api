using System;
using Service.Crypto.Domain;
using System.Collections.Generic;
using MongoDB.Driver;
namespace Service.Crypto.Data
{
    public class CryptoEntityRepository : ICryptoEntity
    {
        private readonly  IMongoCollection<CryptoEntity> _collection;
        public CryptoEntityRepository(ICryptoDbContext context)
        {
            _collection = context?.CryptoCol ?? throw new ArgumentException(nameof(context));
        }
        //  public CryptoEntity GetCryto(string id) 
        //     => _collection.Find(e=>e.ID == id).FirstOrDefault();
         public CryptoEntity GetCryto(string key)
            => _collection.Find(e=>e.Key == key).FirstOrDefault();
         public void AddEntity(CryptoEntity entity){
             _collection.InsertOne(entity);
         }
    }
}