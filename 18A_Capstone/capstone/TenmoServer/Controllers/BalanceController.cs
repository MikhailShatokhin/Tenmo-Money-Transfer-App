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
    [Route("balance")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        //private User userId;
        private IBalanceDAO tenmoDAO;
        //private IUserDao UserDao;

        public BalanceController(IBalanceDAO tenmoDAO)
        {
            this.tenmoDAO = tenmoDAO;
            //this.userId = userId;
        }

        //GetBalance Method
        [HttpGet("{userId}")]
        //[AllowAnonymous]
        public ActionResult<decimal> GetBalance(int userId)
        {
            decimal balance = tenmoDAO.GetBalance(userId);
            return Ok(balance);
        }


    }
}
