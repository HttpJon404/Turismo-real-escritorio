using Contexto;
using EntidadServicio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidad;

namespace TurismoNegocio
{
    public class ServicioBl
    {

        private static ServicioBl instance;
        public static ServicioBl GetInstance()
        {
            if (instance == null)
            {
                instance = new ServicioBl();
            }
            return instance;
        }



        public List<Servicios> GetServicios()
        {
            try
            {
                DBApi dbapi = new DBApi();
                dynamic users = dbapi.Get("https://localhost:44358/api/Servicio/");
                var resp = users.ToString();

                List<Servicios> jsonDes = JsonConvert.DeserializeObject<List<Servicios>>(resp);
                foreach (var item in jsonDes)
                {
                    item.Id = (int)item.Id;
                    item.Valor = (int)item.Valor;

                }
               
                return jsonDes;

            }
            catch (Exception)
            {

                throw;
            }
        }


        


    }
}
