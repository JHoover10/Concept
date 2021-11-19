namespace Concept.Models
{
    public class ConceptCategory
    {
        public string DisplayName { get; set; }
        public string FilePath { get; set; }
        private bool _enabled;
        public bool Enabled 
        {
            get { return _enabled; }

            set
            {
                if (SubCategories != null && SubCategories.Any())
                {
                    foreach (ConceptCategory category in SubCategories)
                    {
                        category.Enabled = value;
                    }
                }

                _enabled = value;
            }
        }
        public List<ConceptCategory> SubCategories { get; set; }
    }
}
