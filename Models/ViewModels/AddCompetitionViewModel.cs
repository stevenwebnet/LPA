using LPA.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LPA.Models.ViewModels
{
	public class AddCompetitionViewModel
	{
		public string Libelle { get; set; }
		public int LieuId { get; set; }
		[BindProperty]
		public List<PhaseViewModel> Phases { get; set; } = new List<PhaseViewModel>();
	}
}
