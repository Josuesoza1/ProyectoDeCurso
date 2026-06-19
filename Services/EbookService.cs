/// <summary>
/// Servicio de lógica de negocio para la gestión de libros electrónicos (Ebooks).
/// </summary>
public class EbookService
{
    private readonly IEbookRepository _ebookRepository;

    /// <summary>
    /// Inicializa el servicio inyectando el repositorio de Ebooks.
    /// </summary>
    public EbookService(IEbookRepository ebookRepository)
    {
        _ebookRepository = ebookRepository;
    }

    /// <summary>
    /// Registra un nuevo ebook autoincrementando su ID de forma segura.
    /// </summary>
    public void RegistrarEbook(string doi, string titulo, string autor, string genero, int anio, int cantidad, string formato, double tamano, string urlDescarga, string idioma)
    {
        var ebooksExistentes = _ebookRepository.ObtenerTodo();
        int id = ebooksExistentes.Count > 0 ? ebooksExistentes.Max(e => e.ID) + 1 : 1;

        Ebook nuevoEbook = new Ebook(id, doi, titulo, autor, genero, anio, cantidad, formato, tamano, urlDescarga, idioma);
        _ebookRepository.Agregar(nuevoEbook);
    }

    /// <summary>
    /// Obtiene la lista completa de ebooks registrados.
    /// </summary>
    public List<Ebook> ObtenerTodo()
    {
        return _ebookRepository.ObtenerTodo();
    }

    /// <summary>
    /// Actualiza la información de un ebook existente.
    /// </summary>
    public void ActualizarEbook(Ebook ebookActualizado)
    {
        _ebookRepository.Actualizar(ebookActualizado);
    }

    /// <summary>
    /// Elimina un ebook del sistema utilizando su código DOI.
    /// </summary>
    public void EliminarEbook(string doi)
    {
        _ebookRepository.Eliminar(doi);
    }

    /// <summary>
    /// Muestra el conteo total de licencias y ebooks registrados.
    /// </summary>
    public int MostrarTotalDeEbooks()
    {
        return _ebookRepository.MostrarTotal();
    }

    /// <summary>
    /// Busca un ebook en el sistema utilizando su DOI.
    /// </summary>
    public Ebook Busqueda(string doi)
    {
        return _ebookRepository.Buscar(e => e.DOI == doi);
    }

    /// <summary>
    /// Busca un ebook en el sistema utilizando su ID interno.
    /// </summary>
    public Ebook BuscarPorId(int id)
    {
        return _ebookRepository.Buscar(e => e.ID == id);
    }
}