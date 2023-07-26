using BLL.Abstractions;
using Core;
using Newtonsoft.Json;
using System.Text;

namespace BLL
{
    public class HttpService : IHttpService
    {
        private readonly ApiConfigurations _apiConfigurations;

        public HttpService(ApiConfigurations apiConfigurations)
        {
            _apiConfigurations = apiConfigurations;
        }

        public async Task<T?> GetObject<T>(string relativePath, IDictionary<string, string> query = null!)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(_apiConfigurations.DefaultApiUrl);

            HttpResponseMessage response = await client.GetAsync(relativePath + GetQueryString(query ?? new Dictionary<string, string>()));

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        private static string GetQueryString(IDictionary<string, string> query)
        {
            var result = new StringBuilder("?");

            var isFirst = true;

            foreach (KeyValuePair<string, string> nameValue in query)
            {
                if (isFirst)
                {
                    isFirst = false;
                } 
                else
                {
                    result.Append('&');
                }
                result.Append(EscapeString(nameValue.Key));
                result.Append('=');
                result.Append(EscapeString(nameValue.Value));
            }

            return result.ToString();
        }

        private static string EscapeString(string toEscape)
        {
            var result = new StringBuilder();
            foreach (char character in toEscape)
            {
                result.Append(character switch
                {
                    '&' => "&amp;",
                    '<' => "&lt;",
                    '>' => "&gt;",
                    '"' => "&quot;",
                    '\'' => "&apos;",
                    _ => character.ToString(),
                });
            }

            return result.ToString();
        }
    }
}
