using Entidades.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Class
{
    public class Avion
    {
        private int _id;
        private string _origen;
        private string _destino;
        private double _costo;
        private int _horasDeVuelo;
        private DateTime _horaDeSalida;
        private bool disponible;       

        public Avion(int id, string origen, string destino, double costo, int horasDeVuelo, DateTime horaDeSalida)
        {
            this._id = id;
            this._origen = origen;
            this._destino = destino;
            this._costo = costo;
            this._horasDeVuelo = horasDeVuelo;
            this._horaDeSalida = horaDeSalida;
        }

        public int Id { get => this._id; set => this._id = value; }
        public string Origen { get => this._origen; set => this._origen = value; }
        public string Destino { get => this._destino; set => this._destino = value; }
        public double Costo { get => this._costo; set => this._costo = value; }
        public int HorasDeVuelo { get => this._horasDeVuelo; set => this._horasDeVuelo = value; }
        public DateTime HoraDeSalida { get => this._horaDeSalida; set => this._horaDeSalida = value; }
        public bool Disponible { get => this.disponible; set => this.disponible = value; }

        public decimal CalcularCostoConAumento()
        {
            decimal porcentajeAdicional = 50.0m;
            decimal porcentaje = porcentajeAdicional / 100.0m;
            decimal aumento = (decimal)this.Costo * porcentaje;
            return (decimal)this.Costo + aumento;
        }
        public override string ToString()
        {
            string message = $"Vuelo de {this.Origen} a => {this.Destino} a las {this.HoraDeSalida.ToString("HH:mm:ss tt")}";
            if (!this.Disponible)
            {
                message = $"Vuelo de {this.Origen} a => {this.Destino} || Ya no se encuentra disponible ||";
            }
            return message;
        }
    }
}
