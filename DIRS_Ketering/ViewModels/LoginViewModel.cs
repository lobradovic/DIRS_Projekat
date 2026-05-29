using DIRS_Ketering.Helpers;
using DIRS_Ketering.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;


namespace DIRS_Ketering.ViewModels
{
        public class LoginViewModel : BaseViewModel
        {
            private readonly IAuthService _authService;

            private string _email;
            private string _lozinka;
            private string _greska;

            public string Email
            {
                get => _email;
                set => SetProperty(ref _email, value);
            }

            public string Lozinka
            {
                get => _lozinka;
                set => SetProperty(ref _lozinka, value);
            }

            public string Greska
            {
                get => _greska;
                set => SetProperty(ref _greska, value);
            }

            public ICommand PrijavaCommand { get; }
            public ICommand OtvoriRegistracijuCommand { get; }

            public LoginViewModel()
            {
                _authService = new AuthService();
                PrijavaCommand = new RelayCommand(_ => Prijava());
                OtvoriRegistracijuCommand = new RelayCommand(_ => OtvoriRegistraciju());
            }

            private void Prijava()
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Lozinka))
                {
                    Greska = "Email i lozinka su obavezni.";
                    return;
                }

                var korisnik = _authService.prijava(Email, Lozinka);

                if (korisnik == null)
                {
                    Greska = "Pogrešan email ili lozinka.";
                    return;
                }

                SessionService.Instance.prijavi(korisnik);

                var main = new View.MainWindow();
                main.Show();
                Application.Current.Windows[0].Close();
            }

        private void OtvoriRegistraciju()
        {
            var reg = new View.RegistracijaWindow();
            reg.Show();
        }
    }
}
