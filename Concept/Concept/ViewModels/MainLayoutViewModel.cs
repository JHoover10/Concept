using Concept.Data;
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

        public List<ConceptCategory> ConceptCategories
        {
            get { return _conceptCategories; }
            set 
            { 
                _conceptCategories = value;
                OnPropertyChanged();
            }
        }

        private readonly LocalDataStore _localDataStore;

        public MainLayoutViewModel(LocalDataStore localDataStore)
        {
            _localDataStore = localDataStore;
        }

        public async Task AddConceptCategories()
        {
            List<ConceptCategory> conceptCategories = await _localDataStore.GetAsync<List<ConceptCategory>>("metadata", "conceptCategories");

            _conceptCategories.AddRange(conceptCategories);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
