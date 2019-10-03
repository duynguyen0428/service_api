using System;
using MongoDB.Driver;
namespace Service.Crypto.Data
{
    public class CryptoDbContext
    {
        private readonly IMongoCollection<CryptoEntity> _cryptoCol;
        public CryptoDbContext(ICryptoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cryptoCol = database.GetCollection<CryptoEntity>(settings.CollectionName);
        }

        public IMongoCollection<CryptoEntity> CryptoCol => _cryptoCol ?? throw new ArgumentException(nameof(_cryptoCol));
    }
}