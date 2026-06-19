/// <summary>
/// Servicio de lógica de negocio para la gestión de libros físicos.
/// </summary>
public class BookService
{
    private readonly IBookRepository _bookRepository;

    /// <summary>
    /// Inicializa el servicio inyectando el repositorio de libros.
    /// </summary>
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <summary>
    /// Registra un nuevo libro físico autoincrementando su ID de forma segura.
    /// </summary>
    public void RegistrarBook(string titulo, string autor, string genero, int anio, int cantidad, string isbn, int numeroDePaginas, string editorial, string estadoFisico = "Bueno")
    {
        var librosExistentes = _bookRepository.ObtenerTodo();
        int id = librosExistentes.Count > 0 ? librosExistentes.Max(b => b.ID) + 1 : 1;

        Book nuevoLibro = new Book(id, isbn, titulo, autor, genero, anio, cantidad, numeroDePaginas, editorial, estadoFisico);
        _bookRepository.Agregar(nuevoLibro);
    }

    /// <summary>
    /// Obtiene la lista completa de libros registrados.
    /// </summary>
    public List<Book> ObtenerTodo()
    {
        return _bookRepository.ObtenerTodo();
    }

    /// <summary>
    /// Actualiza los datos de un libro existente.
    /// </summary>
    public void ActualizarBook(Book libroActualizado)
    {
        _bookRepository.Actualizar(libroActualizado);
    }

    /// <summary>
    /// Elimina un libro del catálogo basándose en su ISBN.
    /// </summary>
    public void EliminarBook(string iSBN)
    {
        _bookRepository.Eliminar(iSBN);
    }

    /// <summary>
    /// Muestra el conteo total de libros físicos en el catálogo.
    /// </summary>
    public int MostrarTotalDeLibros()
    {
        return _bookRepository.MostrarTotal();
    }

    /// <summary>
    /// Busca un libro coincidente mediante su ISBN único.
    /// </summary>
    public Book BuscarPorISBN(string isbn)
    {
        return _bookRepository.Buscar(b => b.ISBN == isbn);
    }

    /// <summary>
    /// Busca un libro coincidente mediante su ID interno.
    /// </summary>
    public Book BuscarPorId(int id)
    {
        return _bookRepository.Buscar(b => b.ID == id);
    }
}