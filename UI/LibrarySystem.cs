
public class LibrarySystem
{
    private readonly UsuarioService _usuarioService;
    private readonly CatalogoService _catalogoService;
    private readonly PrestamoService _prestamoService;

    public LibrarySystem()
    {
        string directorioData = "data";
        if (!Directory.Exists(directorioData)) Directory.CreateDirectory(directorioData);

        IUserRepository repoUsuarios = new UserJsonRepository(Path.Combine(directorioData, "usuarios.json"));


        _usuarioService = new UsuarioService(repoUsuarios);


    }

    public void Iniciar()
    {
        Console.WriteLine("Bienvenido al sistema.");


        User nuevo = new User(0, "Juan", "Pérez", "juan@correo.com", "72345678");

        _usuarioService.RegistrarUsuario(nuevo);
    }
}
