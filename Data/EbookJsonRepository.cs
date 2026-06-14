using System.Text.Json;

public class EbookJsonRepository : IEbookRepository
{
    private readonly string _rutaArchivo;

    public EbookJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;

        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    public void GuardarTodo(List<Ebook> ebook)
    {
        string json = JsonSerializer.Serialize(ebook,
            new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    public List<Ebook> LeerArchivo()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Ebook>();

        string json = File.ReadAllText(_rutaArchivo);

        return JsonSerializer.Deserialize<List<Ebook>>(json) ??
            new List<Ebook>();
    }
    public void Actualizar(Ebook ebookModificado)
    {
        List<Ebook> ebooks = LeerArchivo();

        
        int index = ebooks.FindIndex(e => e.DOI == ebookModificado.DOI);

        if (index == -1)
            throw new ArgumentException("Ebook no encontrado.");

        
        ebooks[index] = ebookModificado;

        GuardarTodo(ebooks);
    }
    public void Agregar(Ebook ebook)
    {
        List<Ebook> ebooks = LeerArchivo();

        if (ebooks.Any(e => e.DOI== ebook.DOI))
            throw new InvalidOperationException("El ebook ya existe");

        ebooks.Add(ebook);
        GuardarTodo(ebooks);
    }

    public Ebook Buscar(Func<Ebook, bool> criterio)
    {
        List<Ebook> ebooks = LeerArchivo();
        return ebooks.FirstOrDefault(criterio);
    }

    public void Eliminar(string dOI)
    {
        List<Ebook> ebooks = LeerArchivo();

        Ebook ebook = ebooks.FirstOrDefault(e => e.DOI == dOI) ??
            throw new InvalidOperationException("El codigo del producto no existe");
        ebooks.Remove(ebook);

        GuardarTodo(ebooks);
    }

    public List<Ebook> Filtrar(Func<Ebook, bool> criterio)
    {
        List<Ebook> ebooks = LeerArchivo();
        return ebooks.Where(criterio).ToList();
    }

    public int MostrarTotal()
    {
        List<Ebook> ebooks = LeerArchivo();
        return ebooks.Count();
    }

    public List<Ebook> ObtenerTodo()
    {
        return LeerArchivo();
    }

    public List<Ebook> OrdenarTodo(Func<Ebook, object> criterio)
    {
        List<Ebook> ebooks = LeerArchivo();
        return ebooks.OrderBy(criterio).ToList();
    }
}