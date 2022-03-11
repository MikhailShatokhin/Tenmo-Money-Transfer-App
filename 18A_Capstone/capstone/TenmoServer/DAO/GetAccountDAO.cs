using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.DAO
{
    public class GetAccountDAO : IGetAccountDAO
    {

        private string connectionString;
        private string sqlGetAccount = "SELECT  a.account_id FROM tenmo_user tu JOIN account a ON a.user_id = tu.user_id WHERE tu.user_id = @userId";

        public GetAccountDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int GetAccount(int userId)
        {
            int accountId = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlGetAccount, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                accountId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return accountId;
        }
    }
}
