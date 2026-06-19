/// <summary>
/// Clase abstracta base que define los atributos y comportamientos comunes de cualquier artículo en la biblioteca.
/// </summary>
public abstract class Catalog
{
    private int _id;
    private string? _titulo;
    private string? _autor;
    private string? _genero;
    private int _anio;
    private int _cantidad;
    private bool _disponible;

    /// <summary>
    /// Identificador único secuencial del artículo en el catálogo.
    /// </summary>
    public int ID
    {
        get => _id;
        set
        {
            if (value < 0)
                throw new ArgumentException("El id no puede ser negativo");
            _id = value;
        }
    }

    /// <summary>
    /// Título oficial de la obra.
    /// </summary>
    public string? Titulo
    {
        get => _titulo;
        protected set => _titulo = ValidarTexto(value, "titulo");
    }

    /// <summary>
    /// Nombre del autor o creador principal.
    /// </summary>
    public string? Autor
    {
        get => _autor;
        protected set => _autor = ValidarTexto(value, "autor");
    }

    /// <summary>
    /// Categoría literaria o temática principal del artículo.
    /// </summary>
    public string? Genero
    {
        get => _genero;
        protected set => _genero = ValidarTexto(value, "genero");
    }

    /// <summary>
    /// Año en el que la obra fue publicada originalmente.
    /// </summary>
    public int Anio
    {
        get => _anio;
        protected set
        {
            if (value < 0 || value > DateTime.Now.Year)
            {
                throw new ArgumentException("El año de publicación no puede ser negativo ni mayor al año actual.");
            }
            _anio = value;
        }
    }

    /// <summary>
    /// Cantidad de copias o licencias disponibles actualmente en stock para préstamos.
    /// </summary>
    public int Cantidad
    {
        get => _cantidad;
        protected set
        {
            if (value < 0)
                throw new ArgumentException("La cantidad no puede ser negativa");
            _cantidad = value;
        }
    }

    /// <summary>
    /// Indicador lógico que determina si hay al menos una copia disponible para prestar.
    /// </summary>
    public bool Disponible { get => _disponible; private set => _disponible = value; }

    /// <summary>
    /// Valida que el texto ingresado no esté vacío ni sea excesivamente corto.
    /// </summary>
    protected string? ValidarTexto(string? texto, string campo)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException($"El {campo} es obligatorio");
        if (texto.Trim().Length < 2)
            throw new ArgumentException($"El {campo} debe tener al menos 2 caracteres");
        return texto.Trim();
    }

    /// <summary>
    /// Constructor protegido para inicializar los datos base del artículo.
    /// </summary>
    protected Catalog(int iD, string? titulo, string? autor, string? genero, int anio, int cantidad)
    {
        ID = iD;
        Titulo = titulo;
        Autor = autor;
        Genero = genero;
        Anio = anio;
        Cantidad = cantidad;
        Disponible = true;
    }

    public void ActualizarTitulo(string nuevoTitulo) => Titulo = nuevoTitulo;
    public void ActualizarAutor(string nuevoAutor) => Autor = nuevoAutor;
    public void ActualizarGenero(string nuevoGenero) => Genero = nuevoGenero;

    /// <summary>
    /// Disminuye el stock en una unidad cuando se realiza un préstamo.
    /// </summary>
    public void PrestarItem()
    {
        if (Cantidad <= 0)
            throw new InvalidOperationException("No hay copias disponibles para prestar.");
        Cantidad--;
        if (Cantidad == 0) Disponible = false;
    }

    /// <summary>
    /// Aumenta el stock en una unidad tras la entrega de un artículo prestado.
    /// </summary>
    public void DevolverItem()
    {
        Cantidad++;
        Disponible = true;
    }

    /// <summary>
    /// Modifica de forma administrativa la cantidad total de artículos existentes.
    /// </summary>
    public void ActualizarCantidad(int nuevaCantidad)
    {
        if (nuevaCantidad < 0)
            throw new ArgumentException("La cantidad no puede ser negativa.");
        Cantidad = nuevaCantidad;
        Disponible = Cantidad > 0;
    }

    /// <summary>
    /// Método abstracto que debe ser implementado por las clases hijas para definirse (Físico o Digital).
    /// </summary>
    public abstract string TipoItem();

    public virtual string ObtenerDescripcion()
    {
        string estado = Disponible ? "Disponible" : "Prestado";
        return $" {TipoItem()}|{ID,-15} | {Titulo,-25} | {Autor,-25} | {Genero,-25} | {Anio,-5} | {estado}";
    }

    public override string ToString() => ObtenerDescripcion();
}