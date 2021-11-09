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
using MahApps.Metro.Controls;
using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;
using TurismoNegocio;
using EntidadServicio;

namespace TurismoPresentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            menu.IsOpen = true;
            swTema.Content = "Oscuro";

            gridListaDpto.Visibility = Visibility.Hidden;

        }
        private int idDpto = 0;
        List<DepartamentoTabla> dptos = new List<DepartamentoTabla>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            menu.IsOpen = true;
        }

        private void btnGestionDpto_Click(object sender, RoutedEventArgs e)
        {
            CargarDeptos();
            CargarEstadosFilter();
            CargarRegiones();
            Main.Content = null;
            gridListaDpto.Visibility = Visibility.Visible;
            imgFondo.Visibility = Visibility.Hidden;
        }


        private void CargarRegiones()
        {
            UbicacionBl ubi = new UbicacionBl();
            List<GetComunas> comunas = ubi.GetRegion();
            if (comunas.Count >= 1)
            {
                cboRegionFilter.ItemsSource = null;
                cboRegionFilter.ItemsSource = comunas;
                cboRegionFilter.SelectedValuePath = "id_region";
                cboRegionFilter.DisplayMemberPath = "nombre_region";
            }
            else
            {
                cboRegionFilter.ItemsSource = null;
                MessageBox.Show("Ha ocurrido un error de red al cargar los datos, reintente nuevamente", "Error de red", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void CargarEstadosFilter()
        {
            DepartamentoBl dpto = new DepartamentoBl();

            List<EstadoDepto> estadosDepto = dpto.GetEstadoDepto();

            if (estadosDepto.Count <= 0)
            {
                cboEstadoFilter.ItemsSource = null;
                MessageBox.Show("Ha ocurrido un error de red al cargar el listado, revise su conexión a internet e intente nuevamente", "Error de red", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                cboEstadoFilter.ItemsSource = null;
                cboEstadoFilter.ItemsSource = estadosDepto;
                cboEstadoFilter.SelectedValuePath = "Id";
                cboEstadoFilter.DisplayMemberPath = "Descripcion";
            }
        }

        private async void btnApagar_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult resultado = await this.ShowMessageAsync("Confirmación", "¿Estás seguro que deseas cerrar la aplicación? "
                    , MessageDialogStyle.AffirmativeAndNegative);

            if (resultado == MessageDialogResult.Affirmative)
            {
                Application.Current.Shutdown();
            }
        }


        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdmUsers();
            imgFondo.Visibility = Visibility.Hidden;
            gridListaDpto.Visibility = Visibility.Hidden;
        }

        private void btnListaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdmDepartamentos(0);
            imgFondo.Visibility = Visibility.Hidden;
            gridListaDpto.Visibility = Visibility.Hidden;
        }



        private void swTema_Toggled(object sender, RoutedEventArgs e)
        {
            //Cambiar tema
            if (swTema.IsOn == true)
            {
                ThemeManager.Current.ChangeTheme(Application.Current, "Dark.Orange");
                swTema.Content = "Normal";

            }
            else
            {
                ThemeManager.Current.ChangeTheme(Application.Current, "Light.Orange");
                swTema.Content = "Oscuro";
            }
        }

        private void btnAbrirInventarios_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdmInventario();
            imgFondo.Visibility = Visibility.Hidden;
            gridListaDpto.Visibility = Visibility.Hidden;
        }


        private void CargarDeptos()
        {

            DepartamentoBl deptos = new DepartamentoBl();
            dptos = deptos.GetDeptos().OrderBy(d => d.id).ToList();
            if (dptos.Count <= 0)
            {
                MessageBox.Show("Ha ocurrido un error de red al cargar el listado, reintente nuevamente", "Error de red", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                dgDeptos.ItemsSource = null;
                dgDeptos.ItemsSource = dptos;
            }

        }


        private void dgDeptos_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgDeptos.Columns[0].Header = "Id";
            dgDeptos.Columns[0].DisplayIndex = 0;
            dgDeptos.Columns[1].Header = "Dirección";
            dgDeptos.Columns[1].DisplayIndex = 1;
            dgDeptos.Columns[2].Header = "Dormitorios";
            dgDeptos.Columns[2].DisplayIndex = 2;
            dgDeptos.Columns[3].Header = "Valor arriendo";
            dgDeptos.Columns[3].DisplayIndex = 3;
            dgDeptos.Columns[4].Header = "Baños";
            dgDeptos.Columns[4].DisplayIndex = 4;
            dgDeptos.Columns[5].Header = "Metros²";
            dgDeptos.Columns[5].DisplayIndex = 5;
            dgDeptos.Columns[6].Header = "Estacionamiento";
            dgDeptos.Columns[6].DisplayIndex = 6;
            dgDeptos.Columns[7].Header = "Condiciones";
            dgDeptos.Columns[7].DisplayIndex = 7;
            dgDeptos.Columns[8].Header = "Comuna";
            dgDeptos.Columns[8].DisplayIndex = 8;
            dgDeptos.Columns[9].Header = "id region";
            dgDeptos.Columns[9].DisplayIndex = 9;
            dgDeptos.Columns[9].Visibility = Visibility.Hidden;
            dgDeptos.Columns[10].Header = "Region";
            dgDeptos.Columns[10].DisplayIndex = 10;
            dgDeptos.Columns[11].Header = "Estado";
            dgDeptos.Columns[11].DisplayIndex = 11;
            dgDeptos.Columns[12].Header = "Portada";
            dgDeptos.Columns[12].DisplayIndex = 12;
            dgDeptos.Columns[12].Visibility = Visibility.Hidden;
            dgDeptos.Columns[13].Header = "Content portada";
            dgDeptos.Columns[13].DisplayIndex = 13;
            dgDeptos.Columns[13].Visibility = Visibility.Hidden;
            dgDeptos.Columns[14].Header = "Fecha creación";
            dgDeptos.Columns[14].DisplayIndex = 14;


        }

        private void btnEditarDepto_Click(object sender, RoutedEventArgs e)
        {





            DepartamentoBl deptoBl = new DepartamentoBl();
            List<DepartamentoTabla> dptos = deptoBl.GetDeptos();

            UsuarioBl userBl = new UsuarioBl();

            var selected = dgDeptos.SelectedItem;
            var deptoTabla = new DepartamentoTabla();

            deptoTabla = (DepartamentoTabla)selected;


            if (selected != null)
            {
                idDpto = (int)deptoTabla.id;
                //MessageBox.Show(deptoTabla.id.ToString());



                Main.Content = new AdmDepartamentos(idDpto);
                imgFondo.Visibility = Visibility.Hidden;
                gridListaDpto.Visibility = Visibility.Hidden;


            }
            else
            {
                MessageBox.Show("Debe seleccionar un departamento en la tabla para poder modificarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            FlyCheckIn.IsOpen = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FlyCheckIn.IsOpen = false;
        }

        private void btnCerrarCheckOut_Click(object sender, RoutedEventArgs e)
        {
            FlyCheckOut.IsOpen = false;
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            FlyCheckOut.IsOpen = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (object o in listBoxCheckout.Items)
                listBoxCheckout.SelectedItems.Add(o);
        }

        private void btnDeactive_Click(object sender, RoutedEventArgs e)
        {
            //Desactivar o activar departamento.

            DepartamentoBl depa = new DepartamentoBl();

            var selected = dgDeptos.SelectedItem;
            var clase = new DepartamentoTabla();

            clase = (DepartamentoTabla)selected;


            if (selected != null)
            {
                if (clase.nombre_estado == "Disponible")
                {
                    MessageBoxResult result = MessageBox.Show("Este departamento está Disponible. ¿Deseas inhabilitarlo?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        //Deshabilitar depa estado 1
                        idDpto = (int)clase.id;
                        try
                        {
                            depa.ActivarDepto(idDpto, 4);
                            CargarDeptos();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                }
                else if (clase.nombre_estado == "No disponible")
                {
                    MessageBoxResult result = MessageBox.Show("Este departamento no está disponible. ¿Deseas habilitarlo?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        //habilitar depa estado 4

                        idDpto = (int)clase.id;
                        try
                        {
                            depa.ActivarDepto(idDpto, 1);
                            CargarDeptos();
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Solo se puede cambiar el estado de los departamento disponibles o no disponibles, intente con otro.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un departamento en la tabla para cambiar su estado", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void btnFiltrarDptos_Click(object sender, RoutedEventArgs e)
        {
            string nombreEstado = cboEstadoFilter.Text;

            string region = cboRegionFilter.Text;
            dgDeptos.ItemsSource = dptos.Where(d => d.nombre_estado.Contains(nombreEstado));
            //dgDeptos.ItemsSource = dptos.Where(d => d.nombre_region.Contains(region));
        }

        private void txtComunaFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnDireccionFilter.Text == string.Empty)
            {
                CargarDeptos();
            }
            else
            {
                string direccion = btnDireccionFilter.Text;
                dgDeptos.ItemsSource = dptos.Where(d => d.direccion.Contains(direccion));
            }
        }

        private void cboRegionFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("here");
            try
            {
                UbicacionBl ubi = new UbicacionBl();

                int idRegion = cboRegionFilter.SelectedIndex + 1;

                //Console.WriteLine(idRegion);
                List<GetComunas> comunas = ubi.GetRegionPorComuna(idRegion);
                cboComunaFilter.ItemsSource = comunas;
                cboComunaFilter.SelectedValuePath = "id_comuna";
                cboComunaFilter.DisplayMemberPath = "nombre_comuna";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CargarDeptos();
        }
    }
}
