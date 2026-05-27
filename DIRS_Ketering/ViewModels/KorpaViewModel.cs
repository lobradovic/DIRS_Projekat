using DIRS_Ketering.Data;
using DIRS_Ketering.Helpers;
using DIRS_Ketering.Models;
using DIRS_Ketering.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DIRS_Ketering.ViewModels
{
    public class KorpaViewModel : BaseViewModel
    {
        private static KorpaViewModel _instance;
        public static KorpaViewModel Instance => _instance ?? (_instance = new KorpaViewModel());

        private ObservableCollection<StavkaKorpe> stavke;
        private StavkaKorpe selektovanaStavka;

        private string adresaIsporuke;
        private DateTime datumIsporuke = DateTime.Now.AddDays(1);

        public string AdresaIsporuke
        {
            get => adresaIsporuke;
            set => SetProperty(ref adresaIsporuke, value);
        }

        public DateTime DatumIsporuke
        {
            get => datumIsporuke;
            set => SetProperty(ref datumIsporuke, value);
        }

        public ObservableCollection<StavkaKorpe> Stavke
        {
            get => stavke;
            set => SetProperty(ref stavke, value);
        }

        public StavkaKorpe SelektovanaStavka
        {
            get => selektovanaStavka;
            set => SetProperty(ref selektovanaStavka, value);
        }

        public decimal Ukupno => Stavke.Sum(s => s.Cena * s.Kolicina);

        public ICommand PotvrdiCommand { get; }
        public ICommand ObrisiCommand { get; }

        private KorpaViewModel()
        {
            Stavke = new ObservableCollection<StavkaKorpe>();
            PotvrdiCommand = new RelayCommand(_ => potvrdiNarudzbinu());
            ObrisiCommand = new RelayCommand(_ => obrisiStavku());
        }

        public void dodajStavku(Jelo jelo)
        {
            var postojeca = Stavke.FirstOrDefault(s => s.JeloId == jelo.Id);
            if (postojeca != null)
            {
                postojeca.Kolicina++;
                OnPropertyChanged(nameof(Ukupno));
                return;
            }

            Stavke.Add(new StavkaKorpe
            {
                JeloId = jelo.Id,
                Naziv = jelo.Naziv,
                Cena = jelo.Cena,
                Kolicina = 1
            });

            OnPropertyChanged(nameof(Ukupno));
        }

        private void obrisiStavku()
        {
            if (SelektovanaStavka == null) return;
            Stavke.Remove(SelektovanaStavka);
            OnPropertyChanged(nameof(Ukupno));
        }

        private void potvrdiNarudzbinu()
        {
            if (Stavke.Count == 0)
            {
                MessageBox.Show("Korpa je prazna.");
                return;
            }
            if (string.IsNullOrEmpty(AdresaIsporuke))
            {
                MessageBox.Show("Unesite adresu isporuke.");
                return;
            }

            using var db = new AppDbContext();

            var porudzbina = new Porudzbina
            {
                KorisnikId = SessionService.Instance.TrenutniKorisnik.Id,
                Status = Status.Narucena,
                AdresaIsporuke = AdresaIsporuke,
                DatumIsporuke = DatumIsporuke
            };

            foreach (var stavka in Stavke)
            {
                porudzbina.Stavke.Add(new Stavka
                {
                    JeloId = stavka.JeloId,
                    Kolicina = stavka.Kolicina,
                    TrenutnaCena = stavka.Cena
                });
            }

            db.Porudzbine.Add(porudzbina);
            db.SaveChanges();

            Stavke.Clear();
            OnPropertyChanged(nameof(Ukupno));
            MessageBox.Show("narudzbina je uspesno kreirana!");
        }
    }
}
