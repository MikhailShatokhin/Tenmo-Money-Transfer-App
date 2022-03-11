using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class GetAccountController : ControllerBase
    {
        private IGetAccountDAO tenmoDAO;

        public GetAccountController(IGetAccountDAO tenmoDAO)
        {
            this.tenmoDAO = tenmoDAO;
        }

        //GetBalance Method
        [HttpGet("{userId}")]
        public ActionResult<int> GetAccount(int userId)
        {
            int accountId = tenmoDAO.GetAccount(userId);
            return Ok(accountId);
        }




    }
}
