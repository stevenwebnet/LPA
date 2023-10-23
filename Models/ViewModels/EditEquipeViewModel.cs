using LPA.Models.Domains;

namespace LPA.Models.ViewModels
{
	public class EditEquipeViewModel
	{
		public int Id { get; set; }
		public string Libelle { get; set; }
		public string Logo { get; set; }
		public List<int> CompetitionsId { get; set; }
	}
}
