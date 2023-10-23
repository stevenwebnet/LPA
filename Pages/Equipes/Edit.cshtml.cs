using LPA.Data;
using LPA.Models.Domains;
using LPA.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LPA.Pages.Equipes
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        [BindProperty]
        public EditEquipeViewModel EditEquipeViewModel { get; set; }
        public List<SelectListItem> Competitions { get; set; }

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(int id)
        {
            var equipe = _dbContext.Equipe
                .Include(e => e.Competitions)
                .SingleOrDefault(e => e.Id == id);
            if (equipe != null)
            {
                EditEquipeViewModel = new EditEquipeViewModel
                {
                    Id = equipe.Id,
                    Libelle = equipe.Libelle,
                    Logo = equipe.Logo,
                    CompetitionsId = equipe.Competitions.Select(c => c.Id).ToList(),
                };
            }
            Competitions = _dbContext.Competition
                .OrderBy(c => c.Lieu.Libelle)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Libelle + " - " + c.Lieu.Libelle
                })
                .ToList();
        }

        public void OnPostUpdate()
        {
            if (EditEquipeViewModel != null)
            {
                var existingEquipe = _dbContext.Equipe.Include(e => e.Competitions).FirstOrDefault(e => e.Id == EditEquipeViewModel.Id);
                if (existingEquipe != null)
                {
                    // Mettez à jour les propriétés simples de l'équipe
                    existingEquipe.Libelle = EditEquipeViewModel.Libelle;
                    existingEquipe.Logo = string.Empty;

                    // Supprimez les associations existantes
                    var currentCompetitions = existingEquipe.Competitions.ToList();
                    foreach (var competition in currentCompetitions)
                    {
                        existingEquipe.Competitions.Remove(competition);
                    }

                    // Ajoutez les nouvelles associations
                    var newCompetitions = _dbContext.Competition.Where(c => EditEquipeViewModel.CompetitionsId.Contains(c.Id)).ToList();
                    foreach (var competition in newCompetitions)
                    {
                        existingEquipe.Competitions.Add(competition);
                    }

                    // Sauvegardez les modifications dans la base de données
                    _dbContext.SaveChanges();

                    ViewData["Message"] = "L'équipe a bien été modifiée !";

                    OnGet(EditEquipeViewModel.Id);  // Rechargez les données pour la vue
                }
            }
        }


        public IActionResult OnPostDelete()
        {
            var existingEquipe = _dbContext.Equipe.Find(EditEquipeViewModel.Id);
            if (existingEquipe != null)
            {
                _dbContext.Equipe.Remove(existingEquipe);
                _dbContext.SaveChanges();

                TempData["Message"] = $"L'équipe '{existingEquipe.Libelle}' a bien été supprimée !";
                return RedirectToPage("/Equipes/List");
            }

            return Page();
        }
    }
}
