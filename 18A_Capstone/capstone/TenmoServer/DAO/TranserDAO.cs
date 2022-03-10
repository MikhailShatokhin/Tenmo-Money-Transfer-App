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
                                          "VALUES (@transferTypeId, @transferStatusId, (select account_id from account where user_id = @accountFrom), (select account_id from account where user_id = @accountTo), @amount); ";
        private string sqlAddBalance = "UPDATE account SET balance = balance + @amount WHERE user_id = @userId";
        private string sqlMinusBalance = "UPDATE account SET balance = balance - @amount WHERE user_id = @userId";
        private string sqlListTransfers = "" +
            "select " +
            "   t.*,ts.transfer_status_desc, tt.transfer_type_desc, tu1.username [From], tu2.username [To] " +
            "from " +
            "   transfer t  	" +
            "   join transfer_status ts on ts.transfer_status_id = t.transfer_status_id 	" +
            "   join transfer_type tt on tt.transfer_type_id = t.transfer_type_id 	" +
            "   join account a1 on a1.account_id = t.account_from 	" +
            "   join account a2 on a2.account_id = t.account_to 	" +
            "   join tenmo_user tu1 on tu1.[user_id] = a1.user_id 	" +
            "   join tenmo_user tu2 on tu2.[user_id] = a2.user_id " +
            "where 	" +
            "   (a1.user_id = @userId or a2.user_id = @userId)";




        public TransferDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void TransferMoney(Transfer transfer)
        {
            try
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
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand(sqlAddBalance, conn);
                    cmd2.Parameters.AddWithValue("@amount", transfer.amount);
                    cmd2.Parameters.AddWithValue("@userId", transfer.accountTo);
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd3 = new SqlCommand(sqlMinusBalance, conn);
                    cmd3.Parameters.AddWithValue("@amount", transfer.amount);
                    cmd3.Parameters.AddWithValue("@userId", transfer.accountFrom);
                    cmd3.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

        }
        public List<Transfer> GetTransfers(int userId)
        {
            List<Transfer> result = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlListTransfers, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer u = GetTransferFromReader(reader);
                        result.Add(u);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return result;
        }

        private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer u = new Transfer();
            {
                u.transferId = Convert.ToInt32(reader["transfer_id"]);
                u.transferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
                u.transferStatusId = Convert.ToInt32(reader["transfer_status_id"]);
                u.accountFrom = Convert.ToInt32(reader["account_from"]);
                u.accountTo = Convert.ToInt32(reader["account_to"]);
                u.amount = Convert.ToDecimal(reader["amount"]);
                u.stringTransferStatus = Convert.ToString(reader["transfer_status_desc"]);
                u.stringTransferType = Convert.ToString(reader["transfer_type_desc"]);
                u.stringAccountFrom = Convert.ToString(reader["From"]);
                u.stringAccountTo = Convert.ToString(reader["To"]);
            }
            return u;
        }
    }
}


