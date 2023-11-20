using Entidades.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Class
{
    public class AvionModelo: IAccionesClasesModelo<Avion>
    {
        public string Origen {  get; set; }
        public string Destino { get; set; }
        public int HorasDeVuelo { get; set; }
        public double Costo { get; set; }
        public DateTime HoraDeSalida { get; set; }
        public int Id { get; set; }
        public bool Disponible { get; set; }

        public Avion GetElement()
        {
            Avion avion = new Avion(this.Id, this.Origen, this.Destino, this.Costo, this.HorasDeVuelo, this.HoraDeSalida);
            avion.Disponible = this.Disponible;
            return avion;
        }

    }
}
