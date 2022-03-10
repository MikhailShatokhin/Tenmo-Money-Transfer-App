using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserDao userDao;

        public UserController(IUserDao _userDao)
        {
            userDao = _userDao;
        }
        [HttpGet()]
        public ActionResult<List<User>> GetUsers()
        {
            List<User> users = userDao.GetUsers();
            return Ok(users);
        }
    }
}
