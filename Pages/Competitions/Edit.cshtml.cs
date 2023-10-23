using LPA.Data;
using LPA.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LPA.Pages.Competitions
{
	[Authorize(Roles = "Admin")]
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _dbContext;

		[BindProperty]
		public EditCompetitionViewModel EditCompetitionViewModel { get; set; }
		public List<SelectListItem> Lieux { get; set; }

		public EditModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void OnGet(int id)
		{
			var competition = _dbContext.Competition.Find(id);
			if (competition != null)
			{
				EditCompetitionViewModel = new EditCompetitionViewModel
				{
					Id = competition.Id,
					Libelle = competition.Libelle,
					LieuId = competition.LieuId,
				};
			}
			Lieux = _dbContext.Lieu.Select(l => new SelectListItem
			{
				Value = l.Id.ToString(),
				Text = l.Libelle
			}).ToList();
		}

		public void OnPostUpdate()
		{
			if (EditCompetitionViewModel != null)
			{
				var existingCompetition = _dbContext.Competition.Find(EditCompetitionViewModel.Id);
				if (existingCompetition != null)
				{
					existingCompetition.Libelle = EditCompetitionViewModel.Libelle;
					existingCompetition.LieuId = EditCompetitionViewModel.LieuId;
					_dbContext.SaveChanges();

					ViewData["Message"] = "La compétition à bien été modifiée !";

                    OnGet(EditCompetitionViewModel.Id);  // Rechargez les données pour la vue
                }
			}
		}

		public IActionResult OnPostDelete()
		{
			var existingCompetition = _dbContext.Competition.Find(EditCompetitionViewModel.Id);
			if (existingCompetition != null)
			{
				_dbContext.Competition.Remove(existingCompetition);
				_dbContext.SaveChanges();

				TempData["Message"] = $"La compétition '{existingCompetition.Libelle}' à bien été supprimée !";
				return RedirectToPage("/Competitions/List");
			}

			return Page();
		}
	}
}
