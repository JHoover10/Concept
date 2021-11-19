using Concept.Models;
using System.ComponentModel;
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

        public MainLayoutViewModel()
        {
            AddConceptCategories();
        }

        private void AddConceptCategories()
        {
            _conceptCategories.Add(new ConceptCategory()
            {
                DisplayName = "Default",
                FilePath = "default.json",
                Enabled = true,
            });

            _conceptCategories.Add(new ConceptCategory()
            {
                DisplayName = "Pokemon",
                SubCategories = new List<ConceptCategory>()
                {
                    new ConceptCategory() 
                    {
                        DisplayName = "Generation 1",
                        FilePath = "pokemon/generation1.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "Generation 2",
                        FilePath = "pokemon/generation2.json"
                    }
                }
            });
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
