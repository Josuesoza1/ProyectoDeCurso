
public class UsuarioService
{
    private readonly IUsuarioRepository _repositorio;
    private List<Usuario> _usuarios;

    public UsuarioService(IUsuarioRepository repositorio)
    {
        _repositorio = repositorio;
        _usuarios = _repositorio.ObtenerTodos();
    }


    public void RegistrarUsuario(Usuario nuevoUsuario)
    {
        nuevoUsuario.Id = _usuarios.Count == 0 ? 1 : _usuarios.Max(u => u.Id) + 1;
        _usuarios.Add(nuevoUsuario);
        _repositorio.GuardarTodos(_usuarios);
        Console.WriteLine($"\n Usuario '{nuevoUsuario.Nombre}' registrado con éxito.");
    }

    // Métodos adicionales útiles
    public List<Usuario> ObtenerTodos()
    {
        return _usuarios;
    }
}
