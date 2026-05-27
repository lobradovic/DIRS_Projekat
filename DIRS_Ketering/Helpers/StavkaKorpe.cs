using DIRS_Ketering.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIRS_Ketering.Helpers
{
    public class StavkaKorpe : BaseViewModel
    {
        public int JeloId { get; set; }
        public string Naziv { get; set; }
        public decimal Cena { get; set; }

        private int kolicina;
        public int Kolicina
        {
            get => kolicina;
            set => SetProperty(ref kolicina, value);
        }
    }
}
