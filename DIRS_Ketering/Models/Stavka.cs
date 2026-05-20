using System;
using System.Collections.Generic;
using System.Text;

namespace DIRS_Ketering.Models
{
    public class Stavka
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public decimal TrenutnaCena { get; set; }
        public int PorudzbineId { get; set; }
        public int JeloId { get; set; }

        public Porudzbina Porudzbina { get; set; } = null!;
        public Jelo Jelo { get; set; } = null!;
    }
}
