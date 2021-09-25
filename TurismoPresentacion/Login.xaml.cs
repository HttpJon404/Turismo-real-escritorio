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
using System.Windows.Shapes;
using EntidadServicio;
using MahApps.Metro.Controls;
using TurismoNegocio;

namespace TurismoPresentacion
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text=="admin")
            {
                MainWindow inicio = new MainWindow();
                this.Close();
                inicio.Show();

            }
        }

        private bool ValidarCampos()
        {
            if (txtEmail.Text!= string.Empty )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
