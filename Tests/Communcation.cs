

using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MMLib.RapidPrototyping.Generators;
using Newtonsoft.Json.Linq;
using Tests.Abstraction;

namespace Tests
{
    public class Communcation :IPerfTest
    {
        public async Task Run(int i)
        {
            var ligen = new WordGenerator();
            dynamic product = new JObject();
            var client = new HttpClient();

            product.Text = string.Join(' ',ligen.Next(i).ToList());

            var text = product.ToString();

 
           

    
            for (int j = 0; j < 100; j++)
                {

                    var webRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44356/test")
                    {
                        Content = new StringContent(text, Encoding.UTF8, "application/json")
                    };

                    var response =await client.SendAsync(webRequest);

     
            }

        }
    }
}
