using System;
using Service.User.Domain;
using Service.User.Data;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
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

        public void AddCredential(int usrid, UserAddCredentialCmd cmd)
        {
            var user = _repo.GetUser(usrid);
            if(user is null)
                throw new ArgumentNullException(nameof(user));
            SendCmdAsync(user,cmd);
        }

        private void SendCmdAsync(Account usr,UserAddCredentialCmd cmd){
            var factory = new ConnectionFactory() { HostName = "localhost",UserName = "user", Password = "password" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);  
                channel.QueueDeclare(queue: "demo.queue.log",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                string message = JsonConvert.SerializeObject(new AddCredentialEvent(Guid.NewGuid(),cmd.label,usr.mobile_no,usr.PIN,cmd.value));
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "demo.exchange",
                                    routingKey: "demo.queue.*",
                                    basicProperties: null,
                                    body: body);
            }
        }
    }
}