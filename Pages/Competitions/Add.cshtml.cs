using LPA.Data;
using LPA.Models.Domains;
using LPA.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace LPA.Pages.Competitions
{
	[Authorize(Roles = "Admin")]
	public class AddModel : PageModel
    {
		private readonly ApplicationDbContext _dbContext;

		[BindProperty]
		public AddCompetitionViewModel AddCompetitionViewModel { get; set; }

		public List<SelectListItem> Lieux { get; set; }

		public AddModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void OnGet()
        {
			Lieux = _dbContext.Lieu.Select(l => new SelectListItem
			{
				Value = l.Id.ToString(),
				Text = l.Libelle
			}).ToList();
		}

		public IActionResult OnPost()
		{
			var competitionDomainModel = new Competition
			{
				Libelle = AddCompetitionViewModel.Libelle,
				LieuId = AddCompetitionViewModel.LieuId
			};

			_dbContext.Competition.Add(competitionDomainModel);
			_dbContext.SaveChanges();

			var phases = new List<Phase>();
			foreach (var phaseViewModel in AddCompetitionViewModel.Phases)
			{
				phases.Add(new Phase
				{
					Libelle = phaseViewModel.Libelle,
					Ordre = phaseViewModel.Ordre,
					CompetitionId = competitionDomainModel.Id
				});
			}

			_dbContext.Phase.AddRange(phases);
			_dbContext.SaveChanges();

			TempData["Message"] = $"La compétition '{competitionDomainModel.Libelle}' et ses phases ont bien été ajoutées !";

			return RedirectToPage("/Competitions/List");
		}
	}
}
