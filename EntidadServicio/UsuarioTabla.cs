using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadServicio
{
    public class UsuarioTabla
    {
        public decimal id { get; set; }

        public string nombre_region { get; set; }
        public string nombre_comuna { get; set; }
        public string rut { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public decimal edad { get; set; }
        public string genero { get; set; }
        public string estado { get; set; }
        public string descripcion { get; set; }
    }
}
