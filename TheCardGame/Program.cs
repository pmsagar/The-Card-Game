using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TheCardGame.Models;
using TheCardGame.Service;

namespace TheCardGame
{
    class Program
    {

        public static void Main(string[] args)
        {
            IConfiguration configuration = Startup.Initialize(args);
            CardGameService cardGameService = new CardGameService(configuration);
            cardGameService.Run();
            Console.ReadKey();
        }



    }
}
