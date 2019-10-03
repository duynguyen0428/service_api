using System;
namespace Service.User.Domain
{
    public class Account
    {   
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PIN { get; set; }
        public string mobile_no { get; set; }
    }
  
}