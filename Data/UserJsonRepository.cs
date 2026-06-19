using System.Text.Json;

/// <summary>
/// Repositorio encargado de persistir y administrar los perfiles de usuarios/lectores utilizando un archivo JSON.
/// </summary>
public class UserJsonRepository : IUserRepository
{
    private readonly string _rutaArchivo;

    /// <summary>
    /// Inicializa el repositorio asegurando la existencia del archivo y su carpeta contenedora.
    /// </summary>
    public UserJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;

        string directorio = System.IO.Path.GetDirectoryName(_rutaArchivo);

        if (!string.IsNullOrEmpty(directorio) && !System.IO.Directory.Exists(directorio))
            System.IO.Directory.CreateDirectory(directorio);

        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    /// <summary>
    /// Serializa y guarda la lista completa de usuarios en el archivo JSON.
    /// </summary>
    public void GuardarTodos(List<User> usuarios)
    {
        string json = JsonSerializer.Serialize(usuarios,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    /// <summary>
    /// Lee, deserializa y retorna la colección de usuarios desde el disco.
    /// </summary>
    public List<User> LeerArchivo()
    {
        if (!File.Exists(_rutaArchivo)) return new List<User>();

        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    /// <summary>
    /// Actualiza la información de un registro de usuario existente.
    /// </summary>
    public void Actualizar(User usuarioModificado)
    {
        List<User> usuarios = LeerArchivo();

        int index = usuarios.FindIndex(u => u.Id == usuarioModificado.Id);

        if (index == -1)
            throw new ArgumentException("Usuario No Encontrado.");

        usuarios[index] = usuarioModificado;
        GuardarTodos(usuarios);
    }

    /// <summary>
    /// Agrega un nuevo usuario al repositorio previniendo duplicidad de Identificadores (ID).
    /// </summary>
    public void Agregar(User user)
    {
        List<User> usuarios = LeerArchivo();

        if (usuarios.Any(u => u.Id == user.Id))
            throw new InvalidOperationException("El usuario ya existe con ese ID");

        usuarios.Add(user);
        GuardarTodos(usuarios);
    }

    /// <summary>
    /// Busca y retorna el primer usuario que cumpla con la condición lógica proporcionada.
    /// </summary>
    public User Buscar(Func<User, bool> criterio)
    {
        List<User> usuarios = LeerArchivo();
        return usuarios.FirstOrDefault(criterio);
    }

    /// <summary>
    /// Elimina un perfil de usuario del almacenamiento local identificándolo por su ID.
    /// </summary>
    public void Eliminar(int id)
    {
        List<User> usuarios = LeerArchivo();
        User usuario = usuarios.FirstOrDefault(u => u.Id == id) ??
            throw new InvalidOperationException("El ID del usuario no existe");

        usuarios.Remove(usuario);
        GuardarTodos(usuarios);
    }

    /// <summary>
    /// Filtra la colección de usuarios aplicando un criterio o predicado dinámico.
    /// </summary>
    public List<User> Filtrar(Func<User, bool> criterio)
    {
        List<User> usuarios = LeerArchivo();
        return usuarios.Where(criterio).ToList();
    }

    /// <summary>
    /// Devuelve el conteo total de usuarios inscritos.
    /// </summary>
    public int MostrarTotal()
    {
        List<User> usuarios = LeerArchivo();
        return usuarios.Count();
    }

    /// <summary>
    /// Retorna la lista con todos los perfiles de usuarios.
    /// </summary>
    public List<User> ObtenerTodo()
    {
        return LeerArchivo();
    }

    /// <summary>
    /// Ordena la colección de usuarios basándose en un criterio selector de propiedades.
    /// </summary>
    public List<User> OrdenarTodo(Func<User, object> criterio)
    {
        List<User> usuarios = LeerArchivo();
        return usuarios.OrderBy(criterio).ToList();
    }
}