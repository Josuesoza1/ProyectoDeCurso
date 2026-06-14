public interface IBookRepository
{

    void Agregar(Book book);
    Book BuscarPorISBN(string ISBN);
    void Actualizar(Book book);
    void Eliminar(string ISBN);
    List<Book> ObtenerTodo();
    List<Book> Filtrar(decimal valor, int opcionFiltro);
    List<Book> OrdenarTodo();
    int MostrarTotal();
}

