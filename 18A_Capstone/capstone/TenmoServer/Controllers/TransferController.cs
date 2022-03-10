using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {

        //private User userId;
        private ITransferDAO tenmoDAO;
        //private IUserDao UserDao;

        public TransferController(ITransferDAO tenmoDAO)
        {
            this.tenmoDAO = tenmoDAO;
            //this.userId = userId;
        }

        //Transfer Method
        [HttpPost]
        //[AllowAnonymous]
        public ActionResult TransferMoney(Transfer transfer)
        {
            tenmoDAO.TransferMoney(transfer);
            return Ok();

        }

        [HttpGet("{userId}")]
        public ActionResult<List<Transfer>> GetTransfers(int userId)
        {
            List<Transfer> result = tenmoDAO.GetTransfers(userId);
            return Ok(result);
        }
    }
}
