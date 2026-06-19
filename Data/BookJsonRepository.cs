using System.Text.Json;

/// <summary>
/// Repositorio encargado de persistir y administrar los libros físicos utilizando un archivo en formato JSON.
/// </summary>
public class BookJsonRepository : IBookRepository
{
    private readonly string _rutaArchivo;

    /// <summary>
    /// Inicializa el repositorio creando el archivo y su directorio contenedor si no existen previamente.
    /// </summary>
    public BookJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;

        string directorio = System.IO.Path.GetDirectoryName(_rutaArchivo);
        if (!string.IsNullOrEmpty(directorio) && !System.IO.Directory.Exists(directorio))
            System.IO.Directory.CreateDirectory(directorio);

        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    /// <summary>
    /// Serializa y guarda la lista completa de libros físicos en el archivo JSON de persistencia.
    /// </summary>
    public void GuardarTodo(List<Book> book)
    {
        string json = JsonSerializer.Serialize(book,
            new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    /// <summary>
    /// Lee, deserializa y devuelve la colección de libros físicos almacenados en el disco.
    /// </summary>
    public List<Book> LeerArchivo()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Book>();
        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
    }

    /// <summary>
    /// Actualiza la información de un libro físico específico dentro de la base de datos JSON.
    /// </summary>
    public void Actualizar(Book libroModificado)
    {
        List<Book> books = LeerArchivo();

        int index = books.FindIndex(b => b.ISBN == libroModificado.ISBN);

        if (index == -1)
            throw new ArgumentException("Libro no encontrado.");

        books[index] = libroModificado;
        GuardarTodo(books);
    }

    /// <summary>
    /// Agrega un nuevo libro físico al repositorio verificando previamente unicidad por ISBN.
    /// </summary>
    public void Agregar(Book book)
    {
        List<Book> books = LeerArchivo();

        if (books.Any(e => e.ISBN == book.ISBN))
            throw new InvalidOperationException("El libro ya existe");

        books.Add(book);
        GuardarTodo(books);
    }

    /// <summary>
    /// Busca y retorna el primer libro físico que cumpla con el criterio de búsqueda especificado.
    /// </summary>
    public Book Buscar(Func<Book, bool> criterio)
    {
        List<Book> books = LeerArchivo();
        return books.FirstOrDefault(criterio);
    }

    /// <summary>
    /// Elimina un registro de libro físico del almacenamiento basándose en su ISBN.
    /// </summary>
    public void Eliminar(string ISBN)
    {
        List<Book> books = LeerArchivo();

        Book book = books.FirstOrDefault(e => e.ISBN == ISBN) ??
        throw new InvalidOperationException("El codigo del producto no existe");
        books.Remove(book);

        GuardarTodo(books);
    }

    /// <summary>
    /// Filtra el conjunto de libros físicos aplicando un criterio dinámico.
    /// </summary>
    public List<Book> Filtrar(Func<Book, bool> criterio)
    {
        List<Book> books = LeerArchivo();
        return books.Where(criterio).ToList();
    }

    /// <summary>
    /// Retorna el conteo total de libros físicos registrados en el archivo.
    /// </summary>
    public int MostrarTotal()
    {
        List<Book> books = LeerArchivo();
        return books.Count();
    }

    /// <summary>
    /// Retorna la lista total de libros físicos.
    /// </summary>
    public List<Book> ObtenerTodo()
    {
        return LeerArchivo();
    }

    /// <summary>
    /// Ordena los libros físicos del repositorio basándose en un criterio de selección.
    /// </summary>
    public List<Book> OrdenarTodo(Func<Book, object> criterio)
    {
        List<Book> books = LeerArchivo();
        return books.OrderBy(criterio).ToList();
    }
}