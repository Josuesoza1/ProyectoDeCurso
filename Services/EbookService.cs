public class EbookService
{
    private readonly IEbookRepository _ebookRepository;

    public EbookService(IEbookRepository ebookRepository)
    {
        _ebookRepository = ebookRepository;
    }

    public void RegistrarEbook(string doi, int id, string titulo, string autor, string genero, int anio, int cantidad, string formato, double tamano, string urlDescarga, string idioma)
    {
        Ebook nuevoEbook = new Ebook(doi, id, titulo, autor, genero, anio, cantidad, formato, tamano, urlDescarga, idioma);
        _ebookRepository.Agregar(nuevoEbook);
    }

    public List<Ebook> ObtenerTodo()
    {
        return _ebookRepository.ObtenerTodo();
    }

    public void ActualizarEbook(string doi, int id, string titulo, string autor, string genero, int anio, int cantidad, string formato, double tamano, string urlDescarga, string idioma)
    {
        Ebook ebook = new Ebook(doi, id, titulo, autor, genero, anio, cantidad, formato, tamano, urlDescarga, idioma);
        _ebookRepository.Actualizar(ebook);
    }

    public void EliminarEbook(string doi)
    {
        // Usamos el DOI como identificador principal, equivalente al ISBN
        _ebookRepository.Eliminar(doi);
    }

    public Ebook Busqueda(string doi)
    {
        return _ebookRepository.Buscar(e => e.DOI == doi);
    }

    public int MostrarTotalDeEbooks()
    {
        return _ebookRepository.MostrarTotal();
    }

    public List<Ebook> Ordenar()
    {
        return _ebookRepository.OrdenarTodo(e => e.Autor);
    }

    public List<Ebook> Filtrar(string autorBuscado)
    {
        return _ebookRepository.Filtrar(e => e.Autor == autorBuscado);
    }
}