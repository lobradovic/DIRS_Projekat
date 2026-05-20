using System;
using System.Collections.Generic;
using System.Text;

namespace DIRS_Ketering.Models
{
    public class Porudzbina
    {
        public int Id { get; set; }
        public DateTime VremeKreiranja { get; set; } = DateTime.Now;
        public Status Status { get; set; } = Status.Narucena;
        public int KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; } = null!;
        public ICollection<Stavka> Stavke { get; set; } = new List<Stavka>();

        public decimal Ukupno => Stavke.Sum(s => s.TrenutnaCena * s.Kolicina);
    }
}
