public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void RegistrarUser(int id, string nombre, string apellido, string correo, string telefono)
    {
        User nuevoUsuario = new User(id, nombre, apellido, correo, telefono);
        _userRepository.Agregar(nuevoUsuario);
    }

    public List<User> ObtenerTodo()
    {
        return _userRepository.ObtenerTodo();
    }

    public void ActualizarUser(int id, string nombre, string apellido, string correo, string telefono)
    {
        // Instanciamos el usuario con los datos actualizados
        User user = new User(id, nombre, apellido, correo, telefono);
        _userRepository.Actualizar(user);
    }

    public void EliminarUser(int id)
    {
        // El ID del usuario es numérico (int), así que lo pasamos directamente
        _userRepository.Eliminar(id);
    }

    public User Busqueda(int id)
    {
        return _userRepository.Buscar(u => u.Id == id);
    }

    public int MostrarTotalDeUsuarios()
    {
        return _userRepository.MostrarTotal();
    }

    public List<User> Ordenar()
    {
        // Ordenamos alfabéticamente por el nombre del usuario
        return _userRepository.OrdenarTodo(u => u.Nombre);
    }

    public List<User> Filtrar(string nombreBuscado)
    {
        // Filtramos para encontrar a todos los usuarios que coincidan con ese nombre
        return _userRepository.Filtrar(u => u.Nombre == nombreBuscado);
    }
}