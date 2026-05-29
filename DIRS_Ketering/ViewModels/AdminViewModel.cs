using DIRS_Ketering.Data;
using DIRS_Ketering.Helpers;
using DIRS_Ketering.Models;
using DIRS_Ketering.Service;
using Microsoft.EntityFrameworkCore;
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

        public ObservableCollection<Porudzbina> porudzbine;
        public Porudzbina selektovanaPorudzbina;

        public ObservableCollection<Porudzbina> Porudzbine
        {
            get => porudzbine;
            set=>SetProperty(ref porudzbine,value);
        }
        public Porudzbina SelektovanaPorudzbina
        {
            get => selektovanaPorudzbina;
            set=>SetProperty(ref selektovanaPorudzbina,value);
        }

        private void prikaziRezervacijeAdmin()
        {
            using var db = new AppDbContext();
          
            Porudzbine = new ObservableCollection<Porudzbina>(
                db.Porudzbine.Include(p => p.Stavke).ThenInclude(s => s.Jelo).ToList()
            );
        }
        public Status SelektovaniStatus { get; set; }
        public Array Statusi => Enum.GetValues(typeof(Status));
        private void promeniStatus()
        {
            if (SelektovanaPorudzbina == null) return;

            using var db = new AppDbContext();
            var por = db.Porudzbine.Find(SelektovanaPorudzbina.Id);
            if (por == null) return;
            por.Status = SelektovaniStatus;
            db.SaveChanges();
            prikaziRezervacijeAdmin();
        }
        public ICommand PromeniStatusCommand { get; }


        DbJsonExport dbJson=new DbJsonExport();
        public ICommand ExportDb { get; }

        PDFService pdfService = new PDFService();
        public ICommand ExportPDFCommand { get; }
        public AdminViewModel()
        {
            NovoJeloCommand = new RelayCommand(_ => novoJelo());
            ObrisiJeloCommand = new RelayCommand(_ => obrisiJelo());
            OcistiFormuCommand = new RelayCommand(_ => ocistiFormu());
            AzurirajJeloCommand = new RelayCommand(_ => azurirajJelo());
            PromeniStatusCommand= new RelayCommand(_ => promeniStatus());
            ExportPDFCommand=new RelayCommand(_=>pdfService.exportPDF());
            ExportDb = new RelayCommand(_ => dbJson.Export());
            prikaziRezervacijeAdmin();
            ucitajJela();
        }

    }
}
