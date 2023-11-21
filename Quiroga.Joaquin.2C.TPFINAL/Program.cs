using Entidades.Class;
using Entidades.DBO;
using Entidades.MetodosExtencion;
using System;
using System.Reflection;

namespace Quiroga.Joaquin._2C.TPFINAL
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            Console.WriteLine("#############################");
            try
            {
                var aviones = DBOAviones.GetVuelos();
                //aviones.ActualizarVuelos();
                foreach (var item in aviones)
                {
                    Console.WriteLine($"{item} - {item.Disponible}");
                }
                //DBOAviones.ActualizarVuelos(aviones);

            }
            catch (Exception ex)
            {
                string nombreClase = MethodBase.GetCurrentMethod().DeclaringType.Name;
                string nombreMetodo = MethodBase.GetCurrentMethod().Name;
                Console.WriteLine($"Error: {ex.Message} Clase: {nombreClase} Metodo: {nombreMetodo}");
                Exception inner = ex.InnerException;
                while (inner is not null)
                {
                    nombreClase = MethodBase.GetCurrentMethod().DeclaringType.Name;
                    nombreMetodo = MethodBase.GetCurrentMethod().Name;
                    Console.WriteLine($"Error: {inner.Message} Clase: {nombreClase} Metodo: {nombreMetodo}");                    
                    inner = inner.InnerException;
                }
            }
        }

    }
}