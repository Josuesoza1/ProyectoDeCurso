
public interface IPrestamoRepository
{
    void GuardarTodos(List<Prestamo> prestamos);
    List<Prestamo> ObtenerTodos();
}
