using LPA.Data;
using LPA.Models.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LPA.Pages.Equipes
{
	[Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        // Ceci vous donnera une hiérarchie Lieu -> Competition -> Equipe
        public List<Lieu> LieuxWithCompetitionsAndTeams { get; set; }

        public ListModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            LieuxWithCompetitionsAndTeams = _dbContext.Lieu
                .Include(l => l.Competitions)
                .ThenInclude(c => c.Equipes)
                .Where(l => l.Competitions.Any(c => c.Equipes.Any()))
                .ToList();
        }
    }
}
