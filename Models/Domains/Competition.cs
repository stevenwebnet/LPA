namespace LPA.Models.Domains
{
    public class Competition
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int LieuId { get; set; }

        public Lieu Lieu { get; set; }
        public ICollection<Equipe> Equipes { get; set; } = new List<Equipe>();
        public ICollection<Tournoi> Tournois { get; set; } = new List<Tournoi>();
        public ICollection<Rencontre> Rencontres { get; set; } = new List<Rencontre>();
    }
}
