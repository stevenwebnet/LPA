using LPA.Data;
using LPA.Models.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LPA.Pages.Phases
{
	[Authorize(Roles = "Admin")]
	public class ListModel : PageModel
    {
		private readonly ApplicationDbContext _dbContext;
		public List<Phase> Phases { get; set; }

		public ListModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void OnGet()
		{
			Phases = _dbContext.Phase.ToList();
		}
	}
}
