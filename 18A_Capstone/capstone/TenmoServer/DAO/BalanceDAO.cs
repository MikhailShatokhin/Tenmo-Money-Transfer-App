using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.DAO
{
    public class BalanceDAO : IBalanceDAO
    {
        private string connectionString;
        private string sqlGetBalance = "SELECT balance FROM account WHERE user_id = ";

        public BalanceDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public decimal GetBalance(int userId)
        {
            decimal balance = 0.00M;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetBalance + userId, conn);

                balance = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            return balance;
        }
    }
}
