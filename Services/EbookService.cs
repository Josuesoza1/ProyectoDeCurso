public class EbookService
{
    private readonly IEbookRepository _ebookRepository;

    public EbookService(IEbookRepository ebookRepository)
    {
        _ebookRepository = ebookRepository;
    }

    public void RegistrarEbook(string doi, string titulo, string autor, string genero, int anio, int cantidad, string formato, double tamano, string urlDescarga, string idioma)
    {
        var ebooksExistentes = _ebookRepository.ObtenerTodo();
        int id = ebooksExistentes.Count > 0 ? ebooksExistentes.Max(e => e.ID) + 1 : 1;

        Ebook nuevoEbook = new Ebook(id, doi, titulo, autor, genero, anio, cantidad, formato, tamano, urlDescarga, idioma);
        _ebookRepository.Agregar(nuevoEbook);
    }

    public List<Ebook> ObtenerTodo()
    {
        return _ebookRepository.ObtenerTodo();
    }

    public void ActualizarEbook(Ebook ebookActualizado)
    {
        _ebookRepository.Actualizar(ebookActualizado);
    }

    public void EliminarEbook(string doi)
    {

        _ebookRepository.Eliminar(doi);
    }



    public int MostrarTotalDeEbooks()
    {
        return _ebookRepository.MostrarTotal();
    }

    //TIPOS DE BÚSQUEDA 

    public Ebook Busqueda(string doi)
    {
        return _ebookRepository.Buscar(e => e.DOI == doi);
    }


    public Ebook BuscarPorId(int id)
    {
        return _ebookRepository.Buscar(e => e.ID == id);
    }

}