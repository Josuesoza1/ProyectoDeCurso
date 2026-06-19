/// <summary>
/// Entidad transaccional que rastrea el préstamo y estado de un artículo solicitado por un usuario.
/// </summary>
public class Loan
{
    private int _idPrestamo;
    private int _usuarioID;
    private int _itemID;
    private string? _tipoItem;
    private string? _observaciones;
    private DateTime _fechaPrestamo;
    private DateTime _fechaDevolucionEsperada;
    private DateTime? _fechaDevolucionReal;

    /// <summary>
    /// Clave primaria del registro transaccional.
    /// </summary>
    public int IdPrestamo
    {
        get => _idPrestamo;
        private set
        {
            if (value < 0)
                throw new ArgumentException("El id no puede ser negativo");
            _idPrestamo = value;
        }
    }

    /// <summary>
    /// ID del Lector responsable de la transacción.
    /// </summary>
    public int UsuarioID
    {
        get => _usuarioID;
        private set
        {
            if (value < 0)
                throw new ArgumentException("El id no puede ser negativo");
            _usuarioID = value;
        }
    }

    /// <summary>
    /// ID del artículo físico o digital del catálogo cedido temporalmente.
    /// </summary>
    public int ItemID
    {
        get => _itemID;
        private set
        {
            if (value < 0)
                throw new ArgumentException("El id no puede ser negativo");
            _itemID = value;
        }
    }

    /// <summary>
    /// Clasificación (Libro Físico o Ebook) para garantizar consistencia cruzada.
    /// </summary>
    public string? TipoItem
    {
        get => _tipoItem;
        private set => _tipoItem = ValidarTexto(value, "tipoItem");
    }

    /// <summary>
    /// Anotaciones misceláneas sobre el estado del artículo al entregar o devolver.
    /// </summary>
    public string? Observaciones
    {
        get => _observaciones;
        private set => _observaciones = ValidarTexto(value, "observaciones");
    }

    /// <summary>
    /// Marca de tiempo exacta del inicio de la transacción.
    /// </summary>
    public DateTime FechaPrestamo { get => _fechaPrestamo; private set => _fechaPrestamo = value; }

    /// <summary>
    /// Límite estipulado de tiempo (deadline) para que el lector regrese el artículo.
    /// </summary>
    public DateTime FechaDevolucionEsperada { get => _fechaDevolucionEsperada; private set => _fechaDevolucionEsperada = value; }

    /// <summary>
    /// Fecha efectiva de conclusión transaccional. Su estado nulo indica "En progreso".
    /// </summary>
    public DateTime? FechaDevolucionReal { get => _fechaDevolucionReal; private set => _fechaDevolucionReal = value; }

    /// <summary>
    /// Constructor formal para inicializar el historial o nuevo préstamo.
    /// </summary>
    public Loan(int idPrestamo, int usuarioID, int itemID, string? tipoItem, string? observaciones, DateTime fechaPrestamo, DateTime fechaDevolucionEsperada, DateTime? fechaDevolucionReal)
    {
        IdPrestamo = idPrestamo;
        UsuarioID = usuarioID;
        ItemID = itemID;
        TipoItem = tipoItem;
        Observaciones = observaciones;
        FechaPrestamo = fechaPrestamo;
        FechaDevolucionEsperada = fechaDevolucionEsperada;
        FechaDevolucionReal = fechaDevolucionReal;
    }

    /// <summary>
    /// Evalúa en tiempo real si el préstamo está Activo, Vencido o Devuelto.
    /// </summary>
    public string Estado
    {
        get
        {
            if (FechaDevolucionReal.HasValue) return "Devuelto";
            if (DateTime.Today > FechaDevolucionEsperada.Date) return "Vencido";
            return "Activo";
        }
    }

    /// <summary>
    /// Sella la transacción cerrando el préstamo con éxito.
    /// </summary>
    public void RegistrarDevolucion(DateTime fechaDevolucion, string nuevasObservaciones)
    {
        FechaDevolucionReal = fechaDevolucion;
        Observaciones = nuevasObservaciones;
    }

    public void ActualizarObservaciones(string nuevasObservaciones) => Observaciones = nuevasObservaciones;

    /// <summary>
    /// Bandera booleana de evaluación rápida contra morosidades.
    /// </summary>
    public bool EstaVencido => Estado == "Vencido";

    /// <summary>
    /// Cálculo de brecha temporal para la vigencia del contrato.
    /// </summary>
    public int DiasRestantes => Estado == "Activo" ? (FechaDevolucionEsperada - DateTime.Now).Days : 0;

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
        string devolucion = FechaDevolucionReal.HasValue ? $"¡Entregado el {FechaDevolucionReal.Value:dd/MM/yyyy}!" : "Pendiente (No devuelto aún)";
        string alerta = EstaVencido ? " ¡ATENCIÓN: VENCIDO!" : "";
        return $" PRÉSTAMO #{IdPrestamo}\n" +
               $"   Usuario ID: {UsuarioID,-5} | Ítem ID: {ItemID} ({TipoItem})\n" +
               $"   Fechas    : Prestado el {FechaPrestamo:dd/MM/yyyy} -> Vence el {FechaDevolucionEsperada:dd/MM/yyyy}\n" +
               $"   Devolución: {devolucion}\n" +
               $"   Estado    : {Estado.ToUpper()}{alerta}\n" +
               $"   Notas     : {Observaciones}\n" +
               $"   {new string('-', 55)}";
    }
}