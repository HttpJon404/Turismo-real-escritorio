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
    /// Lógica de interacción para AdmInventario.xaml
    /// </summary>
    public partial class AdmInventario : Page
    {
        public AdmInventario()
        {
            InitializeComponent();
            CargarInventarios();
            CargarServicios();
        }

        private void CargarInventarios()
        {

            InventarioBl inventarioBl = new InventarioBl();
            List<Inventario> listaInventario = inventarioBl.Getinventario();
            dgInventarios.ItemsSource = listaInventario;

        }
        private void CargarServicios()
        {
            ServicioBl servicioBl = new ServicioBl();
            List<Servicios> listaInventario = servicioBl.GetServicios();
            dgServicios.ItemsSource = listaInventario;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InventarioBl inventarioBl = new InventarioBl();
            List<Inventario> listaInventario = inventarioBl.Getinventario();
            dgInventarios.ItemsSource = listaInventario;
        }

        private void btnGuardarInvt_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormInventario())
            {
                //Guardar inventario
            }
        }

        private bool ValidarFormInventario()
        {
            if (txtDescripcionI.Text != string.Empty && txtCantidadI.Text != string.Empty &&
                txtPrecioInv.Text!=string.Empty)
            {
                return true;
                
            }
            MessageBox.Show("Complete el formulario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        private void btnGuardarServicio_Click(object sender, RoutedEventArgs e)
        {
            ServicioBl servicio = new ServicioBl();
            if (ValidarFormServicio())
            {
              
                //Guardar servicio
            }
        }

        private bool ValidarFormServicio()
        {
            if (txtValorServicio.Text != string.Empty && txtNombreServicio.Text  != string.Empty)
            {
                return true;
            }
            MessageBox.Show("Complete el formulario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }
}
