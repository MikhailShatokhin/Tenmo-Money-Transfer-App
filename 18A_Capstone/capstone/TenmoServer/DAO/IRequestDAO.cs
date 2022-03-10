using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IRequestDAO
    {
        void RequestMoney(Transfer transfer);
        List<Transfer> GetRequests(int userId);
        void Approve(Transfer transfer);
        void Reject(Transfer transfer);
    }
}
