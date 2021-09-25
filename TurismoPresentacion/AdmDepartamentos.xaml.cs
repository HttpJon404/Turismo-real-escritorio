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
        }

        //Validacion Inputs
        private void ValidacionesInput()
        {
            txtBaños.MaxLength = 2;
            txtDormitorios.MaxLength = 3;
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

        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

    } 
}
