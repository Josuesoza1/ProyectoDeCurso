using System.Text.Json;

/// <summary>
/// Repositorio encargado de persistir y administrar los libros electrónicos (Ebooks) utilizando un archivo en formato JSON.
/// </summary>
public class EbookJsonRepository : IEbookRepository
{
    private readonly string _rutaArchivo;

    /// <summary>
    /// Inicializa el repositorio creando el archivo y su directorio contenedor si no existen.
    /// </summary>
    public EbookJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;

        string directorio = System.IO.Path.GetDirectoryName(_rutaArchivo);
        if (!string.IsNullOrEmpty(directorio) && !System.IO.Directory.Exists(directorio))
            System.IO.Directory.CreateDirectory(directorio);

        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    /// <summary>
    /// Serializa y guarda la colección completa de Ebooks en el archivo JSON.
    /// </summary>
    public void GuardarTodo(List<Ebook> ebook)
    {
        string json = JsonSerializer.Serialize(ebook,
            new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    /// <summary>
    /// Lee, deserializa y devuelve la colección de Ebooks almacenados en el almacenamiento local.
    /// </summary>
    public List<Ebook> LeerArchivo()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Ebook>();
        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<Ebook>>(json) ?? new List<Ebook>();
    }

    /// <summary>
    /// Actualiza los datos de un Ebook persistido identificándolo por su DOI.
    /// </summary>
    public void Actualizar(Ebook ebookModificado)
    {
        List<Ebook> ebooks = LeerArchivo();

        int index = ebooks.FindIndex(e => e.DOI == ebookModificado.DOI);

        if (index == -1)
            throw new ArgumentException("Ebook no encontrado.");

        ebooks[index] = ebookModificado;
        GuardarTodo(ebooks);
    }

    /// <summary>
    /// Agrega un nuevo Ebook al repositorio validando unicidad a través del código DOI.
    /// </summary>
    public void Agregar(Ebook ebook)
    {
        List<Ebook> ebooks = LeerArchivo();

        if (ebooks.Any(e => e.DOI == ebook.DOI))
            throw new InvalidOperationException("El ebook ya existe");

        ebooks.Add(ebook);
        GuardarTodo(ebooks);
    }

    /// <summary>
    /// Busca y retorna el primer Ebook que satisfaga la condición lógica o criterio.
    /// </summary>
    public Ebook Buscar(Func<Ebook, bool> criterio)
    {
        List<Ebook> ebooks = LeerArchivo();
        return ebooks.FirstOrDefault(criterio);
    }

    /// <summary>
    /// Elimina un libro electrónico de la base de datos JSON basándose en el DOI.
    /// </summary>
    public void Eliminar(string dOI)
    {
        List<Ebook> ebooks = LeerArchivo();

        Ebook ebook = ebooks.FirstOrDefault(e => e.DOI == dOI) ??
            throw new InvalidOperationException("El codigo del producto no existe");
        ebooks.Remove(ebook);

        GuardarTodo(ebooks);
    }

    /// <summary>
    /// Filtra el conjunto de Ebooks aplicando un criterio de coincidencia.
    /// </summary>
    public List<Ebook> Filtrar(Func<Ebook, bool> criterio)
    {
        List<Ebook> ebooks = LeerArchivo();
        return ebooks.Where(criterio).ToList();
    }

    /// <summary>
    /// Devuelve el conteo total de Ebooks guardados en el archivo.
    /// </summary>
    public int MostrarTotal()
    {
        List<Ebook> ebooks = LeerArchivo();
        return ebooks.Count();
    }

    /// <summary>
    /// Retorna la lista total de libros electrónicos.
    /// </summary>
    public List<Ebook> ObtenerTodo()
    {
        return LeerArchivo();
    }

    /// <summary>
    /// Ordena los Ebooks de la colección basándose en un criterio específico.
    /// </summary>
    public List<Ebook> OrdenarTodo(Func<Ebook, object> criterio)
    {
        List<Ebook> ebooks = LeerArchivo();
        return ebooks.OrderBy(criterio).ToList();
    }
}