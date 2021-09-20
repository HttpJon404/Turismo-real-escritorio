using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;

namespace TurismoPresentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            menu.IsOpen = true;
        }

        private void btnGestionDpto_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdmDepartamentos();
        }

        private async void btnApagar_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult resultado = await this.ShowMessageAsync("Confirmación", "¿Estás seguro que deseas cerrar la aplicación? "
                    , MessageDialogStyle.AffirmativeAndNegative);

            if (resultado == MessageDialogResult.Affirmative)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnListaDpto_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ListadoDptos();
        }


    }
}
