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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;


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

        public bool ValidarCamposInventario()
        {
            if (txtDescripcionI.Text != string.Empty && txtPrecioInv.Text != string.Empty)
            {
                return true;
            }
            return false;
        }

        public bool ValidarCamposInventarioEdit()
        {
            if (txtDescripcionEditInv.Text != string.Empty && txtPrecioEditInv.Text != string.Empty)
            {
                return true;
            }
            return false;
        }

        public bool ValidarCamposServicio()
        {
            if (txtNombreServicio.Text != string.Empty && txtValorServicio.Text != string.Empty)
            {
                return true;
            }
            return false;
        }

        public bool ValidarCamposServicioEdit()
        {
            if (txtNombreServicioEdit.Text != string.Empty && txtValorServicioEdit.Text != string.Empty)
            {
                return true;
            }
            return false;
        }

        private void LimpiarInventarioEdit()
        {
            txtDescripcionEditInv.Text = string.Empty;
            txtPrecioEditInv.Text = string.Empty;
        }

        private void LimpiarServicioEdit()
        {
            txtNombreServicioEdit.Text = string.Empty;
            txtValorServicioEdit.Text = string.Empty;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CargarInventarios()
        {

            InventarioBl inventarioBl = new InventarioBl();
            List<Inventario> listaInventario = inventarioBl.Getinventario().OrderBy(c => c.Id).ToList();
            if (listaInventario.Count<=0)
            {
                MessageBox.Show("Ha ocurrido un error de red, re-intente nuevamente","Error");
                
            }
            else
            {
                dgInventarios.ItemsSource = null;
                dgInventarios.ItemsSource = listaInventario;

            }

        }
        private void CargarServicios()
        {
            ServicioBl servicioBl = new ServicioBl();
            List<Servicios> listaInventario = servicioBl.GetServicios().OrderBy(c => c.Id).ToList();
            dgServicios.ItemsSource = listaInventario;
            if (listaInventario.Count <= 0)
            {
                MessageBox.Show("Ha ocurrido un error de red, re-intente nuevamente", "Error");

            }
            else
            {
                dgServicios.ItemsSource = null;
                dgServicios.ItemsSource = listaInventario;

            }

        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    InventarioBl inventarioBl = new InventarioBl();
        //    List<Inventario> listaInventario = inventarioBl.Getinventario().OrderBy(c => c.Id).ToList();
        //    dgInventarios.ItemsSource = listaInventario;
        //}

        private void btnGuardarServicio_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCamposServicio())
            {
                MessageBox.Show("Debe llenar todos los datos del formulario", "Error");

            }
            else
            {
                string descripcion = txtNombreServicio.Text;
                int valor = Convert.ToInt32(txtValorServicio.Text);

                var resp = ServicioBl.GetInstance().CrearServicio(descripcion, valor);

                if (resp.EsPositiva)
                {
                    MessageBox.Show(resp.Mensaje, "Exito");
                    CargarServicios();
                }
                else
                {
                    MessageBox.Show(resp.Mensaje, "Error");
                }

            }


        }

        private void btnGuardarInvt_Click(object sender, RoutedEventArgs e)
        {
            //txtNombreServicio
            //txtValorServicio

            if (!ValidarCamposInventario())
            {
                MessageBox.Show("Debe llenar todos los datos del formulario", "Error");

            }
            else
            {
                string descripcion = txtDescripcionI.Text;
                int precio = Convert.ToInt32(txtPrecioInv.Text);

                var resp = InventarioBl.GetInstance().CrearInventario(descripcion, precio);

                if (resp.EsPositiva)
                {
                    MessageBox.Show(resp.Mensaje, "Exito");
                    CargarInventarios();
                }
                else
                {
                    MessageBox.Show(resp.Mensaje, "Error");
                }

            }
        }

        private int idInventario = 0;
        private void btnEditarInv_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgInventarios.SelectedItem;
            var inventario = new Inventario();
            if (selected == null)
            {
                MessageBox.Show("Seleccione un registro para editar", "Error");
                EditInventario.IsOpen = false;
            }
            else
            {
                EditInventario.IsOpen = true;


                inventario = (Inventario)selected;
                idInventario = Convert.ToInt32(inventario.Id);
                txtDescripcionEditInv.Text = inventario.Descripcion;
                txtPrecioEditInv.Text = inventario.Precio.ToString();
            }

        }

        private void btnCerrarEditInv_Click(object sender, RoutedEventArgs e)
        {
            EditInventario.IsOpen = false;
            LimpiarInventarioEdit();
        }

        private void btnGuardarEditInv_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCamposInventarioEdit())
            {
                MessageBox.Show("Debe llenar todos los datos del formulario", "Error");

            }
            else
            {
                
                string descripcion = txtDescripcionEditInv.Text;
                int precio = Convert.ToInt32(txtPrecioEditInv.Text);

                var resp = InventarioBl.GetInstance().ActualizarInventario(idInventario,descripcion, precio);

                if (resp.EsPositiva)
                {
                    MessageBox.Show(resp.Mensaje, "Exito");
                    CargarInventarios();
                    idInventario = 0;
                    EditInventario.IsOpen = false;
                    LimpiarInventarioEdit();
                }
                else
                {
                    MessageBox.Show(resp.Mensaje, "Error");
                }

            }
        }

        private void btnEliminarInvt_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgInventarios.SelectedItem;
            var inventario = new Inventario();

            MessageBoxResult result = MessageBox.Show("¿Desea Eliminar este registro?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                inventario = (Inventario)selected;
                idInventario = Convert.ToInt32(inventario.Id);
                //Deshabilitar inventario
                var resp = InventarioBl.GetInstance().DeleteInventario(idInventario);

                if (resp.EsPositiva)
                {
                    MessageBox.Show(resp.Mensaje, "Exito");
                    CargarInventarios();
                    idInventario = 0;
                }
                else
                {
                    MessageBox.Show(resp.Mensaje, "Error");
                }


            }
        }

        private int idServicio = 0;
        private void btnEditarServicio_Click(object sender, RoutedEventArgs e)
        {
            
            var selected = dgServicios.SelectedItem;
            var servicio = new Servicios();
            if (selected == null)
            {
                MessageBox.Show("Seleccione un registro para editar", "Error");
                EditServicios.IsOpen = false;
            }
            else
            {
                EditServicios.IsOpen = true;
                servicio = (Servicios)selected;
                idServicio = Convert.ToInt32(servicio.Id);
                txtNombreServicioEdit.Text = servicio.Descripcion;
                txtValorServicioEdit.Text = servicio.Valor.ToString();
            }

        }

        private void btnCerrarEditServicio_Click(object sender, RoutedEventArgs e)
        {
            EditServicios.IsOpen = false;
            LimpiarServicioEdit();
        }

        private void btnGuardarEditServicio_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCamposServicioEdit())
            {
                MessageBox.Show("Debe llenar todos los datos del formulario", "Error");

            }
            else
            {

                string descripcion = txtNombreServicioEdit.Text;
                int valor = Convert.ToInt32(txtValorServicioEdit.Text);

                var resp = ServicioBl.GetInstance().ActualizarServicio(idServicio, descripcion, valor);

                if (resp.EsPositiva)
                {
                    MessageBox.Show(resp.Mensaje, "Exito");
                    CargarServicios();
                    idServicio = 0;
                    EditServicios.IsOpen = false;
                    LimpiarServicioEdit();
                }
                else
                {
                    MessageBox.Show(resp.Mensaje, "Error");
                }

            }
        }

        private void btnEliminarServicio_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgServicios.SelectedItem;
            var servicio = new Servicios();

            MessageBoxResult result = MessageBox.Show("¿Desea Eliminar este registro?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                servicio = (Servicios)selected;
                idServicio = Convert.ToInt32(servicio.Id);
                //Deshabilitar servicio
                var resp = ServicioBl.GetInstance().DeleteServicio(idServicio);

                if (resp.EsPositiva)
                {
                    MessageBox.Show(resp.Mensaje, "Exito");
                    CargarServicios();
                    idServicio = 0;
                }
                else
                {
                    MessageBox.Show(resp.Mensaje, "Error");
                }
            }
        }
    }
}
