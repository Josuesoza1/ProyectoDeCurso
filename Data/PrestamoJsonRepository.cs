using System.Text.Json;
public class PrestamoJsonRepository : IPrestamoRepository
{
    private readonly string _rutaArchivo;

    public PrestamoJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
    }

    public void GuardarTodos(List<Prestamo> prestamos)
    {
        string json = JsonSerializer.Serialize(prestamos,
            new JsonSerializerOptions
            { 
                WriteIndented = true
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    public List<Prestamo> ObtenerTodos()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Prestamo>();

        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<Prestamo>>(json) ?? new List<Prestamo>();
    }
}
