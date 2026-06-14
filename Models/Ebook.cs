public class Ebook : Catalog
{
    private string? _formato;
    private double _tamano;
    private string? _urlDescarga;
    private string? _idioma;

    public string? Formato
    {
        get => _formato;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El formato no puede estar vacío");
            if (value.ToUpper() != "PDF" && value.ToUpper() != "EPUB" && value.ToUpper() != "MOBI")
                throw new ArgumentException("El formato debe ser PDF,EPUB,MOBI");
            _formato = value.ToUpper();
        }
    }
    public double Tamano
    {
        get => _tamano;
        private set
        {
            if (value < 0 || value > 200)
                throw new ArgumentException("El tamaño debe estar entre 0 y 200");
            _tamano = value;
        }
    }
    public string? UrlDescarga
    {
        get => _urlDescarga;
        private set
        {
            if (!value.ToLower().StartsWith("http://") && !value.ToLower().StartsWith("https://"))
                throw new ArgumentException("La URL debe empezar con http:// o https://");
            _urlDescarga = value;
        }
    }
    public string? Idioma
    {
        get => _idioma;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El idioma no puede estar vacío");
            if (value.ToUpper() != "Es" && value.ToUpper() != "EN" && value.ToUpper() != "FR" && value.ToUpper() != "PT")
                throw new ArgumentException("El idioma no es válido. Usa ES, EN, FR o PT.");
            _idioma = value.ToUpper();
        }
    }


    public Ebook(int iD, string? titulo, string? autor, string? genero, int anio, int cantidad, string formato, double tamano, string urlDescarga, string idioma) :
        base(iD, titulo, autor, genero, anio, cantidad)
    {
        Formato = formato;
        Tamano = tamano;
        UrlDescarga = urlDescarga;
        Idioma = idioma;
    }


    public override string TipoItem() => "EBOOK";

    public override string ToString()
    {
        string estado = Disponible ? "Disponible" : "Prestado";
        return $" {TipoItem()}|{ID,-15} | {Formato} | {Titulo,-15} | {Autor,-15} | {Genero,-15} | {Idioma}| {Anio,-5} | {UrlDescarga} | {Tamano} | {estado}";

    }
}

