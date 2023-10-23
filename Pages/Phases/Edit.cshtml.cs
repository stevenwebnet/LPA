using LPA.Data;
using LPA.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LPA.Pages.Phases
{
	[Authorize(Roles = "Admin")]
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _dbContext;

		[BindProperty]
		public EditPhaseViewModel EditPhaseViewModel { get; set; }

		public EditModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void OnGet(int id)
		{
			var phase = _dbContext.Phase.Find(id);
			if (phase != null)
			{
				EditPhaseViewModel = new EditPhaseViewModel
				{
					Id = phase.Id,
					Libelle = phase.Libelle
				};
			}
		}

		public void OnPostUpdate()
		{
			if (EditPhaseViewModel != null)
			{
				var existingPhase = _dbContext.Phase.Find(EditPhaseViewModel.Id);
				if (existingPhase != null)
				{
					existingPhase.Libelle = EditPhaseViewModel.Libelle;
					_dbContext.SaveChanges();

					ViewData["Message"] = "La phase � bien �t� modifi�e !";
				}
			}
		}

		public IActionResult OnPostDelete()
		{
			var existingPhase = _dbContext.Phase.Find(EditPhaseViewModel.Id);
			if (existingPhase != null)
			{
				_dbContext.Phase.Remove(existingPhase);
				_dbContext.SaveChanges();

				TempData["Message"] = $"La phase '{existingPhase.Libelle}' � bien �t� supprim�e !";
				return RedirectToPage("/Phases/List");
			}

			return Page();
		}
	}
}
