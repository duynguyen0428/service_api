using Service.User.Domain;
using System.Collections.Generic;
namespace Service.User.Application
{
    public interface IUserService
    {
         IEnumerable<Account> GetUsers();
         Account AddUser(UserCreateCommand usr);
         Account GetUser(string usrname,string pwd);
         Account GetUser(int id);
         void UpdateUser(int usrid,UserUpdateCommand cmd);
        void AddCredential(int usrid,UserAddCredentialCmd cmd);
    }
}