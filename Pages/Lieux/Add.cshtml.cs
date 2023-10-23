using LPA.Data;
using LPA.Models.Domains;
using LPA.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace LPA.Pages.Lieux
{
	[Authorize(Roles = "Admin")]
	public class AddModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public AddModel(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public AddLieuViewModel AddLieuRequest { get; set; }
        public void OnGet()
        {
        }

		public IActionResult OnPost()
		{
            var lieuDomainModel = new Lieu
            {
                Libelle = AddLieuRequest.Libelle
            };
            _dbContext.Lieu.Add(lieuDomainModel);
            _dbContext.SaveChanges();

            TempData["Message"] = $"Le lieu '{lieuDomainModel.Libelle}' à bien été ajouté !";

            return RedirectToPage("/Lieux/List");
        }
	}
}
