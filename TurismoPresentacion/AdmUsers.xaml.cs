using EntidadServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            limpiarCampos();
        }

        public void limpiarCampos()
        {
            txtNombres.Text = string.Empty;
            txtRut.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCelular.Text = string.Empty;

        }
        private void btnGuardarUsuario_Click(object sender, RoutedEventArgs e)
        {
            UsuarioBl userBl = new UsuarioBl();
            string nombres = "Jonatan Levi";
            string apellidos = "Pérez Jerez";
            decimal edad = 23;
            string rut = "19782890-0";
            string id_genero = "1";
            decimal id_comuna = 2;
            decimal id_region = 1;
            string direccion = "Calle falsa 14";
            string email = "jo.perezj@duocuc.cl";
            string celular = "94743765";
            string contrasena = "123123";
            int id_rol = 1;

            var resp = UsuarioBl.GetInstance().RegistrarUsuario(nombres, apellidos, edad, rut, id_genero, id_comuna, id_region, direccion, email, celular, contrasena, id_rol);
            
        }

        private void OnlyNumberRut(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9-K-]+").IsMatch(e.Text);
        }

        private void Onlynumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9-K-]+").IsMatch(e.Text);
        }
    }
}
