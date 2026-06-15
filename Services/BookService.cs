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

    
    public void ActualizarBook(Book libroActualizado)
    {
        _bookRepository.Actualizar(libroActualizado);
    }
    public void EliminarBook(string iSBN)
    {
        _bookRepository.Eliminar(iSBN);
    }

    public int MostrarTotalDeLibros()
    {
        return _bookRepository.MostrarTotal();
    }

    //TIPOS DE BÚSQUEDA 

    public Book BuscarPorISBN(string isbn)
    {
        return _bookRepository.Buscar(b => b.ISBN == isbn);
    }

    public Book BuscarPorId(int id)
    {
        return _bookRepository.Buscar(b => b.ID == id);
    }
}


