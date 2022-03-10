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
    [Route("transfer")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private ITransferDAO tenmoDAO;

        public TransferController(ITransferDAO tenmoDAO)
        {
            this.tenmoDAO = tenmoDAO;
        }

        //Transfer Method
        [HttpPost]
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
