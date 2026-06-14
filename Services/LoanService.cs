public class LoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public void RegistrarLoan(int id, int usuarioId, int itemId, string tipoItem, int diasPrestamo = 14)
    {
        Loan nuevoPrestamo = new Loan(id, usuarioId, itemId, tipoItem, diasPrestamo);
        _loanRepository.Agregar(nuevoPrestamo);
    }

    public List<Loan> ObtenerTodo()
    {
        return _loanRepository.ObtenerTodo();
    }

    public void ActualizarLoan(Loan prestamoActualizado)
    {
        _loanRepository.Actualizar(prestamoActualizado);
    }

    public void EliminarLoan(int id)
    {
        _loanRepository.Eliminar(id);
    }

    
    public int MostrarTotalDePrestamos()
    {
        return _loanRepository.MostrarTotal();
    }

    // BÚSQUEDAS
    public Loan BuscarPorId(int id) => _loanRepository.Buscar(p => p.IdPrestamo == id);

    // FILTROS
    public List<Loan> FiltrarPorUsuario(int usuarioId) => _loanRepository.Filtrar(p => p.UsuarioID == usuarioId);

    public List<Loan> FiltrarPorItem(int itemId) => _loanRepository.Filtrar(p => p.ItemID == itemId);

    public List<Loan> FiltrarSoloVencidos() => _loanRepository.Filtrar(p => p.EstaVencido && p.Estado != "Devuelto");

    // ORDENAMIENTOS 
    public List<Loan> OrdenarPorFechaPrestamo() => _loanRepository.OrdenarTodo(p => p.FechaPrestamo);

    public List<Loan> OrdenarPorDevolucionEsperada() => _loanRepository.OrdenarTodo(p => p.FechaDevolucionEsperada);

}