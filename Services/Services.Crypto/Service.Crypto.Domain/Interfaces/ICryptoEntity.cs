using System;
using System.Collections.Generic;
namespace Service.Crypto.Domain
{
    public interface ICryptoEntity
    {
         CrytoEntity GetCryto(Guid id);
         IEnumerable<CrytoEntity> GetCryto(string mobileNo, int pin);
         void AddEntity(CrytoEntity entity);

    }
}