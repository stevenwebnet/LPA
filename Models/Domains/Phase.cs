namespace LPA.Models.Domains
{
    public class Phase
    {
        public int Id { get; set; }
        public string Libelle { get; set; }  
        public int Ordre { get; set; }

        public int CompetitionId { get; set; }

        public ICollection<Rencontre> Rencontres { get; set; }
        public Competition Competition { get; set; }
    }
}
