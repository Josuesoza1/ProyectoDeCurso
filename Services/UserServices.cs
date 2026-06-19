/// <summary>
/// Servicio de lógica de negocio para la administración de lectores y usuarios del sistema.
/// </summary>
public class UserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicializa el servicio inyectando el repositorio de usuarios.
    /// </summary>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Registra un nuevo usuario calculando automáticamente su ID secuencial.
    /// </summary>
    public void RegistrarUser(string nombre, string apellido, string correo, string telefono)
    {
        var usuarioExistente = _userRepository.ObtenerTodo();
        int id = usuarioExistente.Count > 0 ? usuarioExistente.Max(u => u.Id) + 1 : 1;

        User nuevoUsuario = new User(id, nombre, apellido, correo, telefono);
        _userRepository.Agregar(nuevoUsuario);
    }

    /// <summary>
    /// Retorna el listado completo de usuarios registrados.
    /// </summary>
    public List<User> ObtenerTodo()
    {
        return _userRepository.ObtenerTodo();
    }

    /// <summary>
    /// Actualiza la información del perfil de un usuario.
    /// </summary>
    public void ActualizarUser(User usuarioActualizado) => _userRepository.Actualizar(usuarioActualizado);

    /// <summary>
    /// Da de baja o elimina un perfil de usuario utilizando su ID.
    /// </summary>
    public void EliminarUser(int id)
    {
        _userRepository.Eliminar(id);
    }

    /// <summary>
    /// Retorna el conteo total de usuarios inscritos en la base de datos.
    /// </summary>
    public int MostrarTotalDeUsuarios()
    {
        return _userRepository.MostrarTotal();
    }

    /// <summary>
    /// Busca un perfil de usuario coincidente utilizando su ID.
    /// </summary>
    public User BuscarPorId(int id) => _userRepository.Buscar(u => u.Id == id);

    /// <summary>
    /// Filtra el conjunto de usuarios aplicando criterios dinámicos (delegados Func).
    /// </summary>
    public List<User> FiltrarUsuarios(Func<User, bool> criterio)
    {
        return _userRepository.Filtrar(criterio);
    }

    /// <summary>
    /// Ordena el conjunto de lectores basándose en una propiedad específica.
    /// </summary>
    public List<User> OrdenarUsuarios(Func<User, object> criterio)
    {
        return _userRepository.OrdenarTodo(criterio);
    }
}