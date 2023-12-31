﻿namespace LPA.Data
{
    public class Rencontre
    {
        public int Id { get; set; }
        public DateTime DateDebut { get; set; }
        public int? ScroreEquipeDomicile { get; set; }
        public int? ScroreEquipeExterieur { get; set; }

        public int EquipeDomicileId { get; set; }
        public int EquipeExterieurId { get; set; }
        public int CompetitionId { get; set; }

        public ICollection<Pronostic> Pronostics { get; set;} = new List<Pronostic>();
        public Equipe EquipeDomicile { get; set; }
        public Equipe EquipeExterieur { get; set; }
        public Competition Competition { get; set; }
    }
}
