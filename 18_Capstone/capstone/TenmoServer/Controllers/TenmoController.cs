using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
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
    public class TenmoController : ControllerBase
    {
        //private User userId;
        private ITenmoDAO tenmoDAO;
        private IUserDao UserDao;

        public TenmoController(ITenmoDAO tenmoDAO)
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
