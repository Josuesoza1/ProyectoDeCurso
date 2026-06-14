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

    public void ActualizarLoan(int id, int usuarioId, int itemId, string tipoItem, int diasPrestamo)
    {
        // Instanciamos el préstamo con el enfoque de la nueva instancia que solicitaste
        Loan loan = new Loan(id, usuarioId, itemId, tipoItem, diasPrestamo);
        _loanRepository.Actualizar(loan);
    }

    public void EliminarLoan(int id)
    {
        // El ID del préstamo es un int, lo mandamos directo sin conversiones
        _loanRepository.Eliminar(id);
    }

    public Loan Busqueda(int id)
    {
        return _loanRepository.Buscar(p => p.IdPrestamo == id);
    }

    public int MostrarTotalDePrestamos()
    {
        return _loanRepository.MostrarTotal();
    }

    public List<Loan> Ordenar()
    {
        // En préstamos, lo más útil suele ser ordenar cronológicamente por la fecha en que se hicieron
        return _loanRepository.OrdenarTodo(p => p.FechaPrestamo);
    }

    public List<Loan> Filtrar(int usuarioIdBuscado)
    {
        // Filtramos para obtener todos los préstamos que pertenecen a un usuario específico
        return _loanRepository.Filtrar(p => p.UsuarioID == usuarioIdBuscado);
    }
}