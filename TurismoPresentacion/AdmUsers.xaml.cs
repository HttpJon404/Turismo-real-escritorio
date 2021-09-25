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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

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
            dgClientes.IsReadOnly = true;
            ValidacionesInput();
        }
        private void ValidacionesInput()
        {
            txtCelular.MaxLength = 9;
            txtCelularEdit.MaxLength = 9;
            txtEdad.MaxLength = 3;
            txtEdadEdit.MaxLength = 3;
            txtRut.MaxLength = 10;
            txtRutEdit.MaxLength = 10;
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

        public void CerrarRegistro()
        {
            FlyAddUser.IsOpen = false;
            limpiarCampos();
        }
        private void btnCerrarFlyUser_Click(object sender, RoutedEventArgs e)
        {
            CerrarRegistro();
        }
        public bool FormularioValido()
        {
            if (txtNombres.Text != string.Empty && txtRut.Text != string.Empty&& txtApellidos.Text != string.Empty &&
            txtEdad.Text != string.Empty && txtDireccion.Text != string.Empty &&
            txtEmail.Text != string.Empty && txtCelular.Text != string.Empty && cboComuna.SelectedIndex !=-1 && cboRegion.SelectedIndex !=-1 &&
            cboGenero.SelectedIndex !=-1 && cboRol.SelectedIndex !=-1)
            {

                int edadV = int.Parse(txtEdad.Text);

                if (edadV >= 18 && edadV <= 100)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Debe ingresar una edad válida", "Error en edad", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtEdad.BorderBrush = Brushes.Red;
                    return false;
                }


            }
            else
            {
                MessageBox.Show("Debe llenar el formulario", "Error en edad", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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
            cboComuna.SelectedIndex = -1;
            cboRegion.SelectedIndex = -1;
            cboRol.SelectedIndex = -1;
            cboGenero.SelectedIndex = -1;

        }
        private void btnGuardarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (FormularioValido())
            {
                //UsuarioBl userBl = new UsuarioBl();
                //string nombres = "Jonatan Levi";
                //string apellidos = "Pérez Jerez";
                //decimal edad = 23;
                //string rut = "19782890-0";
                //string id_genero = "1";
                //decimal id_comuna = 2;
                //decimal id_region = 1;
                //string direccion = "Calle falsa 14";
                //string email = "jo.perezj@duocuc.cl";
                //string celular = "94743765";
                //string contrasena = "123123";
                //int id_rol = 1;

                //var resp = UsuarioBl.GetInstance().RegistrarUsuario(nombres, apellidos, edad, rut, id_genero, id_comuna, id_region, direccion, email, celular, contrasena, id_rol);
                //CerrarRegistro();
            }





        }

        //Validacion inputs
        private void SoloNumeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void OnlyNumberRut(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9-K-]+").IsMatch(e.Text);
        }

        private void Onlynumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }



        private void btnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            UsuarioBl userBl = new UsuarioBl();
            List<Usuario> usuarios = userBl.GetUsers();
            int i = 0;
            if (dgClientes.SelectedIndex != -1)
            {
                i = dgClientes.SelectedIndex;
                
                if (i >= 0)
                {
                    decimal idUsuario;
                    idUsuario = usuarios[i].id;
                    FlyEditUser.IsOpen = true;
                    var usuario = userBl.GetUserId(idUsuario);
                    foreach (var u in usuario)
                    {
                        txtRutEdit.Text = u.rut;
                        txtNombresEdit.Text = u.nombres;
                        txtApellidosEdit.Text = u.apellidos;
                        txtEdadEdit.Text = u.edad.ToString();
                        txtCelularEdit.Text = u.celular;
                        txtDireccionEdit.Text = u.direccion;
                        txtEmailEdit.Text = u.correo;
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario en la tabla para editarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsuarioBl userBl = new UsuarioBl();
            var usuario = userBl.GetUserId(45);
            foreach(var u in usuario)
            {
                Console.WriteLine(u.nombres);
            }
            
        }

        private void btnCerrarFlyEditUser_Click(object sender, RoutedEventArgs e)
        {
            FlyEditUser.IsOpen = false;
            txtNombresEdit.Text = string.Empty;
            txtRutEdit.Text = string.Empty;
            txtApellidosEdit.Text = string.Empty;
            txtEdadEdit.Text = string.Empty;
            txtDireccionEdit.Text = string.Empty;
            txtEmailEdit.Text = string.Empty;
            txtCelularEdit.Text = string.Empty;
            
        }


    }
}
