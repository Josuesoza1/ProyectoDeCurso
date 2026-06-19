/// <summary>
/// Define el contrato para el repositorio de transacciones de préstamos.
/// </summary>
public interface ILoanRepository
{
    /// <summary>
    /// Agrega un nuevo préstamo a la colección de préstamos activos e históricos.
    /// </summary>
    /// <param name="loan">El objeto préstamo a registrar.</param>
    void Agregar(Loan loan);

    /// <summary>
    /// Busca un préstamo que cumpla los criterios especificados (ej. ID de préstamo).
    /// </summary>
    /// <param name="criterio">La condición lógica de búsqueda.</param>
    /// <returns>El registro del préstamo o null.</returns>
    Loan Buscar(Func<Loan, bool> criterio);

    /// <summary>
    /// Actualiza la información de un préstamo existente (ej. registrar devolución o notas).
    /// </summary>
    /// <param name="loan">El préstamo con sus estados modificados.</param>
    void Actualizar(Loan loan);

    /// <summary>
    /// Elimina el registro de un préstamo de la base de datos, mediante su ID.
    /// </summary>
    /// <param name="id">El identificador único del préstamo.</param> 
    void Eliminar(int id);

    /// <summary>
    /// Crea una lista de todos los préstamos registrados en el sistema.
    /// </summary>
    /// <returns>Lista completa de transacciones.</returns>
    List<Loan> ObtenerTodo();

    /// <summary>
    /// Filtra mediante criterios para encontrar conjuntos específicos (ej. préstamos morosos o activos).
    /// </summary>
    /// <param name="criterio">Condición de filtrado.</param>
    /// <returns>Lista de préstamos que coinciden.</returns>
    List<Loan> Filtrar(Func<Loan, bool> criterio);

    /// <summary>
    /// Ordena los registros de préstamo basándose en un parámetro (ej. fecha de vencimiento).
    /// </summary>
    /// <param name="criterio">Propiedad de ordenamiento.</param>
    /// <returns>Lista de préstamos ordenada.</returns>
    List<Loan> OrdenarTodo(Func<Loan, object> criterio);

    /// <summary>
    /// Devuelve el conteo de todos los préstamos registrados.
    /// </summary>
    /// <returns>Número total de transacciones.</returns>
    int MostrarTotal();
}