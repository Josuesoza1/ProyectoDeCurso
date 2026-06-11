
public class CatalogoService
{
    private readonly ICatalogoRepository _repository;
    private List<Catalogo> _catalogo;
    public CatalogoService(ICatalogoRepository repository)
    {
        _repository = repository;
        _catalogo = _repository.ObtenerTodos();
    }


// ==================== LIBROS ====================

public void AgregarLibro(Libro libro)
{
    libro.ID = GenerarIdLibro();
    _catalogo.Add(libro);
    _repository.GuardarTodos(_catalogo);
    Console.WriteLine($"\n  ✔ Libro '{libro.Titulo}' registrado con ID {libro.ID}.");
}

public List<Libro> ObtenerLibros() => _catalogo.OfType<Libro>().ToList();
public Libro BuscarLibroPorId(int id)
    => _catalogo.OfType<Libro>().FirstOrDefault(l => l.ID == id);

public List<Libro> BuscarLibrosPorTitulo(string titulo)
    => _catalogo.OfType<Libro>().Where(l => l.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase)).ToList();

public List<Libro> BuscarLibrosPorAutor(string autor)
    => _catalogo.OfType<Libro>().Where(l => l.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase)).ToList();

public bool ModificarLibro(int id, string titulo, string autor, int anio,
                           string genero, string editorial, string estadoFisico)
{
    var libro = BuscarLibroPorId(id);
    if (libro == null) return false;

    libro.Titulo = titulo;
    libro.Autor = autor;
    libro.Anio = anio;
    libro.Genero = genero;
    libro.Editorial = editorial;
    libro.EstadoFisico = estadoFisico;
    _repository.GuardarTodos(_catalogo);
    return true;
}

public bool EliminarLibro(int id)
{
    var libro = BuscarLibroPorId(id);
    if (libro == null || !libro.Disponible) return false;
    _catalogo.Remove(libro);
    _repository.GuardarTodos(_catalogo);
    return true;
}

// ==================== EBOOKS ====================

public void AgregarEBook(LibroElectronico libroElecronico)
{
    libroElecronico.ID = GenerarIdEBook();
    _catalogo.Add(libroElecronico);
    _repository.GuardarTodos(_catalogo);
    Console.WriteLine($"\n  ✔ EBook '{libroElecronico.Titulo}' registrado con ID {libroElecronico.ID}.");
}

public List<LibroElectronico> ObtenerEBooks() => _catalogo.OfType<LibroElectronico>().ToList();
public LibroElectronico BuscarEBookPorId(int id)
    => _catalogo.OfType<LibroElectronico>().FirstOrDefault(e => e.ID == id);

public List<LibroElectronico> BuscarEBooksPorTitulo(string titulo)
    => _catalogo.OfType<LibroElectronico>().Where(e => e.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase)).ToList();

public List<LibroElectronico> BuscarEBooksPorAutor(string autor)
    => _catalogo.OfType<LibroElectronico>().Where(e => e.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase)).ToList();

public bool ModificarEBook(int id, string titulo, string autor, int anio,
                           string genero, string formato, double tamano, string idioma)
{
    var ebook = BuscarEBookPorId(id);
    if (ebook == null) return false;

    ebook.Titulo = titulo;
    ebook.Autor = autor;
    ebook.Anio = anio;
    ebook.Genero = genero;
    ebook.Formato = formato;
    ebook.TamanoMB = tamano;
    ebook.Idioma = idioma;
    _repository.GuardarTodos(_catalogo);
    return true;
}

public bool EliminarEBook(int id)
{
    var ebook = BuscarEBookPorId(id);
    if (ebook == null || !ebook.Disponible) return false;
    _catalogo.Remove(ebook);
    _repository.GuardarTodos(_catalogo);
    return true;
}

// ==================== DISPONIBILIDAD ====================

public bool MarcarNoDisponible(int id, string tipo)
{
    if (tipo == "LIBRO")
    {
        var l = BuscarLibroPorId(id);
        if (l == null || !l.Disponible) return false;
        l.Disponible = false;
        _repository.GuardarTodos(_catalogo);
        return true;
    }
    else
    {
        var e = BuscarEBookPorId(id);
        if (e == null || !e.Disponible) return false;
        e.Disponible = false;
        _repository.GuardarTodos(_catalogo);
        return true;
    }
}

public void MarcarDisponible(int id, string tipo)
{
    if (tipo == "LIBRO")
    {
        var l = BuscarLibroPorId(id);
        if (l != null) { l.Disponible = true; _repository.GuardarTodos(_catalogo); }
    }
    else
    {
        var e = BuscarEBookPorId(id);
        if (e != null) { e.Disponible = true; _repository.GuardarTodos(_catalogo); }
    }
}

// ==================== BÚSQUEDA GENERAL ====================

public void MostrarTodoCatalogo()
{
    Console.WriteLine("\n  ═══ CATÁLOGO COMPLETO ═══");

    Console.WriteLine("\n  --- LIBROS FÍSICOS ---");
    if (libros.Count == 0)
        Console.WriteLine("  (No hay libros registrados)");
    else
        libros.ForEach(l => { Console.WriteLine("  " + l.ObtenerDescripcion()); Console.WriteLine(); });

    Console.WriteLine("  --- EBOOKS ---");
    if (_catalogo.OfType<LibroElectronico>().Count() == 0)
        Console.WriteLine("  (No hay eBooks registrados)");
    else
        _catalogo.OfType<LibroElectronico>().ToList().ForEach(e => { Console.WriteLine("  " + e.ObtenerDescripcion()); Console.WriteLine(); });
}

public void MostrarDisponibles()
{
    Console.WriteLine("\n  ═══ ÍTEMS DISPONIBLES ═══");

    var librosDisp = _catalogo.OfType<Libro>().Where(l => l.Disponible).ToList();
    var ebooksDisp = _catalogo.OfType<LibroElectronico>().Where(e => e.Disponible).ToList();

    Console.WriteLine($"\n  LIBROS ({librosDisp.Count}):");
    if (librosDisp.Count == 0) Console.WriteLine("  (Ninguno)");
    else librosDisp.ForEach(l => Console.WriteLine("  " + l));

    Console.WriteLine($"\n  EBOOKS ({ebooksDisp.Count}):");
    if (ebooksDisp.Count == 0) Console.WriteLine("  (Ninguno)");
    else ebooksDisp.ForEach(e => Console.WriteLine("  " + e));
}

// ==================== UTILIDADES ====================
private int GenerarIdLibro() => _catalogo.OfType<Libro>().Count() == 0 ? 1 : _catalogo.OfType<Libro>().Max(l => l.ID) + 1;
private int GenerarIdEBook() => _catalogo.OfType<LibroElectronico>().Count() == 0 ? 1 : _catalogo.OfType<LibroElectronico>().Max(e => e.ID) + 1;
}
