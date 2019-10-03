using System;
using Service.User.Domain;
using Service.User.Data;
using System.Collections.Generic;
namespace Service.User.Application
{
    public class UserServiceRepository : IUserService
    {
        private readonly IUserRepository _repo;

        public UserServiceRepository(IUserRepository repo)
        {
            _repo = repo;
        }
        public Account GetUser(string usrname,string pwd){
            var user = _repo.GetUser(usrname,pwd);
            return user;
        }
        public Account GetUser(int id)
           => _repo.GetUser(id);
        public IEnumerable<Account> GetUsers()
            => _repo.GetUsers();
        
        public Account AddUser(UserCreateCommand usr){
            var newUser = new Account{Username = usr.Username, Password = usr.Password};
            _repo.AddUser(newUser);
            return newUser;
        }

        public void UpdateUser(int usrid,UserUpdateCommand cmd){
            var user = _repo.GetUser(usrid);
            if(user is null)
                throw new ArgumentNullException(nameof(user));
            user.PIN = cmd.pin;
            user.mobile_no = cmd.mobile_no;
            _repo.UpdateUser(user);
        }
    }
}