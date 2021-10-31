using Contexto;
using EntidadServicio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurismoNegocio
{
    public class InventarioBl
    {
        private static InventarioBl instance;
        public static InventarioBl GetInstance()
        {
            if (instance == null)
            {
                instance = new InventarioBl();
            }
            return instance;
        }



        public List<Inventario> Getinventario()
        {
            try
            {
                DBApi dbapi = new DBApi();
                dynamic users = dbapi.Get("https://localhost:44358/api/TipoInventario/");
                var resp = users.ToString();
                List<Inventario> jsonDes = JsonConvert.DeserializeObject<List<Inventario>>(resp);

                return jsonDes;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
