using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Class
{
    public class Usuario : Persona 
    {
        private bool _activo;
        private uint _id;
        public uint Id {  get { return this._id; } private set { this._id = value; } }
        public bool Activo {  get { return this._activo; } }

        /// <summary>
        ///  Se encarga de setearle un id al usuario
        /// </summary>
        private void CargarId()
        {
            uint idUsuario = this.BuscarId();
            this.Id = idUsuario;
        }
        /// <summary>
        /// Busca y retorna el id en la base de datos.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private uint BuscarId()
        {
            throw new NotImplementedException();
        }

        

        public Usuario(string nombre, string apellido, uint edad, uint dni,string correo,string password)
            : base(nombre, apellido, edad, dni)
        {            
            if(base.CrearUsuario(correo, password))
            {
                this._activo = true;
                this.CargarId();
            }
            else
            {
                this._activo = false;
            }
        }
        
        public void CambiarPassword(string passwordNew,string passwordOld)
        {
            string passwordActual = this.Password;

            if(!string.IsNullOrEmpty(passwordActual) && passwordOld == passwordActual) 
            {
                this.Password = passwordNew;
            }
        }

    }
}
