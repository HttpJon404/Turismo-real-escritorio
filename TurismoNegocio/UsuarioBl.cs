using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Contexto;
using EntidadServicio;

namespace TurismoNegocio
{
    public class UsuarioBl
    {
        private static UsuarioBl instance;
        public static UsuarioBl GetInstance()
        {
            if (instance == null)
            {
                instance = new UsuarioBl();
            }
            return instance;
        }

        public List<Usuario> GetUsers()
        {
            try
            {
                DBApi dbapi = new DBApi();
                dynamic users = dbapi.Get("https://localhost:44358/api/usuarios/");
                var resp = users.ToString();
                List<Usuario> jsonDes = JsonConvert.DeserializeObject<List<Usuario>>(resp);
                Console.WriteLine(users);
                Console.WriteLine("---------------------");
                Console.WriteLine(jsonDes);

                return jsonDes;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
