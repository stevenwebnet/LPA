using LPA.Data;
using LPA.Models.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace LPA.Pages.Lieux
{
	[Authorize(Roles = "Admin")]
	public class ListModel : PageModel
    {
		private readonly ApplicationDbContext _dbContext;
		public List<Lieu> Lieux { get; set; }

		public ListModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void OnGet()
        {
			Lieux = _dbContext.Lieu.ToList();
        }
    }
}
