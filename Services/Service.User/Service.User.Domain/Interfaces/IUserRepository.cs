using Service.User.Domain;
using System.Collections.Generic;
namespace Service.User.Domain
{
    public interface IUserRepository
    {
        Account GetUser(int id);
        IEnumerable<Account> GetUsers();
        Account GetUser(string usrname, string pwd);
        void AddUser(Account usr);
        void UpdateUser(Account usr);
    }
}