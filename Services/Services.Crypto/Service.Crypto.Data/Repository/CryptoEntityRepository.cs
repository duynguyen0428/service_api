using System;
namespace Service.Crypto.Data.Repository
{
    public class CryptoEntityRepository : ICrytoEntity
    {
        private readonly  IMongoCollection<CryptoEntity> _collection;
        public CryptoEntityRepository(CryptoDbContext context)
        {
            _collection = context?.CryptoCol ?? throw new ArgumentException(nameof(context));
        }
         public CrytoEntity GetCryto(Guid id) 
            => _collection.Find(e=>e.ID == id).FirstOrDefault();
         public IEnumerable<CrytoEntity> GetCryto(string mobileNo, int pin)
            => _collection.Find(e=>e.Mobile_No == mobileNo && e.PIN == pin).FirstOrDefault();
         public void AddEntity(CrytoEntity entity){
             _collection.InsertOne(entity);
         }
    }
}