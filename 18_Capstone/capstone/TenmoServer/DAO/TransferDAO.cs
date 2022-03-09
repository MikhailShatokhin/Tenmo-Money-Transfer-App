using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferDAO : ITransferDAO
    {
        private string connectionString;
        private string sqlInsertTransfer = "INSERT INTO transfer (transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
                                          "VALUES (@transferTypeId, @transferStatusId, @accountFrom, @accountTo, @amount) ";
        private string sqlAddBalance = "UPDATE account SET balance = balance + @amount WHERE user_id = @userId";
        private string sqlMinusBalance = "UPDATE account SET balance = balance - @amount WHERE user_id = @userId";

        public TransferDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void TransferMoney(Transfer transfer)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlInsertTransfer, conn);
                cmd.Parameters.AddWithValue("@transferTypeId", transfer.transferTypeId);
                cmd.Parameters.AddWithValue("@transferStatusId", transfer.transferStatusId);
                cmd.Parameters.AddWithValue("@accountFrom", transfer.accountFrom);
                cmd.Parameters.AddWithValue("@accountTo", transfer.accountTo);
                cmd.Parameters.AddWithValue("@amount", transfer.amount);

                SqlCommand cmd2 = new SqlCommand(sqlAddBalance, conn);
                cmd2.Parameters.AddWithValue("@amount", transfer.amount);
                cmd2.Parameters.AddWithValue("@userId", transfer.accountTo);

                SqlCommand cmd3 = new SqlCommand(sqlMinusBalance, conn);
                cmd3.Parameters.AddWithValue("@amount", transfer.amount);
                cmd3.Parameters.AddWithValue("@userId", transfer.accountFrom);


            }
        }
    }
}
