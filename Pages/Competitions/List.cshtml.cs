using LPA.Data;
using LPA.Models.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace LPA.Pages.Competitions
{
	[Authorize(Roles = "Admin")]
	public class ListModel : PageModel
    {
		private readonly ApplicationDbContext _dbContext;
		public List<IGrouping<Lieu, Competition>> CompetitionsByLieux { get; set; }
        public ListModel(ApplicationDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public void OnGet()
        {
			CompetitionsByLieux = _dbContext.Competition.GroupBy(c => c.Lieu).ToList();
        }
    }
}
