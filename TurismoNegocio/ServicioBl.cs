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

        public Respuesta<string> CrearServicio(string descripcion, int valor)
        {
            try
            {
                DBApi dbapi = new DBApi();

                Servicios servicios = new Servicios()
                {
                    Descripcion = descripcion,
                    Valor = valor
                };

                var json = JsonConvert.SerializeObject(servicios);

                dynamic users = dbapi.Post("https://localhost:44358/api/TipoServicio", json);
                var resp = users.ToString();

                if (resp == "OK")
                {
                    return new Respuesta<string>
                    {
                        Elemento = null,
                        EsPositiva = true,
                        Mensaje = "Creación exitosa"
                    };
                }
                else
                {
                    return new Respuesta<string>
                    {
                        Elemento = null,
                        EsPositiva = false,
                        Mensaje = "Ocurrio un error Inesperado"
                    };
                }

            }
            catch (Exception e)
            {
                return new Respuesta<string>
                {
                    Elemento = null,
                    EsPositiva = false,
                    Mensaje = "Ocurrio un error Inesperado"
                };

            }
        }

        public Respuesta<string> ActualizarServicio(int id, string descripcion, int valor)
        {
            try
            {
                DBApi dbapi = new DBApi();

                Servicios servicios = new Servicios()
                {
                    Id = id,
                    Descripcion = descripcion,
                    Valor = valor
                };

                var json = JsonConvert.SerializeObject(servicios);

                dynamic users = dbapi.Put("https://localhost:44358/api/tiposervicio", json);
                var resp = users.ToString();

                if (resp == "OK")
                {
                    return new Respuesta<string>
                    {
                        Elemento = null,
                        EsPositiva = true,
                        Mensaje = "Actualización exitosa"
                    };
                }
                else
                {
                    return new Respuesta<string>
                    {
                        Elemento = null,
                        EsPositiva = false,
                        Mensaje = "Ocurrio un error Inesperado"
                    };
                }

            }
            catch (Exception e)
            {
                return new Respuesta<string>
                {
                    Elemento = null,
                    EsPositiva = false,
                    Mensaje = "Ocurrio un error Inesperado"
                };

            }
        }

        public Respuesta<string> DeleteServicio(int id)
        {
            try
            {
                DBApi dbapi = new DBApi();

                Servicios servicios = new Servicios()
                {
                    Id = id
                };

                var json = JsonConvert.SerializeObject(servicios);

                dynamic users = dbapi.Deactive("https://localhost:44358/api/TipoServicio", json);
                var resp = users.ToString();

                if (resp == "OK")
                {
                    return new Respuesta<string>
                    {
                        Elemento = null,
                        EsPositiva = true,
                        Mensaje = "Eliminación exitosa"
                    };
                }
                else
                {
                    return new Respuesta<string>
                    {
                        Elemento = null,
                        EsPositiva = false,
                        Mensaje = "Ocurrio un error Inesperado"
                    };
                }

            }
            catch (Exception e)
            {
                return new Respuesta<string>
                {
                    Elemento = null,
                    EsPositiva = false,
                    Mensaje = "Ocurrio un error Inesperado"
                };

            }
        }
    }
}
