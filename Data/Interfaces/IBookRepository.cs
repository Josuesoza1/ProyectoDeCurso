public interface IBookRepository
{

    void Agregar(Book book);
    Book Buscar(Func<Book, bool> criterio);
    void Actualizar(Book book);
    void Eliminar(string ISBN);
    List<Book> ObtenerTodo();
    List<Book> Filtrar(Func<Book, bool> criterio);
    List<Book> OrdenarTodo(Func<Book, Object> criterio);
    int MostrarTotal();
}

