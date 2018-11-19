using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BadRangeRequestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string url = @"http://localhost:5000/api/file";
            using (var client = new HttpClient())
            {
                // We download the file a first time to check everything works fine and get the file length
                var fileBytes = await client.GetByteArrayAsync(url);

                // We request a range after the end of the file
                client.DefaultRequestHeaders.Range = new RangeHeaderValue(fileBytes.Length, null);
                var response = await client.GetAsync(url);

                // We get a "500 Internal Server Error" instead of a 416
                Console.WriteLine($"{(int) response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
