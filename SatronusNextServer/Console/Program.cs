using ConsoleClient;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleResourceOwnerFlowRefreshToken
{
    public class Program
    {

        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            var response = IdentityServer4Client.LoginAsync("Rei", "Rei").Result;

            await ResourceDataClient.SetBearer(response.AccessToken);
            await ResourceDataClient.SendFileToServer("D://23.txt");
            await ResourceDataClient.DownloadFile("23.txt");

            Console.Read();
        }
    }
}