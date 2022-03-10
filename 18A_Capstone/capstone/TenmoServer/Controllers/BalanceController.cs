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
    [Authorize]
    public class BalanceController : ControllerBase
    {
        private IBalanceDAO tenmoDAO;
        
        public BalanceController(IBalanceDAO tenmoDAO)
        {
            this.tenmoDAO = tenmoDAO;
        }

        //GetBalance Method
        [HttpGet("{userId}")]
        public ActionResult<decimal> GetBalance(int userId)
        {
            decimal balance = tenmoDAO.GetBalance(userId);
            return Ok(balance);
        }


    }
}
