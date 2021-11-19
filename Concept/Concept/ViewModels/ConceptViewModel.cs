using Concept.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Concept.ViewModels
{
    public class ConceptViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _conceptOne;

        public string ConceptOne
        {
            get { return _conceptOne; }
            private set
            {
                _conceptOne = value;
                OnPropertyChanged();
            }
        }

        private List<string> _easyConcepts = new List<string>(_maxConceptsPerDifficulty);

        public List<string> EasyConcepts
        {
            get { return _easyConcepts; }
            private set
            {
                _easyConcepts = value;
                OnPropertyChanged();
            }
        }

        private List<string> _mediumConcepts = new List<string>(_maxConceptsPerDifficulty);

        public List<string> MediumConcepts
        {
            get { return _mediumConcepts; }
            private set
            {
                _mediumConcepts = value;
                OnPropertyChanged();
            }
        }

        private List<string> _hardConcepts = new List<string>(_maxConceptsPerDifficulty);

        public List<string> HardConcepts
        {
            get { return _hardConcepts; }
            private set
            {
                _hardConcepts = value;
                OnPropertyChanged();
            }
        }

        private const int _maxConceptsPerDifficulty = 3;
        private HttpClient _httpClient;

        public ConceptViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;

            for (int i = 0; i < _maxConceptsPerDifficulty; i++)
            {
                _easyConcepts.Add(string.Empty);
                _mediumConcepts.Add(string.Empty);
                _hardConcepts.Add(string.Empty);
            }
        }

        public async Task UpdateConceptsAsync(List<ConceptCategory> conceptCategories)
        {

            List<string> concepts = new List<string>(_maxConceptsPerDifficulty * 3);

            Dictionary<string, List<string>> conceptsToChooseFrom = new Dictionary<string, List<string>>()
            {
                {"Easy", new List<string>() },
                {"Medium", new List<string>() },
                {"Hard", new List<string>() },
            };

            await AddConceptsAsync(conceptsToChooseFrom, conceptCategories);

            UpdateConcepts(EasyConcepts, conceptsToChooseFrom, "Easy");
            UpdateConcepts(MediumConcepts, conceptsToChooseFrom, "Medium");
            UpdateConcepts(HardConcepts, conceptsToChooseFrom, "Hard");
        }

        private void UpdateConcepts(List<string> concepts, Dictionary<string, List<string>> conceptsToChooseFrom, string difficulty)
        {
            Random random = new Random();
            List<int> conceptIndexs = new List<int>(_maxConceptsPerDifficulty);
            int numberOfConcepts = conceptsToChooseFrom[difficulty].Count();

            //TODO: Find better way to handle this edge case
            if (numberOfConcepts < _maxConceptsPerDifficulty)
            {
                for (int i = 0; i < concepts.Count(); i++)
                {
                    concepts[i] = string.Empty;
                }

                return;
            }                

            do
            {
                int temp = random.Next(numberOfConcepts);

                if (!conceptIndexs.Contains(temp))
                    conceptIndexs.Add(temp);

            } while (conceptIndexs.Count() < _maxConceptsPerDifficulty);

            for (int i = 0; i < concepts.Count(); i++)
            {
                concepts[i] = conceptsToChooseFrom[difficulty][conceptIndexs[i]].ToString();
            }
        }

        private async Task AddConceptsAsync(Dictionary<string, List<string>> conceptsToChooseFrom, List<ConceptCategory> conceptCategories)
        {
            foreach (var item in conceptCategories)
            {
                if (item.Enabled && (item.SubCategories == null || !item.SubCategories.Any()))
                {
                    HttpResponseMessage? responseMessage = await _httpClient.GetAsync($"data/{item.FilePath}");
                    JObject concepts = JsonConvert.DeserializeObject<JObject>(await responseMessage.Content.ReadAsStringAsync());

                    conceptsToChooseFrom["Easy"].AddRange(concepts["Easy"].ToObject<List<string>>());
                    conceptsToChooseFrom["Medium"].AddRange(concepts["Medium"].ToObject<List<string>>());
                    conceptsToChooseFrom["Hard"].AddRange(concepts["Hard"].ToObject<List<string>>());
                }
                else if (item.Enabled && item.SubCategories != null && item.SubCategories.Any())
                {
                    await AddConceptsAsync(conceptsToChooseFrom, item.SubCategories);
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
