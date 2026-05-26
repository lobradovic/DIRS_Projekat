using DIRS_Ketering.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DIRS_Ketering.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            var vm = (LoginViewModel)DataContext;
            PbLozinka.PasswordChanged += (s, e) => {
                if (DataContext is LoginViewModel vm)
                    vm.Lozinka = PbLozinka.Password;
            };
        }
    }
}
