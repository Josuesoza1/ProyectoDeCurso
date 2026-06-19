/// <summary>
/// Servicio de lógica de negocio para procesar el ciclo de vida de los préstamos.
/// </summary>
public class LoanService
{
    private readonly ILoanRepository _loanRepository;

    /// <summary>
    /// Inicializa el servicio inyectando el repositorio de préstamos.
    /// </summary>
    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    /// <summary>
    /// Registra una nueva transacción de préstamo calculando las fechas de inicio y vencimiento.
    /// </summary>
    public void RegistrarLoan(int usuarioId, int itemId, string tipoItem, int diasPrestamo = 14)
    {
        DateTime fechaInicio = DateTime.Today;
        DateTime fechaVencimiento = fechaInicio.AddDays(diasPrestamo);

        var prestamosExistentes = _loanRepository.ObtenerTodo();
        int id = prestamosExistentes.Count > 0 ? prestamosExistentes.Max(p => p.IdPrestamo) + 1 : 1;

        Loan nuevoPrestamo = new Loan(id, usuarioId, itemId, tipoItem, "Esperando", fechaInicio, fechaVencimiento, null);
        _loanRepository.Agregar(nuevoPrestamo);
    }

    /// <summary>
    /// Actualiza el estado transaccional de un préstamo (devoluciones, notas).
    /// </summary>
    public void ActualizarLoan(Loan prestamoActualizado)
    {
        _loanRepository.Actualizar(prestamoActualizado);
    }

    /// <summary>
    /// Retorna la lista histórica de todos los préstamos.
    /// </summary>
    public List<Loan> ObtenerTodo()
    {
        return _loanRepository.ObtenerTodo();
    }

    /// <summary>
    /// Elimina físicamente un registro de préstamo por su ID.
    /// </summary>
    public void EliminarLoan(int id)
    {
        _loanRepository.Eliminar(id);
    }

    /// <summary>
    /// Muestra el conteo histórico total de transacciones de préstamo.
    /// </summary>
    public int MostrarTotalDePrestamos()
    {
        return _loanRepository.MostrarTotal();
    }

    /// <summary>
    /// Busca un préstamo coincidente a través de su ID único.
    /// </summary>
    public Loan BuscarPorId(int id) => _loanRepository.Buscar(p => p.IdPrestamo == id);

    /// <summary>
    /// Filtra la lista de transacciones aplicando criterios dinámicos (delegados Func).
    /// </summary>
    public List<Loan> FiltrarPrestamos(Func<Loan, bool> criterio)
    {
        return _loanRepository.Filtrar(criterio);
    }

    /// <summary>
    /// Ordena la lista histórica de transacciones aplicando criterios dinámicos.
    /// </summary>
    public List<Loan> OrdenarPrestamos(Func<Loan, object> criterio)
    {
        return _loanRepository.OrdenarTodo(criterio);
    }
}