using Concept.Models;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Concept.Data
{
    public class LocalDataStore
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _js;

        public LocalDataStore(HttpClient httpClient, IJSRuntime js)
        {
            _httpClient = httpClient;
            _js = js;
        }

        public async Task SynchronizeAsync()
        {
            List<ConceptCategory>? conceptCategories = await _httpClient.GetFromJsonAsync<List<ConceptCategory>>("data/conceptCategories.json");

            if (conceptCategories == null)
                return;

            await PutAsync("metadata", "conceptCategories", JsonConvert.SerializeObject(conceptCategories));

            foreach (ConceptCategory concept in conceptCategories)
            {
                if (concept.DisplayName == "Yours")
                    continue;

                if (concept.SubCategories == null || !concept.SubCategories.Any())
                {
                    HttpResponseMessage responseMessage = await _httpClient.GetAsync($"data/{concept.FilePath}");
                    string content = await responseMessage.Content.ReadAsStringAsync();

                    await PutAsync("ConceptCategories", concept.FilePath, content);
                }
                else
                    await AddSubCategories(concept.SubCategories);
            }
        }

        //TODO: See if this cannot be cleaned up and/or handle string type edge case for T which I think doesn't work
        public async Task<T?> GetAsync<T>(string storeName, string key)
        {
            string value = await GetAsync<string>(storeName, (object)key);

            if (string.IsNullOrWhiteSpace(value))
                return default;

            T result = JsonConvert.DeserializeObject<T>(value);

            return result;
        }

        public async Task SaveAsync<T>(string storeName, object key, T value)
        {
            await PutAsync<T>(storeName, key, value);
        }

        private async Task AddSubCategories(List<ConceptCategory> conceptCategories)
        {
            foreach (ConceptCategory concept in conceptCategories)
            {
                if (concept.SubCategories == null || !concept.SubCategories.Any())
                {
                    HttpResponseMessage? responseMessage = await _httpClient.GetAsync($"data/{concept.FilePath}");
                    string concepts = await responseMessage.Content.ReadAsStringAsync();

                    await PutAsync("ConceptCategories", concept.FilePath, concepts);
                }
                else
                    await AddSubCategories(concept.SubCategories);
            }
        }

        ValueTask<T> GetAsync<T>(string storeName, object key) => _js.InvokeAsync<T>("localDataStore.get", storeName, key);

        ValueTask PutAsync<T>(string storeName, object key, T value) => _js.InvokeVoidAsync("localDataStore.put", storeName, key, value);

        ValueTask DeleteAsync(string storeName, object key) => _js.InvokeVoidAsync("localDataStore.delete", storeName, key);
    }
}
