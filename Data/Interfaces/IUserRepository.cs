
public interface IUserRepository
{

    void Agregar(User user);
    User BuscarPorId(string id);
    void Actualizar(User user);
    void Eliminar(string id);
    List<User> ObtenerTodo();
    List<User> Filtrar(decimal valor, int opcionFiltro);
    List<User> OrdenarTodo();
    int MostrarTotal();
}
