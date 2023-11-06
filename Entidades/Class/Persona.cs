namespace Entidades.Class
{
    public class Persona
    {
        private string _nombre;
        private string _apellido;
        private uint _edad;
        private uint _dni;
        private string _correo;
        private string _password;
        public static string correoIncorrecto;

        static Persona()
        {
            Persona.correoIncorrecto = "Correo Invalido";
        }

        public Persona(string nombre, string apellido, uint edad, uint dni)
        {
            this._nombre = nombre;
            this._apellido = apellido;
            this._edad = edad;
            this._dni = dni;
        }

        public string Nombre { get => _nombre;}
        public string Apellido { get => _apellido;}
        public uint Edad { get => _edad; }
        public uint Dni { get => _dni; }
        public string Correo { get => _correo; protected set => _correo = value; }
        internal protected string Password { get => _password;  protected set => _password = value; }

        private void AgregarCorreo(string correo)
        {
            if (this.ValidarCorreo(correo)) 
            {
                this.Correo = correo;
            }
            else
            {
                this.Correo = Persona.correoIncorrecto;
            }
        }
        private void AgregarPassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                this.Password = password;
            }
            else
            {
                this.Password = "";
            }
        }
        public bool CrearUsuario(string correo,string password)
        {
            this.AgregarCorreo(correo); 
            this.AgregarPassword(password);

            return this.Correo != Persona.correoIncorrecto && this.Password != "";

        }
        public bool ValidarCorreo(string correo)
        {            
            return !string.IsNullOrEmpty(correo) && correo.Split('@') == new string[2];
        }
    }
}