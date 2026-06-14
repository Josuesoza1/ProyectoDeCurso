public abstract class Catalog
{
    private int _id;
    private string? _titulo;
    private string? _autor;
    private string? _genero;
    private int _anio;
    private int _cantidad;
    private bool _disponible;

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

    public string? Titulo
    {
        get => _titulo;
        protected set => _titulo = ValidarTexto(value, "titulo");
    }
    public string? Autor
    {
        get => _autor;
        protected set => _autor = ValidarTexto(value, "autor");
    }
    public string? Genero
    {
        get => _genero;
        protected set => _genero = ValidarTexto(value, "genero");
    }

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
    public bool Disponible { get => _disponible; private set => _disponible = value; }

    protected string? ValidarTexto(string? texto, string campo)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException($"El {campo} es obligatorio");
        if (texto.Trim().Length < 2)
            throw new ArgumentException($"El {campo} debe tener al menos 2 caracteres");
        return texto.Trim();
    }



    protected Catalog(int iD, string? titulo, string? autor, string? genero, int anio, int cantidad)
    {
        ID = iD;
        Titulo = titulo;
        Autor = autor;
        Genero = genero;
        Anio = anio;
        Cantidad = cantidad;
        Disponible = false;
    }

    public void ActualizarTitulo(string nuevoTitulo)
    {
        Titulo = nuevoTitulo;
    }

    public void ActualizarAutor(string nuevoAutor)
    {
        Autor = nuevoAutor;
    }

    public void ActualizarGenero(string nuevoGenero)
    {
        Genero = nuevoGenero;
    }




    public abstract string TipoItem();

    public virtual string ObtenerDescripcion()
    {
        string estado = Disponible ? "Disponible" : "Prestado";
        return $" {TipoItem()}|{ID,-15} | {Titulo,-25} | {Autor,-25} | {Genero,-25} | {Anio,-5} | {estado}";
    }

    public override string ToString() => ObtenerDescripcion();



}

