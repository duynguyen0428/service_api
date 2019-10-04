using System;
namespace Service.Crypto.Application
{
    public interface ICryptoService
    {
        string EncryptValue(string aeskey,string rawstr);
        string DencryptValue(string aeskey,string ciphertext);
        Tuple<string,string> AddNew(AddNewCredentialCmd cmd); 
        string Get(CredentialQuery query);
    }
}