namespace LPA.Data
{
    public class Pronostic
    {
        public int Id { get; set; }
        public int ScoreEquipeDomicile { get; set; }
        public int ScoreEquipeExterieure { get; set; }
        public int? PointsAccumules { get; set; }
        public string ApplicationUserId { get; set; }

        public int RencontreId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Rencontre Rencontre { get; set; }
    }
}

