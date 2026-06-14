
public interface ILoanRepository
{

    void Agregar(Loan loan);
    Loan BuscarPorId(string id);
    void Actualizar(Loan loan);
    void Eliminar(string id);
    List<Loan> ObtenerTodo();
    List<Loan> Filtrar(decimal valor, int opcionFiltro);
    List<Loan> OrdenarTodo();
    int MostrarTotal();
}
