using System;
using Lib.Crypto;
using Service.Crypto.Domain;
using Newtonsoft.Json;
namespace Service.Crypto.Application
{
    public class CryptoServiceRepository: ICryptoService
    {
        private readonly ICryptoEntity _repo;

        public CryptoServiceRepository(ICryptoEntity repo)
        {
            _repo = repo;
        }
        public string EncryptValue(string aeskey,string rawstr){
                return string.Empty;
        }
        public string DencryptValue(string aeskey,string ciphertext){
                return string.Empty;
        }
        public Tuple<string,string> AddNew(AddNewCredentialCmd cmd){
            var aesKey = $"{cmd.mobile_phone}@{cmd.mobile_phone}";
            var rawKey = $"{cmd.mobile_phone}#{cmd.pin}#{cmd.label}";
            var cipherService = new Encrypt(aesKey);
            var cipherKey = cipherService.ComputeSha256Hash(rawKey);
            var cipherValue = cipherService.URLEncodeAESEncryption(cmd.value);

            var newEntity = new CryptoEntity{Key = cipherKey, data = cipherValue};
            _repo.AddEntity(newEntity);
            return new Tuple<string,string>(rawKey,cipherKey);
        }

        public string Get(CredentialQuery query)
        {
            var aesKey = $"{query.mobile_phone}@{query.mobile_phone}";
            var rawKey = $"{query.mobile_phone}#{query.pin}#{query.label}";
            var cipherService = new Encrypt(aesKey);
            var decipherService = new Decrypt(aesKey);
            var cipherKey = cipherService.ComputeSha256Hash(rawKey);
           
            var entity = _repo.GetCryto(cipherKey);
            
            // return JsonConvert.SerializeObject(entity) ;
            return decipherService.URLDecodeAESEncryption(entity?.data) ;
        }
    }
}