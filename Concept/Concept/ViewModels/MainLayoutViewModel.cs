using Concept.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Concept.ViewModels
{
    public class MainLayoutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<ConceptCategory> _conceptCategories = new List<ConceptCategory>();
        private readonly HttpClient _httpClient;

        public List<ConceptCategory> ConceptCategories
        {
            get { return _conceptCategories; }
            set 
            { 
                _conceptCategories = value;
                OnPropertyChanged();
            }
        }

        public MainLayoutViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddConceptCategories()
        {
            List<ConceptCategory> conceptCategories = await _httpClient.GetFromJsonAsync<List<ConceptCategory>>("data/conceptCategories.json");

            _conceptCategories.AddRange(conceptCategories);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
