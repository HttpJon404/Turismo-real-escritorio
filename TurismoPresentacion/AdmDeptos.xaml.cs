﻿using System;
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
using EntidadServicio;

namespace TurismoPresentacion
{
    /// <summary>
    /// Lógica de interacción para AdmDeptos.xaml
    /// </summary>
    public partial class AdmDeptos : Page
    {
        public AdmDeptos()
        {
            InitializeComponent();
            CargarDeptos();
        }

        private void CargarDeptos()
        {
            DepartamentoBl deptos = new DepartamentoBl();
            List<DepartamentoTabla> dptos = deptos.GetDeptos();
            dgDeptos.ItemsSource = dptos;
            
        }

        public void Test()
        {

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
            dgDeptos.Columns[9].Header = "Región";
            dgDeptos.Columns[9].DisplayIndex = 9;
            dgDeptos.Columns[10].Header = "Estado";
            dgDeptos.Columns[10].DisplayIndex = 10;
            dgDeptos.Columns[11].Header = "Portada";
            dgDeptos.Columns[11].DisplayIndex = 11;
            dgDeptos.Columns[12].Header = "Content portada";
            dgDeptos.Columns[12].DisplayIndex = 13;
            dgDeptos.Columns[13].Header = "Fecha creación";
            dgDeptos.Columns[13].DisplayIndex = 13;
        }
    }
}
