/// <summary>
/// Define el contrato para el repositorio de libros electrónicos (Ebooks).
/// </summary>
public interface IEbookRepository
{
    /// <summary>
    /// Crea un nuevo libro electrónico y lo agrega al repositorio.
    /// </summary>
    /// <param name="ebook">El documento digital a guardar.</param>
    void Agregar(Ebook ebook);

    /// <summary>
    /// Busca un libro electrónico en el repositorio que cumpla con el criterio especificado.
    /// </summary>
    /// <param name="criterio">La condición lógica de búsqueda.</param>
    /// <returns>El Ebook encontrado o null.</returns>
    Ebook Buscar(Func<Ebook, bool> criterio);

    /// <summary>
    /// Actualiza la información de un libro electrónico existente en el repositorio.
    /// </summary>
    /// <param name="ebook">El objeto Ebook modificado.</param>
    void Actualizar(Ebook ebook);

    /// <summary>
    /// Elimina un libro electrónico del repositorio utilizando su DOI como criterio de búsqueda.
    /// </summary>
    /// <param name="dOI">El identificador digital del Ebook.</param>
    void Eliminar(string dOI);

    /// <summary>
    /// Crea una lista de todos los libros electrónicos almacenados en el repositorio.
    /// </summary>
    /// <returns>Lista de Ebooks.</returns>
    List<Ebook> ObtenerTodo();

    /// <summary>
    /// Filtra los libros electrónicos en el repositorio según un criterio específico, como el idioma o el formato.
    /// </summary>
    /// <param name="criterio">Condición de filtrado.</param>
    /// <returns>Lista de Ebooks filtrada.</returns>
    List<Ebook> Filtrar(Func<Ebook, bool> criterio);

    /// <summary>
    /// Ordena los libros electrónicos en el repositorio según un criterio específico, como el título o la fecha de publicación.
    /// </summary>
    /// <param name="criterio">Propiedad de ordenamiento.</param>
    /// <returns>Lista de Ebooks ordenada.</returns>
    List<Ebook> OrdenarTodo(Func<Ebook, object> criterio);

    /// <summary>
    /// Obtiene el número total de libros electrónicos almacenados en el repositorio.
    /// </summary>
    /// <returns>Cantidad total de licencias/ebooks.</returns>
    int MostrarTotal();
}