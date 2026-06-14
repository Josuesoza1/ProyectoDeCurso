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

    // TIPOS DE FILTROS

    public List<Ebook> FiltrarPorAutor(string autorBuscado)
    {
        return _ebookRepository.Filtrar(e => e.Autor != null && e.Autor.Contains(autorBuscado, StringComparison.OrdinalIgnoreCase));
    }

    public List<Ebook> FiltrarPorTitulo(string tituloBuscado)
    {
        return _ebookRepository.Filtrar(e => e.Titulo != null && e.Titulo.Contains(tituloBuscado, StringComparison.OrdinalIgnoreCase));
    }

    public List<Ebook> FiltrarPorFormato(string formatoBuscado)
    {
        return _ebookRepository.Filtrar(e => e.Formato != null && e.Formato.Equals(formatoBuscado, StringComparison.OrdinalIgnoreCase));
    }

    // TIPOS DE ORDENAMIENTO

    public List<Ebook> OrdenarPorTitulo()
    {
        return _ebookRepository.OrdenarTodo(e => e.Titulo ?? string.Empty);
    }

    public List<Ebook> OrdenarPorAutor()
    {
        return _ebookRepository.OrdenarTodo(e => e.Autor ?? string.Empty);
    }

    public List<Ebook> OrdenarPorAnio()
    {
        return _ebookRepository.OrdenarTodo(e => e.Anio);
    }



}