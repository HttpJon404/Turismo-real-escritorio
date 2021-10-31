using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Contexto;
using EntidadServicio;
using Utilidad;


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


                foreach(var usuario in jsonDes)
                {
                    usuario.id = (int)usuario.id;
                    usuario.edad = (int)usuario.edad;
                }

                return jsonDes;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Usuario> GetUserId(decimal id)
        {
            try
            {
                DBApi dbapi = new DBApi();
                dynamic users = dbapi.Get("https://localhost:44358/api/usuarios/"+id);
                var resp = users.ToString();
                var jsonDes = JsonConvert.DeserializeObject<List<Usuario>>(resp);

                foreach (var usuario in jsonDes)
                {
                    usuario.id = (int)usuario.id;
                    usuario.edad = (int)usuario.edad;
                }

                return jsonDes;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Respuesta<string> RegistrarUsuario(string nombre, string apellidos, decimal edad, string rut, string idGenero, decimal idComuna, decimal idRegion,
                           string direccion, string correo, string celular, string contrasena, decimal idRol, string estado)
        {
            try
            {
                DBApi dbApi = new DBApi();

                if (idGenero == "1")
                {
                    idGenero = "M";
                }
                else
                {
                    idGenero = "F";
                }
                contrasena = CripSha1.Encriptar(contrasena);
                UsuarioCreate usuario = new UsuarioCreate
                {
                    id_comuna = idComuna,
                    id_region = idRegion,
                    rut = rut,
                    nombres = nombre,
                    apellidos = apellidos,
                    edad = edad,
                    direccion = direccion,
                    correo = correo,
                    celular = celular,
                    genero = idGenero,
                    contrasena = contrasena,
                    estado = estado,
                    id_rol = idRol
                };

                string json = JsonConvert.SerializeObject(usuario);

                dynamic respuesta = dbApi.Post("https://localhost:44358/api/usuarios", json);
                Console.WriteLine(respuesta);
                var resp = respuesta.ToString();

                List<string> jsonDes = JsonConvert.DeserializeObject<List<string>>(resp);

                if (jsonDes[0] == "DATOS INGRESADOS CORRECTAMENTE")
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = true,
                        Elemento = jsonDes[1],
                        Mensaje = "Usuario registrado correctamente."
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


        //Editar usuario PUT
        public Respuesta<string> EditarUsuario(decimal id, string nombre, string apellidos, string idGenero, decimal idComuna, decimal idRegion,
                           string direccion, string correo, string celular, string contrasena, decimal idRol, string estado)
        {
            try
            {
                DBApi dbApi = new DBApi();

                if (idGenero == "1")
                {
                    idGenero = "M";
                }
                else
                {
                    idGenero = "F";
                }
                contrasena = CripSha1.Encriptar(contrasena);
                int idUsuario = (int)id;
                UsuarioEdit usuario = new UsuarioEdit
                {
                    id = idUsuario,
                    id_comuna = idComuna,
                    id_region = idRegion,
                    nombres = nombre,
                    apellidos = apellidos,
                    direccion = direccion,
                    correo = correo,
                    celular = celular,
                    contrasena = contrasena,
                    estado = estado,
                    genero = idGenero,
                    id_rol = idRol
                };

                string json = JsonConvert.SerializeObject(usuario);
                Console.WriteLine(json);
                dynamic respuesta = dbApi.Put("https://localhost:44358/api/usuarios", json);
                //Console.WriteLine(respuesta);
                //var resp = respuesta.ToString();

                //List<string> jsonDes = JsonConvert.DeserializeObject<List<string>>(resp);
                var resp = respuesta.ToString();

                List<string> jsonDes = JsonConvert.DeserializeObject<List<string>>(resp);

                if (jsonDes[0] == "DATOS ACTUALIZADOS CORRECTAMENTE")
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = true,
                        Elemento = jsonDes[1],
                        Mensaje = "Usuario modificado correctamente."
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


        public Respuesta<string> ActivarUsuario(decimal id, string estado)
        {
            try
            {
                DBApi dbApi = new DBApi();

                int idUsuario = (int)id;

                EditEstadoUsuario usuario = new EditEstadoUsuario
                {
                    id = idUsuario,
                    estado = estado,
                };

                string json = JsonConvert.SerializeObject(usuario);
                Console.WriteLine(json);
                dynamic respuesta = dbApi.Deactive("https://localhost:44358/api/usuarios", json);
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
                        Mensaje = "Estado del usuario  correctamente."
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

        public List<UsuarioTabla> UsuariosGrid()
        {
            UsuarioBl usuariobl = new UsuarioBl();
            List<UsuarioTabla> userListTabla = new List<UsuarioTabla>();

            List<Usuario> userList = new List<Usuario>();

            userList = usuariobl.GetUsers();

            UbicacionBl ubi = new UbicacionBl();
            List<GetComunas> regiones = ubi.GetComunas();

            foreach (var user in userList)
            {
                UsuarioTabla usuario = new UsuarioTabla();
                usuario.id = (int)user.id;
                usuario.rut = user.rut;
                usuario.nombres = user.nombres;
                usuario.apellidos = user.apellidos;
                usuario.direccion = user.direccion;
                usuario.correo = user.correo;
                usuario.celular = user.celular;
                usuario.edad = (int)user.edad;
                usuario.genero = user.genero;
                usuario.descripcion = user.descripcion;
                Console.WriteLine(user.estado);

                if (user.estado == "1.0")
                {
                    usuario.estado = "Habilitado";
                }
                else
                {
                    usuario.estado = "Inhabilitado";
                }
                //nombre region
                foreach (var region in regiones)
                {
                    if (user.id_region == region.id_region)
                    {
                        usuario.nombre_region = region.nombre_region;
                    }
                }

                //nombre comuna
                foreach (var region in regiones)
                {
                    if (user.id_comuna == region.id_comuna)
                    {
                        usuario.nombre_comuna = region.nombre_comuna;
                    }
                }

                userListTabla.Add(usuario);

            }
            return userListTabla;

        }

        public bool LoginUsuario(string correo, string clave)
        {
            List<Usuario> users = this.GetUsers();
            foreach (var flash in users)
            {
                if (flash.correo ==correo)
                {
                    return true;

                }
            }
            return false; ;
        }

        public List<UsuarioTabla> FiltrarUsuarios(string rut)
        {
            List<UsuarioTabla> users = this.UsuariosGrid();

            var filterUsers = users.Where(u => u.rut == rut).ToList();


            return filterUsers;
        }

        //Login

        public Respuesta<string> Login(string correo, string contrasena)
        {
            try
            {
                DBApi dbApi = new DBApi();

                contrasena = CripSha1.Encriptar(contrasena);

                Login login = new Login()
                {
                    correo = correo,
                    contrasena = contrasena
                };

                string json = JsonConvert.SerializeObject(login);

                dynamic respuesta = dbApi.Post("https://localhost:44358/api/login", json);

                var resp = respuesta.ToString();

                List<string> jsonDes = JsonConvert.DeserializeObject<List<string>>(resp);

                if (jsonDes[0] == "OK")
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = true,
                        Elemento = jsonDes[1],
                        Mensaje = "Inicio de sesión correctamente."
                    };
                }
                else
                {
                    return new Respuesta<string>
                    {
                        EsPositiva = false,
                        Elemento = null,
                        Mensaje = jsonDes[0]
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
                    Mensaje = "Ha ocurrido un error al ingresar, por favor reintentelo nuevamente."
                };
            }
        }

    }
}
