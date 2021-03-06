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
using System.Windows.Shapes;
using EntidadServicio;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using TurismoNegocio;

namespace TurismoPresentacion
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
        }

        public bool ValidarCampos()
        {
            if (txtEmail.Text!= string.Empty && txtPassword.Password!=string.Empty)
            {
                return true;
            }
            return false;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
            {
                await this.ShowMessageAsync("Error", "Debe llenar todos los datos del formulario");
            }
            else
            {

                try
                {
                    string email = txtEmail.Text;
                    string contrasena = txtPassword.Password;

                    UsuarioBl user = new UsuarioBl();

                    var resp = UsuarioBl.GetInstance().Login(email, contrasena);

                    if (resp.EsPositiva)
                    {
                        //Login correcto
                        //Id usuario para saber su rol
                        int idUsuario = int.Parse(resp.Elemento);

                        var rol = UsuarioBl.GetInstance().CompletaSessionUsuario(idUsuario);

                        var rolU = rol.Roles[0].Id;
                        int rolUsuario = (int)rolU;

                        if (rolUsuario==2)
                        {
                            await this.ShowMessageAsync("Error", "Solo los administradores y funcionarios pueden hacer uso de este software, Como usuario puedes visitar nuestra pagina web turismoreal.cl");
                        }
                        else
                        {
                            MainWindow main = new MainWindow(rolUsuario);
                            this.Close();
                            main.Show();
                        }

                    }
                    else
                    {
                        await this.ShowMessageAsync("Error", "Las credenciales ingresadas no son válidas, intente nuevamente.");
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }




        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
