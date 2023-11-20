using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Class;
namespace Entidades.MetodosExtencion
{
    public static class MiMetodoExtension
    {
        public static DateTime DevolverHorarioDeUltimoVuelo(this List<Avion> aviones)
        {
            DateTime ultimoVuelo = DateTime.MinValue;
            if( aviones is null ) 
            {
                return ultimoVuelo;
            } 
            else 
            {
                foreach (Avion item in aviones)
                {
                    if (item.Disponible && item.HoraDeSalida > ultimoVuelo)
                    {
                        ultimoVuelo = item.HoraDeSalida;
                    }
                }
                return ultimoVuelo;
            }
            
        }
        public static List<Avion> GeneradorDeVuelos(this List<Avion> aviones)
        {
            string[] destinos = new string[] { "Brasil", "Peru", "Mexico", "Venezuela", "Uruguay", "Paraguay", "España" };
            Random random = new Random();
            List<Avion> avionesNuevos = new List<Avion>();
            for (int i = 0; i < 10; i++)
            {
                AvionModelo avion = new AvionModelo();
                
                avion.Origen = "Argentina";
                avion.Destino = destinos[random.Next(0, destinos.Count())];
                
                switch (avion.Destino)
                {
                    case "España":
                        avion.HorasDeVuelo = 8;
                        avion.Costo = 4000;
                        break;
                    case "Mexico":
                        avion.HorasDeVuelo = 4;
                        avion.Costo = 3000;

                        break;
                    default:
                        avion.HorasDeVuelo = random.Next(1, 3);
                        avion.Costo = random.Next(1500, 2500);
                        break;
                }
                
                avion.HoraDeSalida = DateTime.Now.AddHours(random.Next(1, 6));
                avion.Disponible = true;

                avionesNuevos.Add(avion.GetElement());
            }
            return avionesNuevos;
        }
        public static void ActualizarVuelos(this List<Avion> aviones)
        {
            foreach (Avion avion in aviones)
            {
                avion.Disponible = false;
            }
           
        }
    }
}
