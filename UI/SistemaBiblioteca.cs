
public class SistemaBiblioteca
{
    private readonly UsuarioService _usuarioService;
    private readonly CatalogoService _catalogoService;
    private readonly PrestamoService _prestamoService;

    public SistemaBiblioteca()
    {
        string directorioData = "data";
        if (!Directory.Exists(directorioData)) Directory.CreateDirectory(directorioData);

        IUsuarioRepository repoUsuarios = new UsuarioJsonRepository(Path.Combine(directorioData, "usuarios.json"));


        _usuarioService = new UsuarioService(repoUsuarios);


    }

    public void Iniciar()
    {
        Console.WriteLine("Bienvenido al sistema.");


        Usuario nuevo = new Usuario(0, "Juan", "Pérez", "juan@correo.com", "72345678");

        _usuarioService.RegistrarUsuario(nuevo);
    }
}
