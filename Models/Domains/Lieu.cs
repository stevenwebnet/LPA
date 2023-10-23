using System.ComponentModel.DataAnnotations;

namespace LPA.Models.Domains
{
    public class Lieu
    {
        public int Id { get; set; }
        public string Libelle { get; set; }

        public ICollection<Competition> Competitions { get; set; } = new List<Competition>();
    }
}
