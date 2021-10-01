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
        public AdmDepartamentos()
        {
            
            InitializeComponent();
            ValidacionesInput();
            CargarRegiones();
        }

        //Validacion Inputs
        private void ValidacionesInput()
        {
            txtBaños.MaxLength = 2;
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
        private void btnExaminar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg(*.jpg)|*.jpg|png(*.png)|*.png|gif(*.gif)|*.gif|bmp(*.bmp)|*.bmp|All Files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                //imgDynamic.Source = new BitmapImage(fileUri);
            }
        }
        public void limpiarFormulario()
        {
            txtMetros.Text = string.Empty;
            txtDormitorios.Text = string.Empty;
            txtBaños.Text = string.Empty;
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
            if (txtMetros.Text != string.Empty && txtDormitorios.Text != string.Empty && txtBaños.Text != string.Empty &&
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
            if (FormularioLleno())
            {

            }

        }

        private void btnAbrirInventario_Click(object sender, RoutedEventArgs e)
        {
            FlyInventario.IsOpen = true;
            List<Inventario> inventario = new List<Inventario>();
            Inventario i1 = new Inventario();
            i1.descripcion = "TV 60'";
            Inventario i2 = new Inventario();
            i2.descripcion = "Kit baño";
            inventario.Add(i1);
            inventario.Add(i2);
            dgInventario.ItemsSource = inventario;
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
    } 
}
