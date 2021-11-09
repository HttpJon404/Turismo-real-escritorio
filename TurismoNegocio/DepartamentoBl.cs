using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contexto;
using EntidadServicio;
using Newtonsoft.Json;
using Utilidad;

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


        public List<EstadoDepto> GetEstadoDepto()
        {
            try
            {
                DBApi dbapi = new DBApi();
                dynamic users = dbapi.Get("https://localhost:44358/api/EstadoDepto/");
                var resp = users.ToString();
                decimal[] lista = new decimal[3];
                lista[0] = 3;
                lista[1] = 4;
                lista[2] = 5;
                List<EstadoDepto> jsonDes = JsonConvert.DeserializeObject<List<EstadoDepto>>(resp);
                List<EstadoDepto> jsonReturn = jsonDes.Where(e => e.Id  !=1 && e.Id!=2).ToList();

                
                return jsonDes;
            }
            catch (Exception)
            {
                return new List<EstadoDepto>();
                
            }
        }


        public List<DepartamentoTabla> GetDeptos()
        {
            try
            {
                DBApi dbapi = new DBApi();
                dynamic users = dbapi.Get("https://localhost:44358/api/departamento/");
                var resp = users.ToString();
                List<DepartamentoTabla> jsonDes = JsonConvert.DeserializeObject<List<DepartamentoTabla>>(resp);
                
                foreach (var depto in jsonDes)
                {
                    depto.id = (int)depto.id;
                    depto.dormitorios = (int)depto.dormitorios;
                    depto.valor_arriendo = (int)depto.valor_arriendo;
                    depto.baños = (int)depto.baños;
                    depto.metrosm2 = (int)depto.metrosm2;

                    if (depto.estacionamiento == "0.0")
                    {
                        depto.estacionamiento = "No";
                    }
                    else
                    {
                        depto.estacionamiento = "Sí";
                    }


                }
                return jsonDes;
            }
            catch (Exception)
            {
                return new List<DepartamentoTabla>();
                
            }
        }

        public List<DepartamentoEdit> GetDeptoId(decimal id)
        {
            try
            {
                DBApi dbapi = new DBApi();
                dynamic users = dbapi.Get("https://localhost:44358/api/departamento/" + id);
                var resp = users.ToString();
                var jsonDes = JsonConvert.DeserializeObject<List<DepartamentoEdit>>(resp);


                return jsonDes;

            }
            catch (Exception)
            {
                return new List<DepartamentoEdit>();
                
            }
        }


        public Respuesta<string> RegistrarDepartamento(int dormitorios, int baños, decimal metrosm2, int estacionamiento, string direccion, int id_comuna, int id_estado,
            decimal valor_arriendo, string condiciones, int[] tipo_inventario, string[] rutaArchivo, string portada)
        {
            try
            {
                DBApi dbApi = new DBApi();


                Departamento depto = new Departamento()
                {
                    dormitorios = dormitorios,
                    baños = baños,
                    metrosm2 = metrosm2,
                    estacionamiento = estacionamiento,
                    direccion = direccion,
                    id_comuna = id_comuna,
                    id_estado = 1,
                    valor_arriendo = valor_arriendo,
                    condiciones = condiciones,
                    tipo_inventario = tipo_inventario,
                    ruta_archivo = rutaArchivo,
                    portada = portada,
                };

                string json = JsonConvert.SerializeObject(depto);

                dynamic respuesta = dbApi.Post("https://localhost:44358/api/departamento/", json);

                var resp = respuesta.ToString();

                List<string> jsonDes = JsonConvert.DeserializeObject<List<string>>(resp);

                if (jsonDes[0] == "DEPARTAMENTO CREADO CON EXITO")
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = true,
                        Elemento = jsonDes[1],
                        Mensaje = jsonDes[0]
                    };
                }
                else
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = false,
                        Elemento = jsonDes[1],
                        Mensaje = jsonDes[0] + "En nuestra base de datos, intente recuperando su contraseña"
                    };
                }


            }
            catch (Exception e)
            {
                Log.Business().Error(e.Message, e);
                return new Respuesta<string>
                {
                    EsPositiva = false,
                    Elemento = null,
                    Mensaje = "Ha ocurrido un error al registrarse, por favor reintentelo nuevamente."
                };
            }
        }



        public Respuesta<string> ActualizarDepartamento(int dormitorios, int baños, decimal metrosm2, int estacionamiento, string direccion, int id_comuna, int id_estado,
           decimal valor_arriendo, string condiciones, int[] tipo_inventario, string[] rutaArchivo, string portada)
        {
            try
            {
                DBApi dbApi = new DBApi();


                Departamento depto = new Departamento()
                {
                    dormitorios = dormitorios,
                    baños = baños,
                    metrosm2 = metrosm2,
                    estacionamiento = estacionamiento,
                    direccion = direccion,
                    id_comuna = id_comuna,
                    id_estado = 1,
                    valor_arriendo = valor_arriendo,
                    condiciones = condiciones,
                    tipo_inventario = tipo_inventario,
                    ruta_archivo = rutaArchivo,
                    portada = portada,
                };

                string json = JsonConvert.SerializeObject(depto);

                dynamic respuesta = dbApi.Post("https://localhost:44358/api/departamento/", json);

                var resp = respuesta.ToString();

                List<string> jsonDes = JsonConvert.DeserializeObject<List<string>>(resp);

                if (jsonDes[0] == "DEPARTAMENTO CREADO CON EXITO")
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = true,
                        Elemento = jsonDes[1],
                        Mensaje = jsonDes[0]
                    };
                }
                else
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = false,
                        Elemento = jsonDes[1],
                        Mensaje = jsonDes[0] + "En nuestra base de datos, intente recuperando su contraseña"
                    };
                }


            }
            catch (Exception e)
            {
                Log.Business().Error(e.Message, e);
                return new Respuesta<string>
                {
                    EsPositiva = false,
                    Elemento = null,
                    Mensaje = "Ha ocurrido un error al registrarse, por favor reintentelo nuevamente."
                };
            }
        }


        public Respuesta<string> ActivarDepto(decimal id, int estado)
        {
            try
            {
                DBApi dbApi = new DBApi();

                int idDepto = (int)id;

                Departamento depto= new Departamento
                {
                    id = idDepto,
                    id_estado = estado,
                };

                string json = JsonConvert.SerializeObject(depto);
                Console.WriteLine(json);
                dynamic respuesta = dbApi.Deactive("https://localhost:44358/api/departamento", json);
                //Console.WriteLine(respuesta);
                //var resp = respuesta.ToString();

                //List<string> jsonDes = JsonConvert.DeserializeObject<List<string>>(resp);
                var resp = respuesta.ToString();

                List<string> jsonDes = JsonConvert.DeserializeObject<List<string>>(resp);

                if (jsonDes[0] == "ESTADO ACTUALIZADO CORRECTAMENTE")
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = true,
                        Elemento = jsonDes[1],
                        Mensaje = "Estado del departamento actualizado correctamente."
                    };
                }
                else
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = false,
                        Elemento = jsonDes[1],
                        Mensaje = jsonDes[0] + "En nuestra base de datos, intente recuperando su contraseña"
                    };
                }



            }
            catch (Exception e)
            {
                Log.Business().Error(e.Message, e);
                return new Respuesta<string>
                {
                    EsPositiva = false,
                    Elemento = null,
                    Mensaje = "Ha ocurrido un error al "
                };
            }
        }



        public List<Checkin> GetInventarioDpto(decimal id)
        {
            try
            {
                int idDpto = (int)id;
                DBApi dbApi = new DBApi();
                dynamic dpto = dbApi.Get("https://localhost:44358/api/checkin/" + idDpto);
                var resp = dpto.ToString();
                List<Checkin> jsonDes = JsonConvert.DeserializeObject<List<Checkin>>(resp);

                var list = new List<Checkin>();
                var check = new Checkin();
                for (int i = 0; i < jsonDes.Count; i++)
                {
                    check.id = Convert.ToInt32(jsonDes[i].id);
                    check.descripcion = jsonDes[i].descripcion;

                    check.precio = Convert.ToInt32(jsonDes[i].precio);

                    list.Add(check);
                    check = new Checkin();
                }



                return list;

            }
            catch (Exception e)
            {
                return new List<Checkin>();
               
            }
        }

        public Respuesta<Departamento> GenerarCheckin(decimal id)
        {
            try
            {
                //int idDpto = (int)id;

                DBApi dBApi = new DBApi();

                Departamento dep = new Departamento()
                {
                    id = (int)id
                };

                var json = JsonConvert.SerializeObject(dep);

                dynamic dpto = dBApi.Post("https://localhost:44358/api/checkin", json);
                var resp = dpto.ToString();

                if (resp == "CHECKIN GENERADO CON EXITO") 
                {
                    return new Respuesta<Departamento>()
                    {
                        EsPositiva = true,
                        Elemento = null,
                        Mensaje = "CHECKIN GENERADO CON EXITO"
                    };
                }
                else
                {
                    return new Respuesta<Departamento>()
                    {
                        EsPositiva = false,
                        Elemento = null,
                        Mensaje = "OCURRIO UN ERROR INESPERADO, REINTENTE NUEVAMENTE"
                    };
                }
            }
            catch (Exception)
            {
                return new Respuesta<Departamento>();
                //throw;
            }
        }


        public Respuesta<Checkout> GenerarCheckout(decimal id, int multa)
        {
            try
            {
                //int idDpto = (int)id;

                DBApi dBApi = new DBApi();

                Checkout check = new Checkout()
                {
                    id = (int)id,
                    multa = multa
                };

                var json = JsonConvert.SerializeObject(check);

                dynamic dpto = dBApi.Put("https://localhost:44358/api/checkin", json);
                var resp = dpto.ToString();

                if (resp == "CHECKOUT GENERADO CON EXITO")
                {
                    return new Respuesta<Checkout>()
                    {
                        EsPositiva = true,
                        Elemento = null,
                        Mensaje = "CHECKOUT GENERADO CON EXITO"
                    };
                }
                else
                {
                    return new Respuesta<Checkout>()
                    {
                        EsPositiva = false,
                        Elemento = null,
                        Mensaje = "OCURRIO UN ERROR INESPERADO, REINTENTE NUEVAMENTE"
                    };
                }
            }
            catch (Exception)
            {
                return new Respuesta<Checkout>();
                //throw;
            }
        }


    }
}
