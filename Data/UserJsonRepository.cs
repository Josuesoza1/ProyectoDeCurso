using System.Text.Json;
public class UserJsonRepository : IUserRepository
{
    private readonly string _rutaArchivo;

    public UserJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
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

    public List<User> ObtenerTodos()
    {
        if (!File.Exists(_rutaArchivo)) return new List<User>();

        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }
}
