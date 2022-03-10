using System;
using System.Collections.Generic;
using TenmoClient.Models;
using TenmoClient.Services;

namespace TenmoClient
{
    public class TenmoApp
    {
        private readonly TenmoConsoleService console = new TenmoConsoleService();
        private readonly TenmoApiService tenmoApiService;

        public TenmoApp(string apiUrl)
        {
            tenmoApiService = new TenmoApiService(apiUrl);
        }

        public void Run()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                // The menu changes depending on whether the user is logged in or not
                if (tenmoApiService.IsLoggedIn)
                {
                    keepGoing = RunAuthenticated();
                }
                else // User is not yet logged in
                {
                    keepGoing = RunUnauthenticated();
                }
            }
        }

        private bool RunUnauthenticated()
        {
            console.PrintLoginMenu();
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 2, 1);
            while (true)
            {
                if (menuSelection == 0)
                {
                    return false;   // Exit the main menu loop
                }

                if (menuSelection == 1)
                {
                    // Log in
                    Login();
                    return true;    // Keep the main menu loop going
                }

                if (menuSelection == 2)
                {
                    // Register a new user
                    Register();
                    return true;    // Keep the main menu loop going
                }
                console.PrintError("Invalid selection. Please choose an option.");
                console.Pause();
            }
        }

        private bool RunAuthenticated()
        {
            console.PrintMainMenu(tenmoApiService.Username);
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 6);
            if (menuSelection == 0)
            {
                // Exit the loop
                return false;
            }

            if (menuSelection == 1)
            {
                // Get your current balance
                GetBalance();
                return true;
            }

            if (menuSelection == 2)
            {
                // View your past transfers
                GetTransfers();
                return true;
            }

            if (menuSelection == 3)
            {
                // View your pending requests
            }

            if (menuSelection == 4)
            {
                // Send TE bucks
                GetUsers();
                TransferMoney();
                return true;
            }

            if (menuSelection == 5)
            {
                // Request TE bucks
            }

            if (menuSelection == 6)
            {
                // Log out
                tenmoApiService.Logout();
                console.PrintSuccess("You are now logged out");
            }

            return true;    // Keep the main menu loop going
        }

        private void Login()
        {
            LoginUser loginUser = console.PromptForLogin();
            if (loginUser == null)
            {
                return;
            }

            try
            {
                ApiUser user = tenmoApiService.Login(loginUser);
                if (user == null)
                {
                    console.PrintError("Login failed.");
                }
                else
                {
                    console.PrintSuccess("You are now logged in");
                }
            }
            catch (Exception)
            {
                console.PrintError("Login failed.");
            }
            console.Pause();
        }

        private void Register()
        {
            LoginUser registerUser = console.PromptForLogin();
            if (registerUser == null)
            {
                return;
            }
            try
            {
                bool isRegistered = tenmoApiService.Register(registerUser);
                if (isRegistered)
                {
                    console.PrintSuccess("Registration was successful. Please log in.");
                }
                else
                {
                    console.PrintError("Registration was unsuccessful.");
                }
            }
            catch (Exception)
            {
                console.PrintError("Registration was unsuccessful.");
            }
            console.Pause();
        }

        //Get Balance Method
        private void GetBalance()
        {
            decimal balance = tenmoApiService.GetBalance(tenmoApiService.UserId);

            console.PrintSuccess($"Your current account balance is: {balance:C}");
            console.Pause();
        }

        //List User Method
        private void GetUsers()
        {
            Console.WriteLine("|-------------- Users --------------|");
            Console.WriteLine("|    Id | Username                  |");
            Console.WriteLine("|-------+---------------------------|");

            List<User> users = tenmoApiService.GetUsers();
            foreach (User user in users)
            {
                Console.WriteLine($"|  {user.UserId} | {user.Username.PadRight(26,' ')}|");                
            }
            Console.WriteLine("|-------+---------------------------|");
        }

        //Transfer History Get
        private void GetTransfers()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Transfers                                  ");
            Console.WriteLine("ID          From/To                 Amount ");
            Console.WriteLine("-------------------------------------------");

            List<Transfer> transfers = tenmoApiService.GetTransfers(tenmoApiService.UserId);
            HashSet<int> transferIds = new HashSet<int>();
            foreach (Transfer transfer in transfers)
            {
                transferIds.Add(transfer.transferId);
                string fromTo = null;
                string fromToText = null;
                if (transfer.accountFrom == (tenmoApiService.UserId+1000))
                {
                    fromTo = "To:  ";
                    fromToText = $"{transfer.stringAccountTo}";
                }
                else
                {
                    fromTo = "From:";
                    fromToText = $"{transfer.stringAccountFrom}";
                }
                
                Console.WriteLine($"{transfer.transferId.ToString().PadRight(12, ' ')}{fromTo}{PadBoth(fromToText, 18)}${transfer.amount.ToString().PadLeft(7,' ')}");
            }
            Console.WriteLine("-------------------------------------------");
            int choice = console.PromptForInteger("Please enter transfer ID to view details (0 to cancel): ", null);
            if (choice == 0)
            {
                return;
            }
            else if (!transferIds.Contains(choice))
            {
                console.PrintError("Not a valid transfer Id.");
                console.Pause();
            }
            else
            {
                foreach (Transfer transfer in transfers)
                {
                    if(transfer.transferId == choice)
                    {
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Transfer Details");
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine($"Id: {transfer.transferId}");
                        Console.WriteLine($"From: {transfer.stringAccountFrom}");
                        Console.WriteLine($"To: {transfer.stringAccountTo}");
                        Console.WriteLine($"Type: {transfer.stringTransferType}");
                        Console.WriteLine($"Status: {transfer.stringTransferStatus}");
                        Console.WriteLine($"Amount: {transfer.amount:C}");
                        console.Pause();
                    }
                }
            }
        }

        public string PadBoth(string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft).PadRight(length);
        }


        //Send Money Method
        private void TransferMoney()
        {
            int toUserId = console.PromptForInteger($"Id of the user you are sending to [0]", null);

            if (SameUser(toUserId) || !UserExists(toUserId))
            {
                return;
            }

            decimal sendAmount = console.PromptForDecimal($"Enter the amount to send", null);
            if (sendAmount <= 0)
            {
                console.PrintError("Send amount must be greater than 0.");
                console.Pause();
                return;
            }
            else if (sendAmount > tenmoApiService.GetBalance(tenmoApiService.UserId))
            {
                console.PrintError("Send amount must be less than your current balance.");
                console.Pause();
                return;
            }
            else
            {
                tenmoApiService.TransferMoney(toUserId, sendAmount, tenmoApiService.UserId);
                console.PrintSuccess("Money successfully sent!");
                console.Pause();
            }
        }
        private bool SameUser(int toUserId)
        {
            bool sameUser = false;
            List<User> users = tenmoApiService.GetUsers();
            foreach (User user in users)
            {
                if (toUserId == tenmoApiService.UserId)
                {
                    sameUser = true;
                }
            }
            if (sameUser)
            {
                console.PrintError("Cannot send money to yourself. Please try again.");
                console.Pause();
            }
            return sameUser;
        }
        public bool UserExists(int toUserId)
        {
            bool userExists = false;
            List<User> users = tenmoApiService.GetUsers();
            foreach (User user in users)
            {
                if (user.UserId == toUserId)
                {
                    userExists = true;
                }
            }
            if (!userExists)
            {
                console.PrintError("Invalid UserId.");
                console.Pause();
            }
            return userExists;
        }
    }
}
