using DIRS_Ketering.Helpers;
using DIRS_Ketering.Service;
using System.Windows;
using System.Windows.Input;

namespace DIRS_Ketering.ViewModels
{
    public class RegistracijaViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        private string _ime;
        private string _email;
        private string _lozinka;
        private string _greska;

        public string Ime
        {
            get => _ime;
            set => SetProperty(ref _ime, value);
        }

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

        public ICommand RegistrujCommand { get; }

        public RegistracijaViewModel()
        {
            _authService = new AuthService();
            RegistrujCommand = new RelayCommand(_ => Registruj());
        }

        private void Registruj()
        {
            if (string.IsNullOrEmpty(Ime) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Lozinka))
            {
                Greska = "Sva polja su obavezna.";
                return;
            }

            _authService.registracija(Ime, Email, Lozinka);
            MessageBox.Show("Registracija uspešna! Možete se prijaviti.");

            var login = new View.LoginWindow();
            login.Show();
            Application.Current.Windows[0].Close();
        }
    }
}