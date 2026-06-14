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

    //TIPOS DE FILTROS

    public List<Book> FiltrarPorAutor(string autorBuscado)
    {
        return _bookRepository.Filtrar(b => b.Autor != null && b.Autor.Contains(autorBuscado, StringComparison.OrdinalIgnoreCase));
    }

    public List<Book> FiltrarPorTitulo(string tituloBuscado)
    {
        return _bookRepository.Filtrar(b => b.Titulo != null && b.Titulo.Contains(tituloBuscado, StringComparison.OrdinalIgnoreCase));
    }

    public List<Book> FiltrarPorGenero(string generoBuscado)
    {
        return _bookRepository.Filtrar(b => b.Genero != null && b.Genero.Equals(generoBuscado, StringComparison.OrdinalIgnoreCase));
    }

    //TIPOS DE ORDENAMIENTO

    public List<Book> OrdenarPorAutor()
    {
        return _bookRepository.OrdenarTodo(b => b.Autor ?? string.Empty);
    }

    public List<Book> OrdenarPorTitulo()
    {
        return _bookRepository.OrdenarTodo(b => b.Titulo ?? string.Empty);
    }

    public List<Book> OrdenarPorAnio()
    {
        return _bookRepository.OrdenarTodo(b => b.Anio);
    }

}


