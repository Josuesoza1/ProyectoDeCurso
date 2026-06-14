
public interface ILoanRepository
{

    void Agregar(Loan loan);
    Loan Buscar(Func<Loan, bool> criterio);
    void Actualizar(Loan loan);
    void Eliminar(int id);
    List<Loan> ObtenerTodo();
    List<Loan> Filtrar(Func<Loan, bool> criterio);
    List<Loan> OrdenarTodo(Func<Loan, object>  criterio);
    int MostrarTotal();
}
