using DIRS_Ketering.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DIRS_Ketering.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MenuViewModel MenuViewModel { get; }
        public AdminViewModel AdminViewModel { get; }
        public KorpaViewModel KorpaViewModel => KorpaViewModel.Instance;
        public PorudzbinaViewModel PorudzbinaViewModel { get; }
        public bool JeAdmin => SessionService.Instance.korisnikAdmin();

        public Visibility AdminVisibility => JeAdmin ? Visibility.Visible : Visibility.Collapsed;

        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();
            AdminViewModel = new AdminViewModel();
            PorudzbinaViewModel = new PorudzbinaViewModel();
        }
    }
}
