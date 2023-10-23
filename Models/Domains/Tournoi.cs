namespace LPA.Models.Domains
{
    public class Tournoi
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string CodeInvitation { get; set; }
        public int CompetitionId { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
        public Competition Competition { get; set; }
    }
}
