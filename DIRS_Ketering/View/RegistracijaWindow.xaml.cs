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
    /// Interaction logic for RegistracijaWindow.xaml
    /// </summary>
    public partial class RegistracijaWindow : Window
    {
        public RegistracijaWindow()
        {
            InitializeComponent();
            DataContext = new RegistracijaViewModel();
            PbLozinka.PasswordChanged += (s, e) => ((RegistracijaViewModel)DataContext).Lozinka = PbLozinka.Password;
        }
    }
}
