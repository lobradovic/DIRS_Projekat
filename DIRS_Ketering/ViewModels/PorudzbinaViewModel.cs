using DIRS_Ketering.Data;
using DIRS_Ketering.Helpers;
using DIRS_Ketering.Models;
using DIRS_Ketering.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DIRS_Ketering.ViewModels
{
    public class PorudzbinaViewModel:BaseViewModel
    {
        private ObservableCollection<Porudzbina> porudzbine;
        private Porudzbina selektovanaPorudzbina;

        public ObservableCollection<Porudzbina> Porudzbine
        { 
            get => porudzbine;
            set=>SetProperty(ref porudzbine, value);
        }
        public Porudzbina SelektovanaPorudzbina
        {
            get => selektovanaPorudzbina;
            set => SetProperty(ref selektovanaPorudzbina, value);
        }
        public ICommand OtkaziPorudzbinuCommand{ get; }
        public PorudzbinaViewModel()
        {
            OtkaziPorudzbinuCommand = new RelayCommand(_ => otkaziPorudzbinu());
            prikaziPorudzbine();
        }

        public void prikaziPorudzbine()
        {
            using var db = new AppDbContext();
            var korisnikId = SessionService.Instance.TrenutniKorisnik.Id;
            Porudzbine = new ObservableCollection<Porudzbina>(
                db.Porudzbine.Include(p=>p.Stavke).ThenInclude(s=>s.Jelo)
                .Where(p=>p.KorisnikId==korisnikId).Where(p=>p.Status!=Status.Otkazana).ToList()
            );
        }

        private void otkaziPorudzbinu()
        {
            if (SelektovanaPorudzbina == null) return;
            if(SelektovanaPorudzbina.Status!=Status.Narucena)
            {
                MessageBox.Show("Ne mozete otkazati ovu rezervaciju!");
                return;
            }

            var potvrda = MessageBox.Show(
                "Da li zelite da otkazete ovu narudzbinu?", "Potvrda",
                MessageBoxButton.YesNo);

            if (potvrda != MessageBoxResult.Yes) return;

            using var db=new AppDbContext();
            var por = db.Porudzbine.Find(SelektovanaPorudzbina.Id);
            if (por == null) return;
            por.Status = Status.Otkazana;
            db.SaveChanges();
            prikaziPorudzbine();
        }
    }
}
