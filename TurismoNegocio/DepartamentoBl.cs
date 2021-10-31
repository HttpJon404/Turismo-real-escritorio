using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contexto;
using EntidadServicio;
using Newtonsoft.Json;

namespace TurismoNegocio
{
    public class DepartamentoBl
    {
        private static DepartamentoBl instance;
        public static DepartamentoBl GetInstance()
        {
            if (instance == null)
            {
                instance = new DepartamentoBl();
            }
            return instance;
        }

        public List<Departamento> GetDeptos()
        {
            try
            {
                DBApi dbapi = new DBApi();
                dynamic users = dbapi.Get("https://localhost:44358/api/departamento/");
                var resp = users.ToString();
                List<Departamento> jsonDes = JsonConvert.DeserializeObject<List<Departamento>>(resp);
                foreach ( var depto in jsonDes)
                {
                    depto.id = (int)depto.id;
                    depto.dormitorios = (int)depto.dormitorios;
                    depto.valor_arriendo = (int)depto.valor_arriendo;
                    depto.baños = (int)depto.baños;
                    depto.metrosm2 = (int)depto.metrosm2;


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
