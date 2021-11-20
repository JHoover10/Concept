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
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "Generation 3",
                        FilePath = "pokemon/generation3.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "Generation 4",
                        FilePath = "pokemon/generation4.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "Generation 5",
                        FilePath = "pokemon/generation5.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "Generation 6",
                        FilePath = "pokemon/generation6.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "Generation 7",
                        FilePath = "pokemon/generation7.json"
                    },
                }
            });

            _conceptCategories.Add(new ConceptCategory()
            {
                DisplayName = "Disney Movies",
                SubCategories = new List<ConceptCategory>()
                {
                    new ConceptCategory()
                    {
                        DisplayName = "The Golden Era (1937 to 1942)",
                        FilePath = "disneyMovies/theGoldenEra.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "The Wartime Era (1942 to 1949)",
                        FilePath = "disneyMovies/theWartimeEra.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "The Silver Era (1950 to 1967)",
                        FilePath = "disneyMovies/theSilverEra.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "The Bronze Era (1970 to 1977)",
                        FilePath = "disneyMovies/theBronzeEra.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "The Dark Ages (1981 to 1988)",
                        FilePath = "disneyMovies/theDarkAges.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "The Renaissance Era (1989 to 1999)",
                        FilePath = "disneyMovies/theRenaissanceEra.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "The Experimental Era (1999 to 2008)",
                        FilePath = "disneyMovies/theExperimentalEra.json"
                    },
                    new ConceptCategory()
                    {
                        DisplayName = "The Revival Era (2009 to Present)",
                        FilePath = "disneyMovies/theRevivalEra.json"
                    },
                }
            });
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
