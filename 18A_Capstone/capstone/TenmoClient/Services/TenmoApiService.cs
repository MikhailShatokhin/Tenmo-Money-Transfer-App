using RestSharp;
using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl = "https://localhost:44315/";

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        // Add methods to call api here...

        //GetBalance Method
        public decimal GetBalance(int userId)
        {
            RestRequest request = new RestRequest($"{ApiUrl}balance/{userId}");
            IRestResponse<decimal> response = client.Get<decimal>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }

        public List<User> GetUsers()
        {
            RestRequest request = new RestRequest($"{ApiUrl}user");
            IRestResponse<List<User>> response = client.Get<List<User>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }
        public User GetUserName(int userId)
        {
            RestRequest request = new RestRequest($"{ApiUrl}user/{userId}");
            IRestResponse<User> response = client.Get<User>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }

        public List<Transfer> GetRequests(int userId)
        {
            RestRequest request = new RestRequest($"{ApiUrl}request/{userId}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }

        public bool TransferMoney(int toUserId, decimal sendAmount, int fromUserId)
        {
            Transfer newTransfer = new Transfer();
            newTransfer.transferStatusId = 2;
            newTransfer.transferTypeId = 2;
            newTransfer.accountTo = toUserId;
            newTransfer.accountFrom = fromUserId;
            newTransfer.amount = sendAmount;

            RestRequest request = new RestRequest($"{ApiUrl}transfer");
            request.AddJsonBody(newTransfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return true;
            }
        }

        public bool RequestMoney(int fromUserId, decimal requestAmount, int toUserId)
        {
            Transfer newTransfer = new Transfer();
            newTransfer.transferStatusId = 1;
            newTransfer.transferTypeId = 1;
            newTransfer.accountTo = toUserId;
            newTransfer.accountFrom = fromUserId;
            newTransfer.amount = requestAmount;

            RestRequest request = new RestRequest($"{ApiUrl}request");
            request.AddJsonBody(newTransfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return true;
            }
        }

        public List<Transfer> GetTransfers(int userId)
        {
            RestRequest request = new RestRequest($"{ApiUrl}transfer/{userId}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }

        public void Approve(Transfer newApproval)
        {
            newApproval.transferStatusId = 2;
            newApproval.transferTypeId = 1;

            RestRequest request = new RestRequest($"{ApiUrl}request/{newApproval.transferId}");
            request.AddJsonBody(newApproval);
            IRestResponse<Transfer> response = client.Put<Transfer>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            } 
            else
            {
                return;
            }
        }

        public void Reject(Transfer newRejection)
        {
            newRejection.transferStatusId = 3;
            newRejection.transferTypeId = 1;

            RestRequest request = new RestRequest($"{ApiUrl}request/{newRejection.transferId}");
            request.AddJsonBody(newRejection);
            IRestResponse<Transfer> response = client.Put<Transfer>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return;
            }
        }
    }
}
