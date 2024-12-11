using System.Text;
using System.Net;
using System.Text.Json;
using TselevikMAUI.Models;

namespace TselevikMAUI.Services
{
    public class DataStoreWebItems : IDataStore<Item, string>
    {
        const string Url = "http://195.2.73.217:7092/api/Items";

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private HttpClient GetClient()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Item>>(result, options);
        }
        public async Task<Item> GetItemAsync(string id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + id);
            var Item = JsonSerializer.Deserialize<Item>(result, options);
            return await Task.FromResult(Item);
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            HttpClient client = GetClient();

            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(item),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var answer = JsonSerializer.Deserialize<Item>(
                await response.Content.ReadAsStringAsync(), options);
            bool result = answer != null;
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(item),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var answer = JsonSerializer.Deserialize<Item>(
                await response.Content.ReadAsStringAsync(), options);
            bool result = answer != null;
            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var answer = JsonSerializer.Deserialize<Item>(
               await response.Content.ReadAsStringAsync(), options);
            bool result = answer != null;
            return await Task.FromResult(result);
        }
    }
}
