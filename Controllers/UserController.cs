using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Services;

namespace Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger,UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet]
        public ActionResult<List<User>> Get() =>
        Ok(_userService.Get());

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var idsSplit = id.Split(',');
            var user = _userService.Get(idsSplit);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("Create")]
        public ActionResult<List<User>> Create([FromBody] List<User> users)
        {
            var createdUsers = _userService.Create(users);
            // var doctorsFilter = Builders<User>.Filter.Eq(u => u.Profession, "Doctor");
            // return CreatedAtRoute("GetUser", new { id = users._id.ToString() }, users);
            return createdUsers;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User userIn)
        {
            var idsSplit = id.Split(',');
            var user = _userService.Get(idsSplit);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string ids)
        {
            var idsSplit = ids.Split(',');
            var user = _userService.Get(idsSplit);

            if (user == null)
            {
                return NotFound();
            }
            _userService.Remove(idsSplit);

            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _userService.RemoveAll();
            return NoContent();
        }
  
    }
}
