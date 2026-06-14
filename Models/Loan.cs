
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
    public string? TipoItem
    {
        get => _tipoItem;
        private set => _tipoItem = ValidarTexto(value, "tipoItem");
    }

    public string? Observaciones
    {
        get => _observaciones;
        private set => _observaciones = ValidarTexto(value, "observaciones");
    }

    public DateTime FechaPrestamo { get => _fechaPrestamo; private set => _fechaPrestamo = value; }
    public DateTime FechaDevolucionEsperada { get => _fechaDevolucionEsperada; private set => _fechaDevolucionEsperada = value; }
    public DateTime? FechaDevolucionReal { get => _fechaDevolucionReal; private set => _fechaDevolucionReal = value; }
    


    public Loan(int id, int usuarioId, int itemId, string tipoItem, int diasPrestamo = 14)
    {
        IdPrestamo = id;
        UsuarioID = usuarioId;
        ItemID = itemId;
        TipoItem = tipoItem;
        Observaciones = "Esperando";
        FechaPrestamo = DateTime.Now;
        FechaDevolucionEsperada = DateTime.Now.AddDays(diasPrestamo);
        FechaDevolucionReal = null;

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



    public void RegistrarDevolucion(DateTime fechaDevolucion, string nuevasObservaciones)
    {
        FechaDevolucionReal = fechaDevolucion;
        Observaciones = nuevasObservaciones;
    }

    public void ActualizarObservaciones(string nuevasObservaciones)
    {
        Observaciones = nuevasObservaciones;
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

        return $"Préstamo ID:{IdPrestamo} | Usuario ID:{UsuarioID} | Item ID:{ItemID} ({TipoItem})\n" +
               $"  Prestado: {FechaPrestamo:dd/MM/yyyy} | Vence: {FechaDevolucionEsperada:dd/MM/yyyy}" +
               $" | Devuelto: {devolucion} | Estado: {Estado}{alerta}";
    }
}
