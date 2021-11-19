namespace Concept.Models
{
    public class ConceptCategory
    {
        public string DisplayName { get; set; }
        public string FilePath { get; set; }
        public bool Enabled { get; set; }
        public List<ConceptCategory> SubCategories { get; set; }
    }
}
