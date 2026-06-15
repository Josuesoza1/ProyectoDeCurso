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
    public void ActualizarUser(User usuarioActualizado) => _userRepository.Actualizar(usuarioActualizado);

    public void EliminarUser(int id)
    {

        _userRepository.Eliminar(id);
    }



    public int MostrarTotalDeUsuarios()
    {
        return _userRepository.MostrarTotal();
    }
    public User BuscarPorId(int id) => _userRepository.Buscar(u => u.Id == id);

}