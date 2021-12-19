using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadServicio
{
    public class DeptoMantencion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
        public int Id_depto { get; set; }
    }
}
