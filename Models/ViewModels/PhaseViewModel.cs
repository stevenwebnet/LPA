using System.ComponentModel.DataAnnotations;

namespace LPA.Models.ViewModels
{
	public class PhaseViewModel
    {
		public int Id { get; set; }
		public string Libelle { get; set; }
		public int Ordre { get; set; }
	}
}
