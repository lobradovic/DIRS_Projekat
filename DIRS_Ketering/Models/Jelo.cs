using System;
using System.Collections.Generic;
using System.Text;

namespace DIRS_Ketering.Models
{
    public class Jelo
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public decimal Cena { get; set; }

        public ICollection<Stavka> Stavke { get; set; } = new List<Stavka>();
    }
}
