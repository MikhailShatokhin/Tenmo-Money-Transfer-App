using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("request")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        private IRequestDAO tenmoDAO;

        public RequestController(IRequestDAO tenmoDAO)
        {
            this.tenmoDAO = tenmoDAO;
            //this.userId = userId;
        }

        //Transfer Method
        [HttpPost]
        public ActionResult RequestMoney(Transfer transfer)
        {
            tenmoDAO.RequestMoney(transfer);
            return Ok();

        }

        [HttpGet("{userId}")]
        public ActionResult<List<Transfer>> GetRequests(int userId)
        {
            List<Transfer> result = tenmoDAO.GetRequests(userId);
            return Ok(result);
        }

        [HttpPut("{transferId}")]
        public ActionResult Approve(Transfer transfer)
        {
            if(transfer.transferStatusId == 2)
            {
                tenmoDAO.Approve(transfer);
                return Ok();
            }
            else
            {
                tenmoDAO.Reject(transfer);
                return Ok();
            }
            
        }
        
    }
}
