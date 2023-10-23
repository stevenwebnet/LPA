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
		public AddCompetitionViewModel AddCompetitionRequest { get; set; }

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
				Libelle = AddCompetitionRequest.Libelle,
				LieuId = AddCompetitionRequest.LieuId
			};

			_dbContext.Competition.Add(competitionDomainModel);
			_dbContext.SaveChanges();

			TempData["Message"] = $"La compétition '{competitionDomainModel.Libelle}' à bien été ajoutée !";

			return RedirectToPage("/Competitions/List");
		}
	}
}
