using EntidadServicio;
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
using TurismoNegocio;

namespace TurismoPresentacion
{
    /// <summary>
    /// Lógica de interacción para AdmUsers.xaml
    /// </summary>
    public partial class AdmUsers : Page
    {
        public AdmUsers()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void CargarClientes()
        {
            UsuarioBl userBl = new UsuarioBl();
            List<Usuario> usuarios = userBl.GetUsers();
            dgClientes.ItemsSource = usuarios;
        }

        private void btnAbrirFlyout_Click(object sender, RoutedEventArgs e)
        {
            FlyAddUser.IsOpen = true;

        }

        private void btnCerrarFlyUser_Click(object sender, RoutedEventArgs e)
        {
            FlyAddUser.IsOpen = false;
        }
    }
}
