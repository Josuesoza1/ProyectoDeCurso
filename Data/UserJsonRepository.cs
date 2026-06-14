using System.Text.Json;

public class UserJsonRepository : IUserRepository
{
    private readonly string _rutaArchivo;

    public UserJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    public void GuardarTodos(List<User> usuarios)
    {
        string json = JsonSerializer.Serialize(usuarios,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    public List<User> LeerArchivo()
    {
        if (!File.Exists(_rutaArchivo)) return new List<User>();

        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    public void Actualizar(User user)
    {
        List<User> usuarios = LeerArchivo();
        User usuarioExistente = usuarios.FirstOrDefault(u => u.Id == user.Id) ??
            throw new ArgumentException("Usuario No Encontrado.");


        usuarioExistente.ActualizarNombre(user.Nombre);
        usuarioExistente.ActualizarApellido(user.Apellido);
        usuarioExistente.ActualizarCorreo(user.Correo);
        usuarioExistente.ActualizarTelefono(user.Telefono);

        GuardarTodos(usuarios);
    }

    public void Agregar(User user)
    {
        List<User> usuarios = LeerArchivo();

        if (usuarios.Any(u => u.Id == user.Id))
            throw new InvalidOperationException("El usuario ya existe con ese ID");

        usuarios.Add(user);
        GuardarTodos(usuarios);
    }

    public User Buscar(Func<User, bool> criterio)
    {
        List<User> usuarios = LeerArchivo();
        return usuarios.FirstOrDefault(criterio);
    }

    public void Eliminar(int id)
    {

        List<User> usuarios = LeerArchivo();
        User usuario = usuarios.FirstOrDefault(u => u.Id == id) ??
            throw new InvalidOperationException("El ID del usuario no existe");

        usuarios.Remove(usuario);
        GuardarTodos(usuarios);
    }

    public List<User> Filtrar(Func<User, bool> criterio)
    {
        List<User> usuarios = LeerArchivo();
        return usuarios.Where(criterio).ToList();
    }

    public int MostrarTotal()
    {
        List<User> usuarios = LeerArchivo();
        return usuarios.Count();
    }

    public List<User> ObtenerTodo()
    {
        return LeerArchivo();
    }

    public List<User> OrdenarTodo(Func<User, object> criterio)
    {
        List<User> usuarios = LeerArchivo();
        return usuarios.OrderBy(criterio).ToList();
    }
}