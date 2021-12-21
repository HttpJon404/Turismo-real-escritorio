using Microsoft.Win32;
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
using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using EntidadServicio;
using TurismoNegocio;

namespace TurismoPresentacion
{
    /// <summary>
    /// Lógica de interacción para AdmDepartamentos.xaml
    /// </summary>
    public partial class AdmDepartamentos : Page
    {
        List<InventarioTabla> lInventario = new List<InventarioTabla>();
        //Lista de ids de inventario para enviar al web service.
        List<int> idsInventarioAdd = new List<int>();
        string[] listaOrigen = new string[2];
        string[] nombreImagenes = new string[2];
        string portadaOrigen;
        string archivoPortada;
        string[] rutaImagenesSave = new string[2];
        bool open = false;

        List<Inventario> listaInventarioGlobal = new InventarioBl().Getinventario();


        public AdmDepartamentos(int id)
        {

            InitializeComponent();
            ValidacionesInput();
            CargarEstadoDeptos();
            CargarRegiones();
            lblEstado.Visibility = Visibility.Hidden;
            cboEstado.Visibility = Visibility.Hidden;
            CargarInventarios();
            if (id != 0)
            {
                //Modo editar departamento
                modoEditarDepto(id);

            }

        }

        private void CargarEstadoDeptos()
        {
            DepartamentoBl dpto = new DepartamentoBl();

            List<EstadoDepto> comunas = dpto.GetEstadoDepto();

            if (comunas.Count <= 0)
            {
                cboEstado.ItemsSource = null;
                MessageBox.Show("Ha ocurrido un error de red al cargar el listado, revise su conexión a internet e intente nuevamente", "Error de red", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                cboEstado.ItemsSource = comunas;
                cboEstado.SelectedValuePath = "Id";
                cboEstado.DisplayMemberPath = "Descripcion";
            }

        }

        private void modoEditarDepto(int id)
        {
            btnGuardarDpto.Content = "Modificar";
            lblTitulo.Content = "Modificar departamento";
            btnLimpiar.IsEnabled = false;
            lblEstado.Visibility = Visibility.Visible;
            cboEstado.Visibility = Visibility.Visible;



            btnLimpiar.Visibility = Visibility.Hidden;
            cargarDatosDepto(id);
        }

        private void cargarDatosDepto(int id)
        {

            DepartamentoBl deptoBl = new DepartamentoBl();
            var depto = deptoBl.GetDeptoId(id);

            foreach (var dpto in depto)
            {
                int metros = (int)dpto.metrosm2;
                int dormitorios = (int)dpto.dormitorios;
                int baños = (int)dpto.baños;
                int valor = (int)dpto.valor_arriendo;
                int estacionamiento = (int)dpto.estacionamiento;
                if (estacionamiento == 1)
                {
                    swEstacionamiento.IsOn = true;
                }
                txtMetros.Text = metros.ToString();
                txtDormitorios.Text = dormitorios.ToString();
                txtBanos.Text = baños.ToString();
                txtDireccion.Text = dpto.direccion;
                cboRegion.SelectedValue = dpto.id_region;
                cboComuna.SelectedValue = dpto.id_comuna;
                cboEstado.SelectedValue = dpto.id_estado;
                txtValorArriendo.Text = "$" + valor.ToString();
                txtCondiciones.Text = dpto.condiciones;

                // Crear BitmapSource  
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(dpto.portada);
                bitmap.EndInit();
                //Poner portada en la interfaz
                imgPortadaDpto.Source = bitmap;

                var links = dpto.ruta_archivo;

                //Inventario
                List<TipoInventario> listaInventario = new List<TipoInventario>();
                listaInventario = new DepartamentoBl().GetinventarioPorDepto(id);

                foreach (var invt in listaInventario)
                {
                    InventarioTabla inventario = new InventarioTabla()
                    {
                        Id = Convert.ToInt32(invt.id_inventario),
                        Descripcion = invt.nombre_inventario,
                        Precio = Convert.ToInt32(invt.precio_inventario)
                    };

                    lInventario.Add(inventario);
                    dgInventario.ItemsSource = null;
                    dgInventario.ItemsSource = lInventario;

                }

                //Imagenes
                try
                {
                    var images = deptoBl.GetImagenesDpto(id);

                    // Crear BitmapSource  
                    BitmapImage bitmap1 = new BitmapImage();
                    bitmap1.BeginInit();
                    bitmap1.UriSource = new Uri(images[0].ruta_archivo);
                    bitmap1.EndInit();
                    //Poner portada en la interfaz
                    img1Depto.Source = bitmap1;

                    // Crear BitmapSource  
                    BitmapImage bitmap2 = new BitmapImage();
                    bitmap2.BeginInit();
                    bitmap2.UriSource = new Uri(images[1].ruta_archivo);
                    bitmap2.EndInit();
                    //Poner portada en la interfaz
                    img2Depto.Source = bitmap2;

                }
                catch (Exception)
                {


                }
            }


        }

        private void CargarInventarios()
        {
            dgInventario.Items.Refresh();
            InventarioBl inventarioBl = new InventarioBl();
            List<Inventario> listaInventario = inventarioBl.Getinventario();

            cboInventario.ItemsSource = listaInventario;
            cboInventario.SelectedValuePath = "Id";
            cboInventario.DisplayMemberPath = "Descripcion";
            cboInventario.SelectedIndex = 0;
        }

        //Validacion Inputs
        private void ValidacionesInput()
        {
            txtBanos.MaxLength = 2;
            txtDormitorios.MaxLength = 3;
        }
        private void CargarRegiones()
        {
            UbicacionBl ubi = new UbicacionBl();
            List<GetComunas> comunas = ubi.GetRegion();

            if (comunas.Count <= 0)
            {
                cboRegion.ItemsSource = null;
                MessageBox.Show("Ha ocurrido un error de red al cargar el listado, revise su conexión a internet e intente nuevamente", "Error de red", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                cboRegion.ItemsSource = null;
                cboRegion.ItemsSource = comunas;
                cboRegion.SelectedValuePath = "id_region";
                cboRegion.DisplayMemberPath = "nombre_region";
            }


            cboRegion.ItemsSource = comunas;
            cboRegion.SelectedValuePath = "id_region";
            cboRegion.DisplayMemberPath = "nombre_region";
        }

        //Portada
        private void btnExaminarPort_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg(*.jpg)|*.jpg|png(*.png)|*.png|gif(*.gif)|*.gif|bmp(*.bmp)|*.bmp|All Files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                portadaOrigen = openFileDialog.FileName;
                imgPortadaDpto.Source = new BitmapImage(fileUri);
                archivoPortada = openFileDialog.SafeFileName;
            }

        }

        //Imagen 1
        private void btnExaminar_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg(*.jpg)|*.jpg|png(*.png)|*.png|gif(*.gif)|*.gif|bmp(*.bmp)|*.bmp|All Files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                var rutaOrigen = openFileDialog.FileName;
                img1Depto.Source = new BitmapImage(fileUri);
                listaOrigen[0] = rutaOrigen;
                nombreImagenes[0] = openFileDialog.SafeFileName;

            }
        }

        //Imagen 2
        private void btnExaminar3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg(*.jpg)|*.jpg|png(*.png)|*.png|gif(*.gif)|*.gif|bmp(*.bmp)|*.bmp|All Files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                var rutaOrigen = openFileDialog.FileName;
                img2Depto.Source = new BitmapImage(fileUri);
                listaOrigen[1] = rutaOrigen;
                nombreImagenes[1] = openFileDialog.SafeFileName;


            }
        }

        public void limpiarFormulario()
        {
            txtMetros.Text = string.Empty;
            txtDormitorios.Text = string.Empty;
            txtBanos.Text = string.Empty;
            swEstacionamiento.IsOn = false;
            txtDireccion.Text = string.Empty;
            cboRegion.SelectedIndex = -1;
            cboComuna.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;
            txtValorArriendo.Text = string.Empty;
            txtCondiciones.Text = string.Empty;
            txtValorAdm.Text = string.Empty;
            txtDescripcion.Text = string.Empty;

        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiarFormulario();
        }

        //Validacion input
        public bool FormularioLleno()
        {
            if (txtMetros.Text != string.Empty && txtDormitorios.Text != string.Empty && txtBanos.Text != string.Empty &&
            txtDireccion.Text != string.Empty && cboRegion.SelectedIndex != -1 && cboComuna.SelectedIndex != -1 && txtValorArriendo.Text != string.Empty && txtCondiciones.Text != string.Empty)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Para agregar un departamento debe llenar el formulario", "Completar formulario", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void btnGuardarDpto_Click(object sender, RoutedEventArgs e)
        {
            string accion = (string)btnGuardarDpto.Content;

            if (accion == "Guardar")
            {
                //Guardar nuevo departamento
                DepartamentoBl deptoBl = new DepartamentoBl();
                if (FormularioLleno())
                {
                    try
                    {
                        int metros2 = int.Parse(txtMetros.Text);
                        int dormitorios = int.Parse(txtDormitorios.Text);
                        int baños = int.Parse(txtBanos.Text);
                        int metrosm2 = int.Parse(txtMetros.Text);
                        //Estacionamiento
                        int estacionamiento = 0;
                        if (swEstacionamiento.IsOn)
                        {
                            estacionamiento = 1;
                        }
                        string direccion = txtDireccion.Text;

                        decimal id_comuna = decimal.Parse(cboComuna.SelectedValue.ToString());

                        int comuna = (int)id_comuna;

                        decimal id_region = decimal.Parse(cboRegion.SelectedValue.ToString());
                        string arriendo = txtValorArriendo.Text.Remove(0, 1);

                        int valorArriendo = int.Parse(arriendo);

                        string condiciones = txtCondiciones.Text;

                        int estado = 1;

                        //lista imagenes
                        string rutaDestino = @"C:\Turismo";

                        string[] rutaImagenesSave = new string[2];

                        for (int i = 0; i < listaOrigen.Length; i++)
                        {
                            string imagenDptoOrigen = listaOrigen[i];
                            string archivoImagen = nombreImagenes[i];
                            //Destino de la imagen c/turismo/nombreimagen
                            string imagenDestino = System.IO.Path.Combine(rutaDestino, archivoImagen);
                            //Guardar imagen de departamento
                            System.IO.File.Copy(imagenDptoOrigen, imagenDestino, true);

                            //Guardar rutas listas en la lista
                            rutaImagenesSave[i] = imagenDestino;

                        }
                        //Guardar rutas en donde se guardaron las imagenes/c/turismo/imagenX


                        //portada
                        string portada = portadaOrigen;

                        //Destino de la imagen
                        string portadaDestino = System.IO.Path.Combine(rutaDestino, archivoPortada);
                        //Guardar imagen
                        System.IO.File.Copy(portadaOrigen, portadaDestino, true);

                        //lista inventarios
                        int[] inventarios = new int[idsInventarioAdd.Count];

                        for (int i = 0; i < idsInventarioAdd.Count; i++)
                        {
                            inventarios[i] = idsInventarioAdd[i];
                        }


                        try
                        {
                            var resp = deptoBl.RegistrarDepartamento(dormitorios, baños, metrosm2, estacionamiento, direccion, comuna, estado, valorArriendo, condiciones, inventarios, rutaImagenesSave, portadaDestino);
                
                            MessageBox.Show("Departamento agregado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            limpiarFormulario();
                            tabDatos.Focus();
                         

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("No se pudo agregar el departamento, verifique el formulario.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
            else
            {
                //Modificar departamento


            }

        }
        public void CargarInventarioActual()
        {
            dgInventario.Items.Refresh();
            FlyInventario.IsOpen = true;
            open = true;
            dgInventario.ItemsSource = lInventario;
        }
        private void btnAbrirInventario_Click(object sender, RoutedEventArgs e)
        {
            if (open)
            {
                FlyInventario.IsOpen = false;
                open = false;
            }
            else
            {
                FlyInventario.IsOpen = true;
                open = true;
            }
        }

        private void btnCerrarInventario_Click(object sender, RoutedEventArgs e)
        {
            FlyInventario.IsOpen = false;
        }

        private void cboRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Cargar combobox con comunas dependiendo de la región.
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



        //Portada


        private void btnAgregarInventario_Click(object sender, RoutedEventArgs e)
        {


            decimal id = (decimal)cboInventario.SelectedValue;
            int idInventario = (int)id;
            foreach (var i in listaInventarioGlobal)
            {
                if (Convert.ToInt32(i.Id) == idInventario)
                {

                    InventarioTabla inventario = new InventarioTabla()
                    {
                        Id = Convert.ToInt32(i.Id),
                        Descripcion = i.Descripcion,
                        Precio = Convert.ToInt32(i.Precio)
                    };

                    lInventario.Add(inventario);
                    idsInventarioAdd.Add(idInventario);

                    CargarInventarioActual();

                }
            }
            CalcularTotalInventario();

        }
        private void CalcularTotalInventario()
        {

            int total = 0;
            //Calcular total inventario
            foreach (var inv in lInventario)
            {
                total = total + inv.Precio;
            }
            lblTotal.Content = "$" + total.ToString();

        }


        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            FlyInventario.IsOpen = false;
            open = false;
        }

        private void dgInventario_AutoGeneratedColumns(object sender, EventArgs e)
        {
            //if (modoEditInventarios)
            //{

            //    dgInventario.Columns[0].Header = "Id";
            //    dgInventario.Columns[0].DisplayIndex = 0;
            //    dgInventario.Columns[0].Visibility = Visibility.Hidden;
            //    dgInventario.Columns[1].Header = "Id";
            //    dgInventario.Columns[1].DisplayIndex = 1;
            //    dgInventario.Columns[2].Header = "Descripcion";
            //    dgInventario.Columns[2].DisplayIndex = 2;
            //    dgInventario.Columns[3].Header = "Precio";
            //    dgInventario.Columns[3].DisplayIndex = 3;

            //    modoEditInventarios = false;
            //    //dgInventario.Columns[3].Visibility = Visibility.Hidden;

            //}
        }

        private void btnEliminarInventario_Click(object sender, RoutedEventArgs e)
        {
            int idInventario;
            var selected = dgInventario.SelectedItem;
            var inventarioTabla = new InventarioTabla();

            inventarioTabla = (InventarioTabla)selected;

            if (selected != null)
            {


                MessageBoxResult result = MessageBox.Show("¿Desea Eliminar este inventario?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {

                    idInventario = inventarioTabla.Id;

                    lInventario.Remove(inventarioTabla);
                    dgInventario.Items.Refresh();
                    dgInventario.ItemsSource = lInventario;
                    idsInventarioAdd.Remove(idInventario);

                    CalcularTotalInventario();
                }



            }
            else
            {
                MessageBox.Show("Debe seleccionar un inventario en la tabla para eliminarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }




        //----------Tabulacion-----------------//
        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            tabInventario.Focus();
        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {
            tabImagenes.Focus();
        }

        private void btnBack1_Click(object sender, RoutedEventArgs e)
        {
            tabDatos.Focus();
        }

        private void btnBack2_Click(object sender, RoutedEventArgs e)
        {
            tabInventario.Focus();
        }


        private void txtValorArriendo_LostFocus(object sender, RoutedEventArgs e)
        {
            string texto = txtValorArriendo.Text;

            if (texto.Contains("$"))
            {
                txtValorArriendo.Text = texto;
                string text = texto.Remove(0, 1);
                MessageBox.Show(text);
            }
            else
            {
                txtValorArriendo.Text = "$" + texto;
            }

        }




    }
}

