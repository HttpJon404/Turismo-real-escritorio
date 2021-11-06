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

        public AdmDepartamentos(int id)
        {
            
            InitializeComponent();
            ValidacionesInput();
            CargarRegiones();
            CargarInventarios();
            if (id!=0)
            {
                //Modo editar departamento
                modoEditarDepto();
            }

        }

        private void modoEditarDepto()
        {
            lblTitulo.Content = "Modificar departamento";
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
            cboRegion.ItemsSource = comunas;
            cboRegion.SelectedValuePath = "id_region";
            cboRegion.DisplayMemberPath = "nombre_region";
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
            txtDireccion.Text != string.Empty && cboRegion.SelectedIndex != -1 && cboComuna.SelectedIndex != -1 &&
            cboEstado.SelectedIndex != -1 && txtValorArriendo.Text != string.Empty && txtCondiciones.Text != string.Empty &&
            txtValorAdm.Text != string.Empty && txtDescripcion.Text != string.Empty)
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
                    //Estado
                    int valorArriendo = int.Parse(txtValorArriendo.Text);
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

        private void btnAgregarInventario_Click(object sender, RoutedEventArgs e)
        {
            InventarioTabla inventario = new InventarioTabla();
            decimal id = (decimal)cboInventario.SelectedValue;
            int idInventario = (int)id;
            inventario.Id = (int)idInventario;
            inventario.Descripcion = cboInventario.Text;
            lInventario.Add(inventario);
            idsInventarioAdd.Add(idInventario);
            CargarInventarioActual();


        }



        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            FlyInventario.IsOpen = false;
            open = false;
        }


    } 
}
