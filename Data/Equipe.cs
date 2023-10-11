namespace LPA.Data
{
    public class Equipe
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Logo { get; set; }

        public ICollection<Competition> Competitions { get; set; } = new List<Competition>();
    }
}
