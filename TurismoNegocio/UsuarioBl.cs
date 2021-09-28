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
                Console.WriteLine(users);
                Console.WriteLine("---------------------");
                Console.WriteLine(jsonDes[0].id.ToString());

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


        public bool EditarUsuario(string idLink, string newPass)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminarUsuario(decimal id)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {

                return false;
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


    }
}
