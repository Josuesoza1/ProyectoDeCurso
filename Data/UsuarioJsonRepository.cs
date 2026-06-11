using System.Text.Json;
public class UsuarioJsonRepository : IUsuarioRepository
{
    private readonly string _rutaArchivo;

    public UsuarioJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
    }

    public void GuardarTodos(List<Usuario> usuarios)
    {
        string json = JsonSerializer.Serialize(usuarios,
            new JsonSerializerOptions
            {
                WriteIndented = true 
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    public List<Usuario> ObtenerTodos()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Usuario>();

        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
    }
}
