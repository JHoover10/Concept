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

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
