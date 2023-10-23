using LPA.Data;
using LPA.Models.Domains;
using LPA.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace LPA.Pages.Phases
{
	[Authorize(Roles = "Admin")]
	public class AddModel : PageModel
    {
		private readonly ApplicationDbContext _dbContext;

		[BindProperty]
		public AddPhaseViewModel AddPhaseRequest { get; set; }

		public AddModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void OnGet() { }

		public IActionResult OnPost()
		{
			var phaseDomainModel = new Phase
			{
				Libelle = AddPhaseRequest.Libelle
			};

			_dbContext.Phase.Add(phaseDomainModel);
			_dbContext.SaveChanges();

			TempData["Message"] = $"La phase '{phaseDomainModel.Libelle}' à bien été ajoutée !";

			return RedirectToPage("/Phases/List");
		}
	}
}
