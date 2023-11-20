

namespace Entidades.Class
{
    public class Persona
    {
        private string _nombre;
        private string _apellido;
        private int _edad;
        private string _genero;
        private string _correo;
        private string _password;

        public Persona(string nombre, string apellido, int edad, string genero)
        {
            this._nombre = nombre;
            this._apellido = apellido;
            this._edad = edad;
            this._genero = genero;
        }

        public string Nombre { get => this._nombre; set => this._nombre = value; }
        public string Apellido { get => this._apellido; set => this._apellido = value; }
        public int Edad { get => this._edad; set => this._edad = value; }
        public string Genero { get => this._genero; set => this._genero = value; }
        public string Correo { get => this._correo;  set => this._correo = value; }
        public string Password { get => this._password;  protected set => this._password = value; }

    }
}