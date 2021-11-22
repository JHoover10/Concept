using Concept.Models;
using Microsoft.JSInterop;

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

        public async Task SynchronizeAsync(List<ConceptCategory> conceptCategories)
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
