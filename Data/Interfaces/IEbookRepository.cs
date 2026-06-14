public interface IEbookRepository
{

    void Agregar(Ebook ebook);
    Ebook BuscarPorCodigo(string codigo);
    void Actualizar(Ebook ebook);
    void Eliminar(string codigo);
    List<Ebook> ObtenerTodo();
    List<Ebook> Filtrar(decimal valor, int opcionFiltro);
    List<Ebook> OrdenarTodo();
    int MostrarTotal();
}
