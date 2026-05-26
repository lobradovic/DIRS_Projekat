using DIRS_Ketering.Data;
using DIRS_Ketering.Helpers;
using DIRS_Ketering.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DIRS_Ketering.ViewModels
{
    public class AdminViewModel:BaseViewModel
    {
        private ObservableCollection<Jelo> jela;
        private Jelo selektovanoJelo;
        private string naziv;
        private string opis;
        private decimal cena;


        public ObservableCollection<Jelo> Jela { get => jela; set => SetProperty(ref jela, value); }
        public Jelo SelektovanoJelo
        { 
            get => selektovanoJelo;
            set { 
                SetProperty(ref selektovanoJelo, value);
                if (value != null)
                {
                    Naziv = value.Naziv;
                    Opis = value.Opis;
                    Cena = value.Cena;
                }
            } 
        }
        public string Naziv { get => naziv; set => SetProperty(ref naziv, value); }
        public string Opis { get => opis; set => SetProperty(ref opis, value); }
        public decimal Cena { get => cena; set => SetProperty(ref cena, value); }

        public ICommand NovoJeloCommand { get; }
        public ICommand IzmeniJeloCommand { get; }
        public ICommand ObrisiJeloCommand { get; }
        public ICommand OcistiFormuCommand { get; }
        public ICommand AzurirajJeloCommand { get; }

        public AdminViewModel()
        {
            NovoJeloCommand = new RelayCommand(_ => novoJelo());
            ObrisiJeloCommand=new RelayCommand(_ => obrisiJelo());
            OcistiFormuCommand=new RelayCommand(_ => ocistiFormu());
            AzurirajJeloCommand=new RelayCommand(_=>azurirajJelo());
            ucitajJela();
        }

        public void ucitajJela()
        {
            using var db = new AppDbContext();
            Jela = new ObservableCollection<Jelo>(db.Jela.ToList());
        }
        public void novoJelo()
        {
            if(string.IsNullOrEmpty(naziv) || Cena<=0)
            {
                MessageBox.Show("Niste uneli sva potrebna polja!");
                return;
            }

            using var db = new AppDbContext();
            db.Jela.Add(new Jelo { Naziv = Naziv, Opis = Opis, Cena = Cena });
            db.SaveChanges();
            ucitajJela();
            ocistiFormu();
        }

        public void obrisiJelo()
        {
            if(SelektovanoJelo==null)
            {
                MessageBox.Show("Niste odabrali nijedno jelo!");
                return;
            }
            var potvrda = MessageBox.Show(
                            $"Da li ste sigurni da zelite da obrisete {SelektovanoJelo.Naziv}?",
                            "Potvrda brisanja",
                            MessageBoxButton.YesNo);

            if (potvrda != MessageBoxResult.Yes) return;

            using var db = new AppDbContext();
            var jelo = db.Jela.Find(SelektovanoJelo.Id);
            if (jelo == null) return;

            db.Jela.Remove(jelo);
            db.SaveChanges();

            ucitajJela();
            ocistiFormu();

        }
        public void azurirajJelo()
        {
            if(SelektovanoJelo==null)
            {
                MessageBox.Show("Niste odabrali nijedno jelo!");
                return;
            }
            var potvrda = MessageBox.Show(
                $"Da li ste sigurni da zelite da azurirate {SelektovanoJelo.Naziv}?",
                "Potvrda azuriranja",
                MessageBoxButton.YesNo);

            if (potvrda != MessageBoxResult.Yes) return;
            using var db = new AppDbContext();

            var jelo = db.Jela.Find(SelektovanoJelo.Id);
            if (jelo == null) return;

            jelo.Naziv = Naziv;
            jelo.Opis = Opis;
            jelo.Cena = Cena;
            db.SaveChanges();

            ucitajJela();
            ocistiFormu();

        }
        private void ocistiFormu()
        {
            SelektovanoJelo = null;
            Naziv = string.Empty;
            Opis = string.Empty;
            Cena = 0;
        }

    }
}
