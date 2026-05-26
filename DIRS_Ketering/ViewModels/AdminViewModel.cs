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
        private Jelo jelo;
        private string naziv;
        private string opis;
        private decimal cena;


        public ObservableCollection<Jelo> Jela { get => jela; set => SetProperty(ref jela, value); }
        public Jelo Jelo { get => jelo; set => SetProperty(ref jelo, value); }
        public string Naziv { get => naziv; set => SetProperty(ref naziv, value); }
        public string Opis { get => opis; set => SetProperty(ref opis, value); }
        public decimal Cena { get => cena; set => SetProperty(ref cena, value); }

        public ICommand NovoJeloCommand { get; }
        public ICommand IzmeniJeloCommand { get; }
        public ICommand ObrisiJeloCommand { get; }

        public AdminViewModel()
        {
            NovoJeloCommand = new RelayCommand(_ => novoJelo());
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
        private void ocistiFormu()
        {
            Jelo = null;
            Naziv = string.Empty;
            Opis = string.Empty;
            Cena = 0;
        }

    }
}
