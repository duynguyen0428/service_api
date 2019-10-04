using System;
using System.Collections.Generic;
namespace Service.Crypto.Domain
{
    public interface ICryptoEntity
    {
        //  CryptoEntity GetCryto(string id);
         CryptoEntity GetCryto(string key);
         void AddEntity(CryptoEntity entity);

    }
}