using System.Text.Json;

public class CatalogoJsonRepository : ICatalogoRepository
{
    private readonly string _rutaArchivo;

    public CatalogoJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
    }



    public void GuardarTodos(List<Catalogo> catalogo)
    {
        string json = JsonSerializer.Serialize(catalogo,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    public List<Catalogo> ObtenerTodos()
    {
        if (!File.Exists(_rutaArchivo))
            return new List<Catalogo>();

        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<Catalogo>>(json) ?? new List<Catalogo>();
    }
}


