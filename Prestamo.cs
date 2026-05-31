
public class Prestamo
{
    private int _id;
    private int _usuarioID;
    private int _itemID;
    private string? _tipoItem;
    private DateTime _fechaPrestamo;
    private DateTime _fechaDevolucionEsperada;
    private DateTime? _fechaDevolucionReal;
    private string? _observaciones;


    public int Id
    {
        get => _id;
        set
        {
            if (value < 0)
                throw new ArgumentException("El id no puede ser negativo");
            _id = value;
        }
    }
    public int UsuarioID
    {
        get => _usuarioID;
        set
        {
            if (value < 0)
                throw new ArgumentException("El id no puede ser negativo");
            _usuarioID = value;
        }
    }
    public int ItemID
    {
        get => _itemID;
        set
        {
            if (value < 0)
                throw new ArgumentException("El id no puede ser negativo");
            _itemID = value;
        }
    }
    public string? TipoItem
    {
        get => _tipoItem;
        set => _tipoItem = ValidarTexto(value, "tipoItem");
    }
    public DateTime FechaPrestamo { get => _fechaPrestamo; set => _fechaPrestamo = value; }
    public DateTime FechaDevolucionEsperada { get => _fechaDevolucionEsperada; set => _fechaDevolucionEsperada = value; }
    public DateTime? FechaDevolucionReal { get => _fechaDevolucionReal; set => _fechaDevolucionReal = value; }
    public string? Observaciones
    {
        get => _observaciones;
        set => _observaciones = ValidarTexto(value,"observaciones");
    }



    public Prestamo() { }

    public Prestamo(int id, int usuarioId, int itemId, string tipoItem, int diasPrestamo = 14)
    {
        Id = id;
        UsuarioID = usuarioId;
        ItemID = itemId;
        TipoItem = tipoItem;
        FechaPrestamo = DateTime.Now;
        FechaDevolucionEsperada = DateTime.Now.AddDays(diasPrestamo);
        FechaDevolucionReal = null;
        Observaciones = "Esperando";

    }
    public string Estado
    {
        get
        {
            if (FechaDevolucionReal.HasValue)
            {
                return "Devuelto";
            }

            if (DateTime.Now > FechaDevolucionEsperada)
            {
                return "Vencido";
            }

            return "Activo";
        }
    }
    public bool EstaVencido => Estado == "Vencido";

    public int DiasRestantes => Estado == "Activo"
        ? (FechaDevolucionEsperada - DateTime.Now).Days
        : 0;



    private string? ValidarTexto(string? texto, string campo)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException($"El {campo} es obligatorio");
        if (texto.Trim().Length < 2)
            throw new ArgumentException($"El {campo} debe tener al menos 2 caracteres");
        return texto.Trim();
    }

    public override string ToString()
    {
        string devolucion = FechaDevolucionReal.HasValue
            ? FechaDevolucionReal.Value.ToString("dd/MM/yyyy")
            : "Pendiente";

        string alerta = EstaVencido ? " *** VENCIDO ***" : "";

        return $"Préstamo ID:{Id} | Usuario ID:{UsuarioID} | Item ID:{ItemID} ({TipoItem})\n" +
               $"  Prestado: {FechaPrestamo:dd/MM/yyyy} | Vence: {FechaDevolucionEsperada:dd/MM/yyyy}" +
               $" | Devuelto: {devolucion} | Estado: {Estado}{alerta}";
    }
}
