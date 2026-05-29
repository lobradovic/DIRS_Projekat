using System;
using System.Collections.Generic;
using System.Text;

namespace DIRS_Ketering.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Lozinka { get; set; } = string.Empty;
        public Rola Rola { get; set; } = Rola.Klijent;

        public ICollection<Porudzbina> Porudzbine { get; set; } = new List<Porudzbina>();
    }
}
