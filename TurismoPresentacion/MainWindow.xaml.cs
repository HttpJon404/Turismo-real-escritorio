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
            menu.IsOpen = true;
            swTema.Content = "Oscuro";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            menu.IsOpen = true;
        }

        private void btnGestionDpto_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdmDeptos();
            imgFondo.Visibility = Visibility.Hidden;
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


        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdmUsers();
            imgFondo.Visibility = Visibility.Hidden;
        }

        private void btnListaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdmDepartamentos();
            imgFondo.Visibility = Visibility.Hidden;
        }



        private void swTema_Toggled(object sender, RoutedEventArgs e)
        {
            //Cambiar tema
            if (swTema.IsOn == true)
            {
                ThemeManager.Current.ChangeTheme(Application.Current, "Dark.Orange");
                swTema.Content = "Normal";

            }
            else
            {
                ThemeManager.Current.ChangeTheme(Application.Current, "Light.Orange");
                swTema.Content = "Oscuro";
            }
        }
    }
}
