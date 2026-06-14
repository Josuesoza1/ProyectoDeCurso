
public class UsuarioService
{
    private readonly IUserRepository _repositorio;
    private List<User> _usuarios;

    public UsuarioService(IUserRepository repositorio)
    {
        _repositorio = repositorio;
        _usuarios = _repositorio.ObtenerTodos();
    }


    public void RegistrarUsuario(User nuevoUsuario)
    {
        nuevoUsuario.Id = _usuarios.Count == 0 ? 1 : _usuarios.Max(u => u.Id) + 1;
        _usuarios.Add(nuevoUsuario);
        _repositorio.GuardarTodos(_usuarios);
        Console.WriteLine($"\n Usuario '{nuevoUsuario.Nombre}' registrado con éxito.");
    }

    // Métodos adicionales útiles
    public List<User> ObtenerTodos()
    {
        return _usuarios;
    }
}
