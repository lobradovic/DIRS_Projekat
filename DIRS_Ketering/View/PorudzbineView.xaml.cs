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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DIRS_Ketering.View
{
    /// <summary>
    /// Interaction logic for PorudzbineView.xaml
    /// </summary>
    public partial class PorudzbineView : UserControl
    {
        public PorudzbineView()
        {
            InitializeComponent();
            IsVisibleChanged += (s, e) =>
            {
                if ((bool)e.NewValue && DataContext is PorudzbinaViewModel vm)
                    vm.prikaziPorudzbine();
            };
        }
    }
}
