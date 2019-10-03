using Service.User.Domain;
using System.Collections.Generic;
using System.Linq;
namespace Service.User.Data {
    public class UserRepository : IUserRepository {
        private readonly UserDbContext _context;
        public UserRepository (UserDbContext context) {
            _context = context;
        }
    
        public Account GetUser(int id)
        {
            return _context.Accounts.FirstOrDefault(u => u.ID == id);
        }
        public void AddUser(Account usr){
            _context.Accounts.Add(usr);
            _context.SaveChangesAsync();
        }
        public Account GetUser(string usrname, string pwd)
           => _context.Accounts.FirstOrDefault(u => u.Password == pwd && u.Username == usrname);
        
        public IEnumerable<Account> GetUsers()
        => _context.Accounts.ToList();
        public void UpdateUser(Account usr){
            _context.Update<Account>(usr);
            _context.SaveChanges();
        }

    }
}