

using System.Diagnostics;
using AutoTransactionToken.Log;
using AutoTransactionToken.Simulator;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace AutoTransactionToken
{
    public static class Service
    {
        private static HttpClient http;
        public static void Init()
        {
            http = new HttpClient();
            http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");
        }
        public async static Task<string> Get(string url)
        {
            var response = await http.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        public async static Task<string> Post(string url, string data)
        {
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await http.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<bool> ClickElement(int id, string element)
        {
            string url = $"http://127.0.0.1:9123/ce";
            string data = "{\"index\":"+id+",\"element\":\""+element+"\"}";
            return await Service.Post(url, data) == "OK";
        }
        public static async Task<bool> ClickElement2(int id, string element)
        {
            string url = $"http://127.0.0.1:9123/ce2";
            string data = "{\"index\":"+id+",\"element\":\""+element+"\"}";
            return await Service.Post(url, data) == "OK";
        }
        public static async Task<bool> SetTextElement(int id,string text)
        {
            string url = $"http://127.0.0.1:9123/it";
            string data = "{\"index\":" + id + ",\"text\":\"" + text + "\"}";
            return await Service.Post(url, data) == "OK";
        }
        public static async Task<bool> SetTextElement2(int id,int elementIndex,string text)
        {
            string url = $"http://127.0.0.1:9123/it2";
            string data = "{\"index\":" + id + ",\"element_index\":" + elementIndex + ",\"text\":\"" + text + "\"}";
            return await Service.Post(url, data) == "OK";
        }
        public static async Task<bool> ClickElement3(int id,int elementIndex,string element)
        {
            string url = $"http://127.0.0.1:9123/ce3";
            string data = "{\"index\":" + id + ",\"element_index\":" + elementIndex + ",\"element\":\"" + element + "\"}";
            return await Service.Post(url, data) == "OK";
        }
        public static async Task<bool> ContainElement(int id, string element)
        {
            string url = $"http://127.0.0.1:9123/ct";
            string data = "{\"index\":"+id+",\"element\":\""+element+"\"}";
            return await Service.Post(url, data) == "OK";
        }

        public static async Task<string> GetKeyRegister(int id)
        {
            string url = $"http://127.0.0.1:9123/key/{id}";
            return await Service.Get(url);
        }

        public static async Task<string> GetAddress(int id)
        {
            string url = $"http://127.0.0.1:9123/address/{id}";
            return await Service.Get(url);
        }
    }
}
