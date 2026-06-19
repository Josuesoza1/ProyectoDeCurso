/// <summary>
/// Define el contrato para el repositorio de libros físicos.
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Crea un nuevo libro y lo agrega a la colección de libros. El ISBN debe ser único para cada libro.
    /// </summary>
    /// <param name="book">El objeto de tipo Book a guardar.</param>
    void Agregar(Book book);

    /// <summary>
    /// Busca un libro en la colección utilizando un criterio específico, como el ISBN.
    /// </summary>
    /// <param name="criterio">La condición lógica de búsqueda.</param>
    /// <returns>El libro encontrado o null.</returns>
    Book Buscar(Func<Book, bool> criterio);

    /// <summary>
    /// Actualiza la información de un libro existente en la colección de libros. El libro a actualizar se identifica por su ISBN.
    /// </summary>
    /// <param name="book">El libro modificado.</param>
    void Actualizar(Book book);

    /// <summary>
    /// Elimina un libro de la colección de libros utilizando su ISBN como criterio de búsqueda.
    /// </summary>
    /// <param name="ISBN">El código único del libro.</param>
    void Eliminar(string ISBN);

    /// <summary>
    /// Consulta y devuelve una lista de todos los libros en la colección de libros.
    /// </summary>
    /// <returns>Lista de libros físicos.</returns>
    List<Book> ObtenerTodo();

    /// <summary>
    /// Filtra los libros en la colección utilizando un criterio específico (ej. autor o género).
    /// </summary>
    /// <param name="criterio">Condición de filtrado.</param>
    /// <returns>Lista de libros que cumplen la condición.</returns>
    List<Book> Filtrar(Func<Book, bool> criterio);

    /// <summary>
    /// Ordena los libros en la colección utilizando un criterio específico (ej. Año de publicación).
    /// </summary>
    /// <param name="criterio">Propiedad de ordenamiento.</param>
    /// <returns>Lista de libros ordenada.</returns>
    List<Book> OrdenarTodo(Func<Book, object> criterio);

    /// <summary>
    /// Devuelve el número total de libros en la colección de libros.
    /// </summary>
    /// <returns>Cantidad entera de libros.</returns>
    int MostrarTotal();
}