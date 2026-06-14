
public interface IUserRepository
{

    void Agregar(User user);
    User Buscar(Func<User,bool> criterio);
    void Actualizar(User user);
    void Eliminar(int id);
    List<User> ObtenerTodo();
    List<User> Filtrar(Func<User, bool> criterio);
    List<User> OrdenarTodo(Func<User, object> criterio);
    int MostrarTotal();
}
