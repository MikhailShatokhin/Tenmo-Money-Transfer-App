using RestSharp;
using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

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

        public bool TransferMoney(int toUserId, decimal sendAmount, int fromUserId)
        {
            Transfer newTransfer = new Transfer();
            newTransfer.accountTo = toUserId;
            newTransfer.accountFrom = fromUserId;
            newTransfer.amount = sendAmount;

            RestRequest request = new RestRequest($"{ApiUrl}transfer");
            request.AddJsonBody(newTransfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);
            //IRestResponse<Transfer> response2 = client.Put<Transfer>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            //else if (!response2.IsSuccessful)
            //{
            //    throw new Exception("Error occurred - received non-success response: " + (int)response2.StatusCode);
            //}
            //else if (response2.ResponseStatus != ResponseStatus.Completed)
            //{
            //    throw new Exception("Error occurred - unable to reach server.");
            //}
            else
            {
                return true;
            }
        }

    }
}
