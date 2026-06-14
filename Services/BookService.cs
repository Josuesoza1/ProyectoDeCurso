public class BookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public void RegistrarBook(int id, string titulo, string autor, string genero, int anio, int cantidad, string isbn, int numeroDePaginas, string editorial, string estadoFisico = "Bueno")
    {
        Book nuevoLibro = new Book(id, titulo, autor, genero, anio, cantidad, isbn, numeroDePaginas, editorial, estadoFisico);
        _bookRepository.Agregar(nuevoLibro);
    }

    public List<Book> ObtenerTodo()
    {
        return _bookRepository.ObtenerTodo();
    }

    public void ActualizarBook(int id, string titulo, string autor, string genero, int anio, int cantidad, string isbn, int numeroDePaginas, string editorial, string estadoFisico)
    {
        Book book = new(id, titulo, autor, genero, anio, cantidad, isbn, numeroDePaginas, editorial, estadoFisico);
        _bookRepository.Actualizar(book);
    }

    public void EliminarBook(string iSBN)
    {
        _bookRepository.Eliminar(iSBN);
    }

    public Book Busqueda(string iSBN)
    {
        return _bookRepository.Buscar(e => e.ISBN == iSBN);
    }

    public int MostrarTotalDeLibros()
    {
        return _bookRepository.MostrarTotal();
    }

    public List<Book> Ordenar()
    {
        return _bookRepository.OrdenarTodo(e=> e.Autor);
    }

    public List<Book> Filtrar(string autorBuscado)
    {
        return _bookRepository.Filtrar(e => e.Autor == autorBuscado);
    }
}

