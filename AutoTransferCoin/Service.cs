

using System.Web;

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
        public static async Task<bool> ClickElement(int id, string element)
        {
            string url = HttpUtility.UrlEncode($"http://localhost:9123/click_element/{id}/{element}");
            return await Service.Get(url) == "OK";
        }
    }
}
