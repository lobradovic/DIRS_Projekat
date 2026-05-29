using DIRS_Ketering.Data;
using DIRS_Ketering.Helpers;
using DIRS_Ketering.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace DIRS_Ketering.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private ObservableCollection<Jelo> jela;
        private Jelo selektovanoJelo;

        public ObservableCollection<Jelo> Jela
        {
            get => jela;
            set => SetProperty(ref jela, value);
        }
        public Jelo SelektovanoJelo
        {
            get => selektovanoJelo;
            set => SetProperty(ref selektovanoJelo, value);
        }
        public ICommand DodajKorpuCommand { get; }

        public MenuViewModel()
        {
            DodajKorpuCommand = new RelayCommand(_ => dodajKorpu());
            ucitajJela();
        }

        private void dodajKorpu()
        {
            if (selektovanoJelo == null) return;
            KorpaViewModel.Instance.dodajStavku(SelektovanoJelo);
        }
        public void ucitajJela()
        {
            using var db = new AppDbContext();
            Jela = new ObservableCollection<Jelo>(db.Jela.ToList());
        }
    }
}
