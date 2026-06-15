public class LoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public void RegistrarLoan(int id, int usuarioId, int itemId, string tipoItem, int diasPrestamo = 14)
    {

        DateTime fechaInicio = DateTime.Now;
        DateTime fechaVencimiento = fechaInicio.AddDays(diasPrestamo);


        Loan nuevoPrestamo = new Loan(id, usuarioId, itemId, tipoItem, "Esperando", fechaInicio, fechaVencimiento, null);
        _loanRepository.Agregar(nuevoPrestamo);
    }

    public void ActualizarLoan(Loan prestamoActualizado)
    {
        _loanRepository.Actualizar(prestamoActualizado);
    }

    public List<Loan> ObtenerTodo()
    {
        return _loanRepository.ObtenerTodo();
    }

    public void EliminarLoan(int id)
    {
        _loanRepository.Eliminar(id);
    }


    public int MostrarTotalDePrestamos()
    {
        return _loanRepository.MostrarTotal();
    }

    public Loan BuscarPorId(int id) => _loanRepository.Buscar(p => p.IdPrestamo == id);


}