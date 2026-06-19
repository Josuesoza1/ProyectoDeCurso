/// <summary>
/// Entidad que representa a un lector o miembro de la biblioteca registrado en el sistema.
/// </summary>
public class User
{
    private int _id;
    private string _nombre = string.Empty;
    private string _apellido = string.Empty;
    private string _correo = string.Empty;
    private string _telefono = string.Empty;

    /// <summary>
    /// Identificador numérico único del usuario en la base de datos.
    /// </summary>
    public int Id
    {
        get => _id;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("El ID no puede mayor que 0.");
            _id = value;
        }
    }

    /// <summary>
    /// Nombres de pila del usuario.
    /// </summary>
    public string? Nombre
    {
        get => _nombre;
        private set => _nombre = ValidarTexto(value, "nombre");
    }

    /// <summary>
    /// Apellidos legales del usuario.
    /// </summary>
    public string? Apellido
    {
        get => _apellido;
        private set => _apellido = ValidarTexto(value, "apellido");
    }

    /// <summary>
    /// Dirección de contacto electrónico válida.
    /// </summary>
    public string? Correo
    {
        get => _correo;
        private set
        {
            string correoLimpio = (value ?? "").Trim();
            if (string.IsNullOrWhiteSpace(correoLimpio) || !correoLimpio.Contains("@") || !correoLimpio.Contains(".") || correoLimpio.Length < 5)
                throw new ArgumentException("El correo debe contener '@', '.' y ser una dirección válida.");
            _correo = correoLimpio;
        }
    }

    /// <summary>
    /// Número de contacto directo de 8 dígitos según la normativa nacional.
    /// </summary>
    public string? Telefono
    {
        get => _telefono;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El número de teléfono no puede estar vacío.");
            if (value.Length != 8)
                throw new ArgumentException("El número de telefono debe contener 8 digitos. ");
            foreach (char c in value)
                if (!char.IsDigit(c))
                    throw new ArgumentException("El número de teléfono solo debe contener números.");
            char var = value[0];
            if (!"578".Contains(var))
                throw new ArgumentException("Error en el prefijo numero de telefono debe iniciar con el prefijo 5,7 u 8 ");
            _telefono = value;
        }
    }

    /// <summary>
    /// Constructor principal para la creación de un nuevo perfil de usuario.
    /// </summary>
    public User(int id, string? nombre, string? apellido, string? correo, string? telefono)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Correo = correo;
        Telefono = telefono;
    }

    private string? ValidarTexto(string? texto, string campo)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException($"El {campo} es obligatorio");
        if (texto.Trim().Length < 2)
            throw new ArgumentException($"El {campo} debe tener al menos 2 caracteres");
        return texto.Trim();
    }

    public void ActualizarNombre(string? nuevoNombre) => Nombre = nuevoNombre;
    public void ActualizarApellido(string? nuevoApellido) => Apellido = nuevoApellido;
    public void ActualizarCorreo(string? nuevoCorreo) => Correo = nuevoCorreo;
    public void ActualizarTelefono(string? nuevoTelefono) => Telefono = nuevoTelefono;

    /// <summary>
    /// Propiedad calculada que concatena el nombre y apellido.
    /// </summary>
    public string NombreCompleto => $"{Nombre} {Apellido}";

    public override string ToString()
    {
        return $" ID: {Id,-4} | {NombreCompleto,-25} |  {Correo,-25} |  {Telefono}";
    }
}