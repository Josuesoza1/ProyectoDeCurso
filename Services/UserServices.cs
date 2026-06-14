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

    public void ActualizarUser(User usuarioActualizado)
    {
        _userRepository.Actualizar(usuarioActualizado);
    }
    public void EliminarUser(int id)
    {

        _userRepository.Eliminar(id);
    }



    public int MostrarTotalDeUsuarios()
    {
        return _userRepository.MostrarTotal();
    }

    // BÚSQUEDAS 
    public User BuscarPorId(int id) => _userRepository.Buscar(u => u.Id == id);

    public User BuscarPorCorreo(string correo)
        => _userRepository.Buscar(u => u.Correo != null && u.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase));

    // FILTROS 
    public List<User> FiltrarPorNombreOApellido(string texto)
        => _userRepository.Filtrar(u => u.NombreCompleto.Contains(texto, StringComparison.OrdinalIgnoreCase));

    // ORDENAMIENTOS 
    public List<User> OrdenarPorNombre() => _userRepository.OrdenarTodo(u => u.Nombre);

    public List<User> OrdenarPorId() => _userRepository.OrdenarTodo(u => u.Id);


}