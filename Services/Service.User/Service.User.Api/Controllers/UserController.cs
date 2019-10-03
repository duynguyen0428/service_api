using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.User.Application;
using Service.User.Domain;
namespace Service.User.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;

        }
        [HttpGet("users")]
        public IEnumerable<Account> GetUser(){
            return _service.GetUsers();
        }
        [HttpGet("user/{id}")]
        public Account GetUser(int id){
            return _service.GetUser(id);
        }
        [HttpPost("user")]
        public IActionResult Create(UserCreateCommand usrcmd)
        {
            var user = _service.AddUser(usrcmd);
            return Ok(user);
        }
        [HttpPut("user/{id}")]
        public IActionResult Update(int id,[FromBody]UserUpdateCommand cmd)
        {
            _service.UpdateUser(id,cmd);

            return Ok();
        }
    }
}