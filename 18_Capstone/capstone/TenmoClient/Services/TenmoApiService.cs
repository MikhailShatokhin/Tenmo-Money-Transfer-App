using RestSharp;
using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl = "https://localhost:44315/";
        //protected static RestClient client = null;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        // Add methods to call api here...

        //GetBalance Method
        public decimal GetBalance(int userId)
        {
            //decimal balance = 0.00M;

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

    }
}
