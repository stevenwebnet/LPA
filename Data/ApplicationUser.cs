using Microsoft.AspNetCore.Identity;

namespace LPA.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateCreation { get; set; } = DateTime.Now;

        public ICollection<Pronostic> Pronostics { get; set; } = new List<Pronostic>();
        public ICollection<Tournoi> Tournois { get; set; } = new List<Tournoi>();
    }
}
