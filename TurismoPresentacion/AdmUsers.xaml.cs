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
        decimal idUsuario = 0;
        List<UsuarioTabla> usuarios = new List<UsuarioTabla>();
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


        private void CargarRegiones()
        {
            UbicacionBl ubi = new UbicacionBl();
            List<GetComunas> comunas = ubi.GetRegion();
            if (comunas.Count>=1)
            {
                cboRegion.ItemsSource = null;
                cboRegion.ItemsSource = comunas;
                cboRegion.SelectedValuePath = "id_region";
                cboRegion.DisplayMemberPath = "nombre_region";
            }
            else
            {
                cboRegion.ItemsSource = null;
                MessageBox.Show("Ha ocurrido un error de red al cargar los datos, reintente nuevamente", "Error de red", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
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
            dgClientes.ItemsSource = null;
            UsuarioBl userBl = new UsuarioBl();
            usuarios = userBl.UsuariosGrid().GroupBy(u => u.id).Select(g => g.First()).OrderBy(c => c.id).ToList();
            if (usuarios.Count <= 0)
            {
                MessageBox.Show("Ha ocurrido un error de red al cargar el listado, reintente nuevamente", "Error de red", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                dgClientes.ItemsSource = null;
                dgClientes.ItemsSource = usuarios;
            }

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
            if (txtNombres.Text != string.Empty && txtRut.Text != string.Empty && txtApellidos.Text != string.Empty &&
            txtEdad.Text != string.Empty && txtDireccion.Text != string.Empty &&
            txtEmail.Text != string.Empty && txtCelular.Text != string.Empty && cboComuna.SelectedIndex != -1 && cboRegion.SelectedIndex != -1 &&
            cboGenero.SelectedIndex != -1 && cboRol.SelectedIndex != -1)
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
        public bool FormularioValidoEdit()
        {
            if (txtNombresEdit.Text != string.Empty && txtRutEdit.Text != string.Empty && txtApellidosEdit.Text != string.Empty &&
            txtEdadEdit.Text != string.Empty && txtDireccionEdit.Text != string.Empty &&
            txtEmailEdit.Text != string.Empty && txtCelularEdit.Text != string.Empty && cboComuna1.SelectedIndex != -1 && cboRegion1.SelectedIndex != -1 &&
            cboGenero1.SelectedIndex != -1 && cboRol1.SelectedIndex != -1)
            {

                return true;
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
            if (cboEstado.SelectedIndex == 0)
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
            if (cboGenero.SelectedIndex == 0)
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
            if (cboRol.SelectedIndex == 0)
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

        private string CapturarEstadoEdit()
        {
            if (cboEstado1.SelectedIndex == 0)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        private string CapturarGeneroEdit()
        {
            if (cboGenero1.SelectedIndex == 0)
            {
                //Masculino
                return "1";
            }
            else
            {
                return "2";
            }
        }
        private int CapturarRolEdit()
        {
            if (cboRol1.SelectedIndex == 0)
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
                int id_rol = CapturarRolEdit();
                string estado = CapturarEstadoEdit();


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


        private void btnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            UsuarioBl userBl = new UsuarioBl();
            List<Usuario> usuarios = userBl.GetUsers();


            var selected = dgClientes.SelectedItem;
            var usuarioTabla = new UsuarioTabla();

            usuarioTabla = (UsuarioTabla)selected;

            if (selected != null)
            {
                idUsuario = usuarioTabla.id;
                //MessageBox.Show(usuarioTabla.id.ToString());
                txtRutEdit.IsEnabled = false;
                txtEdadEdit.IsEnabled = false;
                FlyEditUser.IsOpen = true;
                //Cargar regiones
                UbicacionBl ubi = new UbicacionBl();
                List<GetComunas> regiones = ubi.GetRegion();
                cboRegion1.ItemsSource = regiones;
                cboRegion1.SelectedValuePath = "id_region";
                cboRegion1.DisplayMemberPath = "nombre_region";

                //var alo = cboRegion1.SelectedValue;
                //MessageBox.Show(alo.ToString());

                var usuario = userBl.GetUserId(idUsuario);
                lblId.Content = idUsuario.ToString();
                foreach (var u in usuario)
                {
                    //Traer datos al formulario.
                    txtRutEdit.Text = u.rut;
                    txtNombresEdit.Text = u.nombres;
                    txtApellidosEdit.Text = u.apellidos;
                    txtEdadEdit.Text = u.edad.ToString();
                    txtCelularEdit.Text = u.celular;
                    txtDireccionEdit.Text = u.direccion;
                    string contrasena = u.contrasena;
                    txtEmailEdit.Text = u.correo;
                    decimal idregion = u.id_region;
                    cboRegion1.SelectedValue = idregion;

                    int idRegion = cboRegion1.SelectedIndex + 1;

                    //Console.WriteLine("id region: ", idRegion);
                    List<GetComunas> comunas = ubi.GetRegionPorComuna(idRegion);
                    cboComuna1.ItemsSource = comunas;
                    cboComuna1.SelectedValuePath = "id_comuna";
                    cboComuna1.DisplayMemberPath = "nombre_comuna";
                    cboComuna1.SelectedValue = u.id_comuna;

                    //Cargar combobox de género.
                    string genero = u.genero;
                    if (genero == "M")
                    {
                        cboGenero1.SelectedIndex = 0;
                    }
                    else
                    {
                        cboGenero1.SelectedIndex = 1;
                    }
                    //Cargar rol
                    int rol = (int)u.id_rol;
                    if (rol == 1)
                    {
                        cboRol1.SelectedIndex = 0;

                    }
                    else
                    {
                        cboRol1.SelectedIndex = 1;
                    }
                    //Cargar estado
                    string estado = u.estado;
                    if (estado == "1.0")
                    {
                        cboEstado1.SelectedIndex = 0;
                    }
                    else
                    {
                        cboEstado1.SelectedIndex = 1;
                    }

                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario en la tabla para editarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        //Cargar comunas para Agregar
        private void cargarComunas(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UbicacionBl ubi = new UbicacionBl();

                int idRegion = cboRegion.SelectedIndex + 1;

                //Console.WriteLine(idRegion);
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
        //Cargar comunas para editar
        private void cboRegion1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UbicacionBl ubi = new UbicacionBl();

                int idRegion = cboRegion1.SelectedIndex + 1;

                //Console.WriteLine(idRegion);
                List<GetComunas> comunas = ubi.GetRegionPorComuna(idRegion);
                cboComuna1.ItemsSource = comunas;
                cboComuna1.SelectedValuePath = "id_comuna";
                cboComuna1.DisplayMemberPath = "nombre_comuna";
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Cargar tabla con headers e informacion correcta.
        private void dgClientes_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgClientes.Columns[0].Header = "Id";
            dgClientes.Columns[0].DisplayIndex = 0;
            dgClientes.Columns[1].Header = "Región";
            dgClientes.Columns[1].DisplayIndex = 9;
            dgClientes.Columns[2].Header = "Comuna";
            dgClientes.Columns[2].DisplayIndex = 10;
            dgClientes.Columns[3].Header = "Rut";
            dgClientes.Columns[3].DisplayIndex = 3;
            dgClientes.Columns[4].Header = "Nombres";
            dgClientes.Columns[4].DisplayIndex = 1;
            dgClientes.Columns[5].Header = "Apellidos";
            dgClientes.Columns[5].DisplayIndex = 2;
            dgClientes.Columns[6].Header = "Dirección";
            dgClientes.Columns[6].DisplayIndex = 12;
            dgClientes.Columns[7].Header = "Email";
            dgClientes.Columns[7].DisplayIndex = 8;
            dgClientes.Columns[8].Header = "Telefono";
            dgClientes.Columns[8].DisplayIndex = 11;
            dgClientes.Columns[9].Header = "Edad";
            dgClientes.Columns[9].DisplayIndex = 4;
            dgClientes.Columns[10].Header = "Genero";
            dgClientes.Columns[10].DisplayIndex = 5;
            dgClientes.Columns[11].Header = "Estado";
            dgClientes.Columns[11].DisplayIndex = 7;
            dgClientes.Columns[12].Header = "Tipo usuario";
            dgClientes.Columns[12].DisplayIndex = 6;
        }

        private void btnGuardarUsuarioEdit_Click(object sender, RoutedEventArgs e)
        {
            if (FormularioValidoEdit())
            {
                try
                {

                    UsuarioBl userBl = new UsuarioBl();
                    string nombres = txtNombresEdit.Text;
                    string apellidos = txtApellidosEdit.Text;
                    decimal edad = decimal.Parse(txtEdadEdit.Text);
                    string rut = txtRutEdit.Text;
                    string id_genero = CapturarGeneroEdit();
                    decimal id_comuna = decimal.Parse(cboComuna1.SelectedValue.ToString());
                    decimal id_region = decimal.Parse(cboRegion1.SelectedValue.ToString());
                    string direccion = txtDireccionEdit.Text;
                    string email = txtEmailEdit.Text;
                    string celular = txtCelularEdit.Text;
                    string contrasena = "TurismoRealPass";
                    int id_rol = CapturarRolEdit();
                    string estado = CapturarEstadoEdit();
                    decimal id = decimal.Parse(lblId.Content.ToString());

                    var resp = UsuarioBl.GetInstance().EditarUsuario(id, nombres, apellidos, id_genero, id_comuna, id_region, direccion, email, celular, contrasena, id_rol, estado);
                    if (resp.EsPositiva)
                    {
                        MessageBox.Show("Usuario editado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        FlyEditUser.IsOpen = false;
                        CargarClientes();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido moficar al usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            UsuarioBl userBl = new UsuarioBl();

            var selected = dgClientes.SelectedItem;
            var usuarioTabla = new UsuarioTabla();

            usuarioTabla = (UsuarioTabla)selected;


            if (selected != null)
            {
                idUsuario = usuarioTabla.id;


                if (usuarioTabla.estado == "Habilitado")
                {
                    MessageBoxResult result = MessageBox.Show("Este usuario esta habilitado. ¿Deseas inhabilitarlo?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            //Deshabilitar usuario

                            userBl.ActivarUsuario(idUsuario, "0");

                            CargarClientes();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Este usuario esta inhabilitado. ¿Deseas habilitarlo?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            //habilitar usuario
                            idUsuario = usuarioTabla.id;
                            userBl.ActivarUsuario(idUsuario, "1.0");
                            CargarClientes();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario en la tabla para cambiar su estado", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }


        private void btnReestablecerFiltros_Click(object sender, RoutedEventArgs e)
        {
            CargarClientes();
        }

        private void btnFiltrarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            UsuarioBl user = new UsuarioBl();
            string rut = txtRutFilter.Text;
            //List<UsuarioTabla> usuarios = user.FiltrarUsuarios(rut);

            //dgClientes.ItemsSource = null;
            //dgClientes.ItemsSource = usuarios;


            string nombreEstado = cboEstado2.Text;

            
            dgClientes.ItemsSource = usuarios.Where(d => d.estado.Contains(nombreEstado));


        }
        private void filtrarPorRut()
        {
            UsuarioBl user = new UsuarioBl();
            string rut = txtRutFilter.Text;
            List<UsuarioTabla> usuarios = user.FiltrarUsuarios(rut);

            dgClientes.ItemsSource = null;
            dgClientes.ItemsSource = usuarios;

        }

        private void txtRutFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtRutFilter.Text == string.Empty)
            {
                CargarClientes();
            }
            else
            {
                string rut = txtRutFilter.Text;
                dgClientes.ItemsSource = usuarios.Where(u => u.rut.Contains(rut));
            }
        }
    }
}