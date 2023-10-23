using LPA.Data;
using LPA.Models.Domains;
using LPA.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace LPA.Pages.Equipes
{
	[Authorize(Roles = "Admin")]
	public class AddModel : PageModel
    {
		private readonly ApplicationDbContext _dbContext;

		[BindProperty]
		public AddEquipeViewModel AddEquipeRequest { get; set; }
		public List<SelectListItem> Competitions { get; set; }

		public AddModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void OnGet() 
		{
			Competitions = _dbContext.Competition
				.OrderBy(c => c.Lieu.Libelle)
				.Select(c => new SelectListItem
				{
					Value = c.Id.ToString(),
					Text = c.Libelle + " - " + c.Lieu.Libelle
				})
				.ToList();
		}

		public IActionResult OnPost()
		{
			var equipeDomainModel = new Equipe
			{
				Libelle = AddEquipeRequest.Libelle,
				Logo = string.Empty,
				Competitions = _dbContext.Competition.Where(c => AddEquipeRequest.CompetitionsId.Contains(c.Id)).ToList()
			};

			_dbContext.Equipe.Add(equipeDomainModel);
			_dbContext.SaveChanges();

			TempData["Message"] = $"L'équipe '{equipeDomainModel.Libelle}' a bien été ajoutée !";

			return RedirectToPage("/Equipes/List");
		}
	}
}
