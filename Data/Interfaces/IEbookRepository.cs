public interface IEbookRepository
{

    void Agregar(Ebook ebook);
    Ebook Buscar(Func<Ebook, bool> criterio);
    void Actualizar(Ebook ebook);
    void Eliminar(string dOI);
    List<Ebook> ObtenerTodo();
    List<Ebook> Filtrar(Func<Ebook, bool> criterio );
    List<Ebook> OrdenarTodo(Func<Ebook, object> criterio);
    int MostrarTotal();
}
