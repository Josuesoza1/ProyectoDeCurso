
public class BookJsonRepository : IBookRepository
{
    private readonly string _rutaArchivo;
    public BookJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;

        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    public void Actualizar(Book book)
    {
        throw new NotImplementedException();
    }

    public void Agregar(Book book)
    {
        throw new NotImplementedException();
    }

    public Book BuscarPorCodigo(string codigo)
    {
        throw new NotImplementedException();
    }

    public void Eliminar(string codigo)
    {
        throw new NotImplementedException();
    }

    public List<Book> Filtrar(decimal valor, int opcionFiltro)
    {
        throw new NotImplementedException();
    }

    public int MostrarTotal()
    {
        throw new NotImplementedException();
    }

    public List<Book> ObtenerTodo()
    {
        throw new NotImplementedException();
    }

    public List<Book> OrdenarTodo()
    {
        throw new NotImplementedException();
    }
}

