using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadServicio
{
    public class UsuarioEdit
    {
        public decimal id { get; set; }
        public decimal id_comuna { get; set; }
        public decimal id_region { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public string genero { get; set; }
        public string contrasena { get; set; }
        public string estado { get; set; }
        public decimal id_rol { get; set; }
    }
}
