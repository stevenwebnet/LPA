using LPA.Models.Domains;
using Microsoft.AspNetCore.Mvc;

namespace LPA.Models.ViewModels
{
	public class EditCompetitionViewModel
	{
		public int Id { get; set; }
		public string Libelle { get; set; }
		public int LieuId { get; set; }
		[BindProperty]
		public List<PhaseViewModel> Phases { get; set; } = new List<PhaseViewModel> { };
	}
}
