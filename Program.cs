using System;
using System.Net;
using System.Threading.Tasks;

namespace PastebinRipper
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Insert raw pastebin link.");

            String URL = Console.ReadLine();
            
            Console.WriteLine("Insert path to save to.");

            String Path = Console.ReadLine();

            String[] URLs;

            using (WebClient Client = new WebClient())
                URLs = Client.DownloadString(new Uri(URL)).Split('\n');

            int Downloaded = 0;

            Parallel.For(0, URLs.Length, index =>
            {
                using (WebClient Client = new WebClient())
                {
                    String[] URLPieces = URLs[index].Split('.');

                    String Name = URLPieces[2].TrimStart('i', 't', 'c', 'o', 'm', '/') + $".{URLPieces[3].TrimEnd('\r')}";

                    Client.DownloadFile(new Uri(URLs[index]), $"{Path}{Name}");

                    Downloaded++;
                    
                    Console.WriteLine($"Progress: {Downloaded} of {URLs.Length}");
                }
            });
        }
    }
}