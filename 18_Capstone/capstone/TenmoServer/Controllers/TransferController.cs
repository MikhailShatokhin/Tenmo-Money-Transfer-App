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
        [HttpPut]
        //[AllowAnonymous]
        public void TransferMoney(Transfer transfer)
        {
            tenmoDAO.TransferMoney(transfer);
            
        }
    } 
}
