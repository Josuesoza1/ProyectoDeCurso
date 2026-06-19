/// <summary>
/// Define el contrato para el repositorio de usuarios, gestionando las operaciones de persistencia y consulta.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Agrega un nuevo usuario a la colección de persistencia.
    /// </summary>
    /// <param name="user">El objeto de tipo User a guardar.</param>
    void Agregar(User user);

    /// <summary>
    /// Busca un usuario en la colección utilizando una expresión lambda como criterio.
    /// </summary>
    /// <param name="criterio">La condición de búsqueda (ej. buscar por ID).</param>
    /// <returns>El primer usuario que cumpla la condición, o null si no se encuentra.</returns>
    User Buscar(Func<User, bool> criterio);

    /// <summary>
    /// Actualiza la información de un usuario existente en la colección.
    /// </summary>
    /// <param name="user">El objeto User con los datos modificados.</param>
    void Actualizar(User user);

    /// <summary>
    /// Elimina un usuario de la colección utilizando su ID de registro.
    /// </summary>
    /// <param name="id">El identificador único del usuario.</param>
    void Eliminar(int id);

    /// <summary>
    /// Consulta y devuelve una lista de todos los usuarios registrados.
    /// </summary>
    /// <returns>Lista completa de usuarios.</returns>
    List<User> ObtenerTodo();

    /// <summary>
    /// Filtra los usuarios en la colección utilizando un criterio específico.
    /// </summary>
    /// <param name="criterio">La condición de filtrado.</param>
    /// <returns>Lista de usuarios que cumplen con el criterio.</returns>
    List<User> Filtrar(Func<User, bool> criterio);

    /// <summary>
    /// Ordena los usuarios en la colección utilizando un criterio específico.
    /// </summary>
    /// <param name="criterio">La propiedad por la cual se va a ordenar (ej. Nombre).</param>
    /// <returns>Lista de usuarios ordenada.</returns>
    List<User> OrdenarTodo(Func<User, object> criterio);

    /// <summary>
    /// Devuelve el número total de usuarios en el sistema.
    /// </summary>
    /// <returns>Cantidad entera de usuarios registrados.</returns>
    int MostrarTotal();
}