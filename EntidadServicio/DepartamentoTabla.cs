using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadServicio
{
    public class DepartamentoTabla
    {

        public decimal id { get; set; }
        public decimal id_usuario { get; set; }
        public string direccion { get; set; }
        public decimal dormitorios { get; set; }
        public decimal valor_arriendo { get; set; }
        public decimal baños { get; set; }
        public decimal metrosm2 { get; set; }
        public string estacionamiento { get; set; }
        public string condiciones { get; set; }
        public string nombre_comuna { get; set; }
        public decimal region { get; set; }
        public string nombre_region { get; set; }

        public string nombre_estado { get; set; }

       
//      public string resultado { get; set; }
        public string portada { get; set; }
        public string content_portada { get; set; }
        public DateTime fecha_creacion { get; set; }

     


    }
}
