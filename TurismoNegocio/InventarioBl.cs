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
                

                foreach (var item in jsonDes)
                {
                    item.Id = (int)item.Id;
                    //item.Cantidad = (int)item.Cantidad;
                    item.Precio = (int)item.Precio;
                }
                return jsonDes;

            }
            catch (Exception)
            {

                return new List<Inventario>();
            }
        }

        public Respuesta<string> CrearInventario(string descripcion, int precio)
        {
            try
            {
                DBApi dbapi = new DBApi();

                Inventario inventario = new Inventario(){
                      Descripcion = descripcion,
                      Precio = precio
                };

                var json = JsonConvert.SerializeObject(inventario);

                dynamic users = dbapi.Post("https://localhost:44358/api/TipoInventario",json);
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

        public Respuesta<string> ActualizarInventario(int id,string descripcion, int precio)
        {
            try
            {
                DBApi dbapi = new DBApi();

                Inventario inventario = new Inventario()
                {
                    Id= id,
                    Descripcion = descripcion,
                    Precio = precio
                };

                var json = JsonConvert.SerializeObject(inventario);

                dynamic users = dbapi.Put("https://localhost:44358/api/tipoinventario", json);
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

        public Respuesta<string> DeleteInventario(int id)
        {
            try
            {
                DBApi dbapi = new DBApi();

                Inventario inventario = new Inventario()
                {
                    Id = id
                };

                var json = JsonConvert.SerializeObject(inventario);

                dynamic users = dbapi.Deactive("https://localhost:44358/api/TipoInventario", json);
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
