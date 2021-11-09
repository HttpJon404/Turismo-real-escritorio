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







    }
}
