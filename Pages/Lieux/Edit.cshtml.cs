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
	public class EditModel : PageModel
    {
		private readonly ApplicationDbContext _dbContext;

		[BindProperty]
		public EditLieuViewModel EditLieuViewModel { get; set; }

		public EditModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void OnGet(int id)
        {
			var lieu = _dbContext.Lieu.Find(id);
			if (lieu != null)
			{
				EditLieuViewModel = new EditLieuViewModel
				{
					Id = lieu.Id,
					Libelle = lieu.Libelle
				};
			}
        }

		public void OnPostUpdate() 
		{
			if (EditLieuViewModel != null)
			{
				var esxistingLieu = _dbContext.Lieu.Find(EditLieuViewModel.Id);
				if (esxistingLieu != null)
				{
					esxistingLieu.Libelle = EditLieuViewModel.Libelle;
					_dbContext.SaveChanges();

					ViewData["Message"] = "Le lieu à bien été modifié !";
				}
			}
		}

		public IActionResult OnPostDelete()
		{	
			var existingLieu = _dbContext.Lieu.Find(EditLieuViewModel.Id);
			if (existingLieu != null)
			{
				_dbContext.Lieu.Remove(existingLieu);
				_dbContext.SaveChanges();

				TempData["Message"] = $"Le lieu '{existingLieu.Libelle}' à bien été supprimé !";
				return RedirectToPage("/Lieux/List");
			}

			return Page();
		}
	}
}
