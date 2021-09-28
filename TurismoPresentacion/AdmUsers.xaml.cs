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
        private void CargarRegiones()
        {
            UbicacionBl ubi = new UbicacionBl();
            List<GetComunas> comunas = ubi.GetRegion();
            cboRegion.ItemsSource = comunas;
            cboRegion.SelectedValuePath = "id_region";
            cboRegion.DisplayMemberPath = "nombre_region";
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
            List<UsuarioTabla> usuarios = userBl.UsuariosGrid();
            dgClientes.ItemsSource = usuarios;
        }

        private void btnAbrirFlyout_Click(object sender, RoutedEventArgs e)
        {
            FlyAddUser.IsOpen = true;
            CargarRegiones();

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
                MessageBox.Show("Debe llenar el formulario", "Completar formulario", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private string CapturarEstado()
        {
            if (cboEstado.SelectedIndex==0)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        private string CapturarGenero()
        {
            if (cboGenero.SelectedIndex==0)
            {
                //Masculino
                return "1";
            }
            else
            {
                return "2";
            }
        }
        private int CapturarRol()
        {
            if (cboRol.SelectedIndex ==0)
            {
                //Administrador
                return 1;
            }
            else
            {
                //Funcionario
                return 3;
            }
        }
        private void btnGuardarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (FormularioValido())
            {
                UsuarioBl userBl = new UsuarioBl();
                string nombres = txtNombres.Text;
                string apellidos = txtApellidos.Text;
                decimal edad = decimal.Parse(txtEdad.Text);
                string rut = txtRut.Text;
                string id_genero = CapturarGenero();
                decimal id_comuna = decimal.Parse(cboComuna.SelectedValue.ToString());
                decimal id_region = decimal.Parse(cboRegion.SelectedValue.ToString());
                string direccion = txtDireccion.Text;
                string email = txtEmail.Text;
                string celular = txtCelular.Text;
                string contrasena = "TurismoRealPass";
                int id_rol = CapturarRol();
                string estado = CapturarEstado();


                var resp = UsuarioBl.GetInstance().RegistrarUsuario(nombres, apellidos, edad, rut, id_genero, id_comuna, id_region, direccion, email, celular, contrasena, id_rol, estado);
                if (resp.EsPositiva)
                {
                    MessageBox.Show(resp.Mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    CerrarRegistro();
                    CargarClientes();
                    
                }
                else
                {
                    MessageBox.Show(resp.Mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


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
                    txtRutEdit.IsEnabled = false;
                    txtEdadEdit.IsEnabled = false;
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

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    UsuarioBl userBl = new UsuarioBl();
        //    var usuario = userBl.GetUserId(45);
        //    foreach(var u in usuario)
        //    {
        //        Console.WriteLine(u.nombres);
        //    }
            
        //}

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





        private void cargarComunas(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UbicacionBl ubi = new UbicacionBl();

                int idRegion = cboRegion.SelectedIndex+1;

                Console.WriteLine(idRegion);
                List<GetComunas> comunas = ubi.GetRegionPorComuna(idRegion);
                cboComuna.ItemsSource = comunas;
                cboComuna.SelectedValuePath = "id_comuna";
                cboComuna.DisplayMemberPath = "nombre_comuna";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgClientes_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgClientes.Columns[0].Header = "Id";
            dgClientes.Columns[0].DisplayIndex = 0;
            dgClientes.Columns[1].Header = "Región";
            dgClientes.Columns[1].DisplayIndex = 1;
            dgClientes.Columns[2].Header = "Comuna";
            dgClientes.Columns[2].DisplayIndex = 2;
            dgClientes.Columns[3].Header = "Rut";
            dgClientes.Columns[3].DisplayIndex = 3;
            dgClientes.Columns[4].Header = "Nombres";
            dgClientes.Columns[4].DisplayIndex = 4;
            dgClientes.Columns[5].Header = "Apellidos";
            dgClientes.Columns[5].DisplayIndex = 5;
            dgClientes.Columns[6].Header = "Dirección";
            dgClientes.Columns[6].DisplayIndex = 6;
            dgClientes.Columns[7].Header = "Email";
            dgClientes.Columns[7].DisplayIndex = 7;
            dgClientes.Columns[8].Header = "Telefono";
            dgClientes.Columns[8].DisplayIndex = 8;
            dgClientes.Columns[9].Header = "Edad";
            dgClientes.Columns[9].DisplayIndex = 9;
            dgClientes.Columns[10].Header = "Genero";
            dgClientes.Columns[10].DisplayIndex = 10;
            dgClientes.Columns[11].Header = "Estado";
            dgClientes.Columns[11].DisplayIndex = 11;
            dgClientes.Columns[12].Header = "Tipo usuario";
            dgClientes.Columns[12].DisplayIndex = 12;
        }
    }
}
