using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.DAO
{
    public interface IGetAccountDAO
    {
        int GetAccount(int userId);
    }
}
