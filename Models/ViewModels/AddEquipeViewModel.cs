using LPA.Models.Domains;

namespace LPA.Models.ViewModels
{
	public class AddEquipeViewModel
	{
		public string Libelle { get; set; }
		public string Logo { get; set; }
		public List<int> CompetitionsId { get; set; }
	}
}
