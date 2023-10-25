using LPA.Data;
using LPA.Models.Domains;
using LPA.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LPA.Pages.Competitions
{
	[Authorize(Roles = "Admin")]
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _dbContext;

		[BindProperty]
		public EditCompetitionViewModel EditCompetitionViewModel { get; set; }
		public List<SelectListItem> Lieux { get; set; }

		public EditModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void OnGet(int id)
		{
			var competition = _dbContext.Competition
								.Include(c => c.Phases)
								.FirstOrDefault(c => c.Id == id);
			if (competition != null)
			{
				EditCompetitionViewModel = new EditCompetitionViewModel
				{
					Id = competition.Id,
					Libelle = competition.Libelle,
					LieuId = competition.LieuId,
					Phases = competition.Phases.OrderBy(p => p.Ordre).Select(p => new PhaseViewModel
					{
						Id = p.Id,
						Libelle = p.Libelle,
						Ordre = p.Ordre
					}).ToList()
				};
			}
			Lieux = _dbContext.Lieu.Select(l => new SelectListItem
			{
				Value = l.Id.ToString(),
				Text = l.Libelle
			}).ToList();
		}

		public IActionResult OnPostUpdate()
		{
			if (EditCompetitionViewModel != null)
			{
				using (var transaction = _dbContext.Database.BeginTransaction())
				{
					try
					{
						var existingCompetition = _dbContext.Competition.Find(EditCompetitionViewModel.Id);
						if (existingCompetition != null)
						{
							existingCompetition.Libelle = EditCompetitionViewModel.Libelle;
							existingCompetition.LieuId = EditCompetitionViewModel.LieuId;

							foreach (var phaseViewModel in EditCompetitionViewModel.Phases)
							{
								if (phaseViewModel.Id == 0)
								{
									var newPhase = new Phase
									{
										Libelle = phaseViewModel.Libelle,
										Ordre = phaseViewModel.Ordre,
										CompetitionId = EditCompetitionViewModel.Id
									};
									_dbContext.Phase.Add(newPhase);
								}
								else
								{
									var existingPhase = _dbContext.Phase.Find(phaseViewModel.Id);
									if (existingPhase != null)
									{
										existingPhase.Libelle = phaseViewModel.Libelle;
										existingPhase.Ordre = phaseViewModel.Ordre;
									}
								}
							}
							_dbContext.SaveChanges();
							transaction.Commit();

							TempData["Message"] = "La compétition et ses phases ont bien été modifiées !";
							return RedirectToPage("/Competitions/Edit", new { id = EditCompetitionViewModel.Id });
						}
					}
					catch (Exception ex)
					{
						transaction.Rollback();
						TempData["Message"] = $"Une erreur est survenue : {ex.Message}";
						return Page();
					}
				}
			}
			return Page();
		}

		public IActionResult OnPostDelete()
		{
			var existingCompetition = _dbContext.Competition.Find(EditCompetitionViewModel.Id);
			if (existingCompetition != null)
			{
				_dbContext.Competition.Remove(existingCompetition);
				_dbContext.SaveChanges();

				TempData["Message"] = $"La compétition '{existingCompetition.Libelle}' à bien été supprimée !";
				return RedirectToPage("/Competitions/List");
			}

			return Page();
		}

		public IActionResult OnPostDeletePhase(int phaseId)
		{
			var existingPhase = _dbContext.Phase.Find(phaseId);
			if (existingPhase != null)
			{
				_dbContext.Phase.Remove(existingPhase);
				_dbContext.SaveChanges();

				//TempData["Message"] = $"La phase '{existingPhase.Libelle}' à bien été supprimée !";
				return new JsonResult(new { success = true });
			}
			return new JsonResult(new { success = false, message = "Erreur lors de la suppression de la phase." });
		}

		public IActionResult OnPostDeleteAllPhases(int competitionId)
		{
			var existingPhases = _dbContext.Phase.Where(p => p.CompetitionId == competitionId).ToList();
			if (existingPhases != null)
			{
				_dbContext.Phase.RemoveRange(existingPhases);
				_dbContext.SaveChanges();

				//TempData["Message"] = $"Les phases de la competition ont bien été supprimées !";
				return new JsonResult(new { success = true });
			}
			return new JsonResult(new { success = false, message = "Erreur lors de la suppression des phases." });
		}
	}
}
