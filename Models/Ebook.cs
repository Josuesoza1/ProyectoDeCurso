public class Ebook : Catalog
{
    private string _dOI = string.Empty;
    private string _formato = string.Empty;
    private double _tamano;
    private string _urlDescarga = string.Empty;
    private string _idioma = string.Empty;

    public string DOI
    {
        get => _dOI;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El DOI no puede estar vacío.");
            }

            if (!value.StartsWith("10.") || !value.Contains("/"))
            {
                throw new ArgumentException("El DOI es inválido. Debe empezar con '10.' y tener una barra '/'.");
            }

            _dOI = value;
        }
    }


    public string Formato
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
                throw new ArgumentException("El tamaño debe estar entre 0 y 200MB");
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
            if (value.ToUpper() != "ES" && value.ToUpper() != "EN" && value.ToUpper() != "FR" && value.ToUpper() != "PT")
                throw new ArgumentException("El idioma no es válido. Usa ES, EN, FR o PT.");
            _idioma = value.ToUpper();
        }
    }


    public Ebook( int iD, string dOI, string? titulo, string? autor, string? genero,
        int anio, int cantidad, string formato, double tamano, 
        string urlDescarga, string idioma) :
        base(iD, titulo, autor, genero, anio, cantidad)
    {
        DOI = dOI;
        Formato = formato;
        Tamano = tamano;
        UrlDescarga = urlDescarga;
        Idioma = idioma;
    }


    public void ActualizarURL(string nuevaURL)
    {
        UrlDescarga = nuevaURL;
    }

    public void ActualizarFormato(string nuevoFormato)
    {
        Formato = nuevoFormato;
    }   

    public void ActualizarIdioma(string nuevoIdioma)
    {
        Idioma = nuevoIdioma;
    }

    

    public override string TipoItem() => "EBOOK";

    public override string ToString()
    {
        
        string estado = Cantidad > 0 ? $"Disponible ({Cantidad} licencias)" : "Agotado / Prestado";

        return $" [EBOOK] ID: {ID}\n" +
               $"   Título  : {Titulo}\n" +
               $"   Autor   : {Autor}\n" +
               $"   Detalles: {Genero} | Año: {Anio} | Idioma: {Idioma}\n" +
               $"   Archivo : {Formato} ({Tamano} MB) | Enlace: {UrlDescarga}\n" +
               $"   Estatus : {estado}\n" +
               $"   {new string('-', 55)}";
    }
}

