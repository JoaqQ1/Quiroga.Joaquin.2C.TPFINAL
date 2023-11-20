using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Excepciones;
using Entidades.Interfases;

namespace Entidades.Class.Archivos
{
    public static class ManejadorDeUsuario
    {
        const string PATH_USUARIO = "Usuarios.json";

        public static bool BuscarUsuario(Usuario usuario)
        {
            bool rtn = false;
            try
            {
                List<Usuario> lista = ManejadorDeArchivos<Usuario>.LeerArchivo(PATH_USUARIO);
                foreach (Usuario obj in lista)
                {
                    if (usuario == obj)
                    {
                        rtn = true;
                    }
                }
                return rtn;
            }
            catch (ArchivoInvalidException)
            {
                return rtn;
            }
        }
        public static int GetUltimoIdUsuario()
        {
            try
            {
                List<int> ids = ManejadorDeArchivos<int>.LeerArchivo("Ids.json");
                int ultimoId = ids.Last() + 1;
                ids.Add(ultimoId);
                ManejadorDeArchivos<int>.EscribirArchivo(ids, "Ids.json");
                return ultimoId;
            }
            catch (ArchivoInvalidException)
            {
                List<int> ids = new() { 0 };
                ManejadorDeArchivos<int>.EscribirArchivo(ids, "Ids.json");
                return ids.Last();
            }
        }

        /// <summary>
        /// Busca si hay un usuario con el mismo correo en el archivo usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Retornara true si encunetra coincidencia con el correo o false de caso contrario</returns>
        public static bool BuscarUsuarioEnArchivo(Usuario usuario)
        {
            return ManejadorDeArchivos<Usuario>.BuscarCoincidencia(usu => usu.Correo == usuario.Correo, PATH_USUARIO); 
        }

        public static void SerializarUsuarios(List<Usuario> usuarios)
        {
            ManejadorDeArchivos<Usuario>.EscribirArchivo(usuarios, PATH_USUARIO);
        }
        public static List<Usuario> DevolverListaDeUsuarios()
        {
            try
            {
                List<Usuario> usuarios = ManejadorDeArchivos<Usuario>.LeerArchivo(PATH_USUARIO);
                return usuarios;

            }
            catch (Exception)
            {
                return new List<Usuario>();
            }
        }
        /// <summary>
        /// Se encarga de buscar a un usuario en el archivo con el mismo correo y password
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="password"></param>
        /// <returns>En caso de encontrar la coincidencia retornara el usuario</returns>
        public static Usuario RetornarUsuarioLogeado(string correo,string password)
        {    
            return ManejadorDeArchivos<Usuario>.BuscarCoincidenciaYRetornarElemento(usuario => usuario.Correo == correo && usuario.Password == password, PATH_USUARIO);
        }
    }
}
