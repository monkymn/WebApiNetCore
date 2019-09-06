using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.DataProvider;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase {
        private IUserDataProvider userDataProvider;


        public UserController(IUserDataProvider _userDataProvider){
            this.userDataProvider = _userDataProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<Users>> Get(){
            return await this.userDataProvider.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> Get(int id){
            Console.WriteLine("Id Recibido: {0}", id);
            var us = await this.userDataProvider.GetUser(id);
            if(us == null){
                return  NotFound();
            }
            return us;
            //return await this.userDataProvider.GetUser(userId);
        }

        [HttpPost]
        public async Task Post([FromBody] Users user) {
            await this.userDataProvider.AddUser(user);
        }

        [HttpPut]
        public async Task Put(int userId, [FromBody]Users user) {
            await this.userDataProvider.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int userId) {
            await this.userDataProvider.DeleteUser(userId);
        }
    }
}