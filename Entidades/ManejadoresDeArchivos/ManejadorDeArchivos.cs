using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entidades.Excepciones;

namespace Entidades.Class.Archivos
{
    public static class ManejadorDeArchivos<T>
    {
        public const string NOMBRE_CARPETA = "Archivos json";

        /// <summary>
        /// Este metodo se encargar serializar a tipo JSON una lista de tipo generica escribirlos, en el nombre del archivo que se pase.
        /// </summary>
        /// <param name="objs">Lista generica que se va a escribir en el archivo JSON</param> 
        /// <param name="nombreArchivo">Nombre del archivo JSON donde se van a escribir los datos</param>
        /// <exception cref="ArchivoInvalidException"></exception>
        public static void EscribirArchivo(List<T> objs, string nombreArchivo)
        {
            if (objs.Count == 0 || nombreArchivo is null)
            {
                throw new ArgumentNullException("La lista es vacia o el path es nulo");
            }
            else
            {
                string pathDir = ManejadorDeArchivos<T>.CrearCarpeta();
                string pathArchivo = Path.Combine(pathDir, nombreArchivo);
                JsonSerializerOptions opcion = new JsonSerializerOptions();
                opcion.WriteIndented = true;
               
                using (StreamWriter sw = new StreamWriter(pathArchivo))
                {
                    string archivoJson = JsonSerializer.Serialize(objs, opcion);
                    sw.WriteLine(archivoJson);
                }              
            }
        }
        /// <summary>
        /// Este metodo se encargar de leer el archivo de tipo JSON y devolver el tipo de dato en una lista
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <returns>Retorna una lista con los elementos que encontro en el archivo</returns>
        /// <exception cref="ArchivoInvalidException"></exception>
        public static List<T> LeerArchivo(string nombreArchivo)
        {
            if(nombreArchivo is null)
            {
                throw new ArgumentNullException("Path nulo");
            }
            else
            {
                string pathDir = ManejadorDeArchivos<T>.CrearCarpeta();
                string pathArchivo = Path.Combine(pathDir, nombreArchivo);
                List<T> elemento;
                try
                {
                    using (StreamReader sr = new StreamReader(pathArchivo))
                    {
                        string archivoJson = sr.ReadToEnd();
                        elemento = JsonSerializer.Deserialize<List<T>>(archivoJson);
                        return elemento;
                    }
                }
                catch (Exception ex)
                {
                    throw new ArchivoInvalidException("Error al leer el archivo", ex);
                }
            }            
        }

        /// <summary>
        /// Este se encarga de una coincidencia de acuerdo al metodo de busqueda del buscador en el archivo que se pasa por parametro
        /// </summary>
        /// <param name="buscador">Predicado que se encargara de implementar su metodo de coincidencia</param>
        /// <param name="nombreArchivo">Nombre del archivo donde se buscaran las coincidencias</param>
        /// <returns>Retornara true si se encuentra la coincidencia o false de caso contrario</returns>
        public static bool BuscarCoincidencia(Predicate<T> buscador, string nombreArchivo)
        {
            bool rtn = false;
            try
            {
                List<T> lista = ManejadorDeArchivos<T>.LeerArchivo(nombreArchivo);
                foreach (T obj in lista)
                {
                    if (buscador(obj))
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
        /// <summary>
        /// Este se encarga de una coincidencia de acuerdo al metodo de busqueda y retornar la coincidencia del buscador en el archivo que se pasa por parametro
        /// de lo contrario retornara el default del elemento
        /// </summary>
        /// <param name="buscador">Predicado que se encargara de implementar su metodo de coincidencia</param>
        /// <param name="nombreArchivo">Nombre del archivo donde se buscaran las coincidencias</param>
        /// <returns>Retornara el elemento o un default</returns>
        public static T? BuscarCoincidenciaYRetornarElemento(Predicate<T> buscador, string nombreArchivo)
        {
           
            List<T> lista = ManejadorDeArchivos<T>.LeerArchivo(nombreArchivo);
            foreach (T obj in lista)
            {
                if (buscador(obj))
                {
                    return obj;
                }
            }
            return default;            
        }
        /// <summary>
        /// Este metodo se encarga de crear una carpeta, donde se contendran los archivos.
        /// </summary>
        /// <returns>Retornara el nombre de la carpeta creada o existente</returns>
        public static string CrearCarpeta()
        {
            string pathDir = Path.Combine(".", NOMBRE_CARPETA);
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            return pathDir;
        }


    }
}
