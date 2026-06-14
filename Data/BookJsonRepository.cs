

using System.Text.Json;

public class BookJsonRepository : IBookRepository
{
    private readonly string _rutaArchivo;


    public BookJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;

        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    public void GuardarTodo(List<Book> book)
    {
        string json = JsonSerializer.Serialize(book,
            new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    public List<Book> LeerArchivo()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Book>();

        string json = File.ReadAllText(_rutaArchivo);

        return JsonSerializer.Deserialize<List<Book>>(json) ??
            new List<Book>();
    }


    public void Actualizar(Book book)
    {
        List<Book> books = LeerArchivo();
        Book nuevosValores = books.FirstOrDefault(e => e.ISBN == book.ISBN) ??
            throw new ArgumentException("Libro No Encontrado.");

        nuevosValores.ActualizarTitulo(book.Titulo);
        nuevosValores.ActualizarAutor(book.Autor);
        nuevosValores.ActualizarGenero(book.Genero);
        nuevosValores.ActualizarEditorial(book.Editorial);

        GuardarTodo(books);
    }

    public void Agregar(Book book)
    {

        List<Book> books = LeerArchivo();

        if (books.Any(e => e.ISBN == book.ISBN))
            throw new InvalidOperationException("El libro ya existe");

        books.Add(book);
        GuardarTodo(books);
    }

    public Book Buscar(Func<Book, bool> criterio)
    {
        List<Book> books = LeerArchivo();
        return books.FirstOrDefault(criterio);
    }

    public void Eliminar(string ISBN)
    {


        List<Book> books = LeerArchivo();

        Book book = books.FirstOrDefault(e => e.ISBN == ISBN) ??
        throw new InvalidOperationException("El codigo del producto no existe");
        books.Remove(book);

        GuardarTodo(books);

    }

    public List<Book> Filtrar(Func<Book, bool> criterio)
    {
        List<Book> books = LeerArchivo();
        return books.Where(criterio).ToList();
    }

    public int MostrarTotal()
    {
        List<Book> books = LeerArchivo();
        return books.Count();
    }

    public List<Book> ObtenerTodo()
    {
      return LeerArchivo();
    }

    public List<Book> OrdenarTodo(Func<Book, object> criterio)
    {
        List<Book> books = LeerArchivo();
        return books.OrderBy(criterio).ToList();
    }
}

