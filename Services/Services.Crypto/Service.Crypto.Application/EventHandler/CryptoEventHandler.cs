using Infrastructure.Bus;
using Infrastructure.Domain;
using Service.Crypto.Domain;
using Lib.Crypto;
namespace Service.Crypto.Application
{
    public class CryptoEventHandler: Infrastructure.Bus.IEventHandler
    {
        private readonly ICryptoEntity _repo;
        public CryptoEventHandler(ICryptoEntity repo)
        {
            _repo = repo;
        }

        public void Handle(Infrastructure.Domain.Event @event){
            AddCredentialEvent addKey = @event as AddCredentialEvent;
            
            var aesKey = $"{addKey.MOBILE_NO}@{addKey.PIN}";
            var rawKey = $"{addKey.MOBILE_NO}#{addKey.PIN}#{addKey.LABEL}";
            var cipherService = new Encrypt(aesKey);
            var cipherKey = cipherService.ComputeSha256Hash(rawKey);
            var cipherValue = cipherService.URLEncodeAESEncryption(addKey.DATA);

            var newEntity = new CryptoEntity{Key = cipherKey, data = cipherValue};
            _repo.AddEntity(newEntity);
        }
    }
}