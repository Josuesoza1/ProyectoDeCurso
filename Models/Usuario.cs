public class Usuario
{
    private int _id;
    private string _nombre = string.Empty;
    private string _apellido = string.Empty;
    private string _correo = string.Empty;
    private string _telefono = string.Empty;
    private DateTime FechaRegistro;
    private List<int> historialPrestamoIds;

    public int Id
    {
        get => _id;
        set
        {
            if (value < 0)
                throw new ArgumentException("El ID no puede ser negativo");
        }

    }
    public string? Nombre
    {
        get => _nombre;
        private set => _nombre = ValidarTexto(value, "nombre");
    }
    public string? Apellido
    {
        get => _apellido;
        private set => _apellido = ValidarTexto(value, "apellido");
    }
    public string? Correo
    {
        get => _correo;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El correo no puede estar vacío");
            if (!value.Contains("@") || !value.Contains("."))
                throw new ArgumentException("El correo debe contener '@' y '.'");
            _correo = value;
        }
    }
    public string? Telefono
    {
        get => _telefono;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El número de teléfono no puede estar vacío.");
            }
            if (value.Length != 8)
            {
                throw new ArgumentException("El número de telefono debe contener 8 digitos. ");
            }
            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                {
                    throw new ArgumentException("El número de teléfono solo debe contener números.");
                }
            }
            char var = value[0];
            if (!"578".Contains(var))
            {
                throw new ArgumentException("Error en el prefijo numero de telefono debe iniciar con el prefijo 5,7 u 8 ");
            }
            _telefono = value;
        }
    }

    public DateTime FechaRegistro1 { get => FechaRegistro; private set => FechaRegistro = value; }
    public List<int> HistorialPrestamoIds { get => historialPrestamoIds; private set => historialPrestamoIds = value; }



    public Usuario(int id, string? nombre, string? apellido, string? correo, string? telefono)
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
    public string NombreCompleto => $"{Nombre} {Apellido}";


    public override string ToString()
    {
        return $"ID:{Id} | {NombreCompleto} | Correo: {Correo} | Tel: {Telefono} | " +
               $"Registrado: {FechaRegistro:dd/MM/yyyy}";
    }
}

