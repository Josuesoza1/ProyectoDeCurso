public class Book : Catalog
{
    private string? _iSBN;
    private int _numeroDePaginas;
    private string? _editorial;
    private string? _estadoFisico;

    public string? ISBN
    {
        get => _iSBN;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El ISBN no puede estar vacío");
            }
            if (value.Length != 13)
            {
                throw new ArgumentException("El ISBN Debe contener exactamente 13 digitos");
            }
            _iSBN = value;
        }
    }
    public int NumeroDePaginas
    {
        get => _numeroDePaginas;
        private set
        {
            if (value < 0)
                throw new ArgumentException("La cantidad de paginas no puede ser negativa");
            _numeroDePaginas = value;
        }
    }
    public string? Editorial
    {
        get => _editorial;
        private set => _editorial = ValidarTexto(value, "editorial");
    }
    public string? EstadoFisico
    {
        get => _estadoFisico;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El estado no puede estar vacío");
            if (value != "Bueno" && value != "Regular" && value != "Deteriorado")
                throw new ArgumentException("El estado solo puede ser: 'Bueno' , 'Regular' o 'Deteriorado'.");
            _estadoFisico = value;
        }
    }
    public Book(int iD, string isbn, string? titulo, string? autor, string? genero, int anio, int cantidad, int numeroDePaginas, string? editorial, string estadoFisico = "Bueno") 
        : base(iD, titulo, autor, genero, anio, cantidad)
    {
        ISBN = isbn;
        NumeroDePaginas = numeroDePaginas;
        Editorial = editorial;
        EstadoFisico = estadoFisico;
    }


    public void ActualizarEditorial(string nuevoEditorial)
    {
        Editorial = nuevoEditorial;
    }


    public override string TipoItem() => "LIBRO";
    public override string ToString()
    {
        
        string estado = Cantidad > 0 ? $"Disponible ({Cantidad} copias en stock)" : "Agotado / Prestado";

        return $" [LIBRO] ID: {ID}\n" +
               $"   Título  : {Titulo}\n" +
               $"   Autor   : {Autor}\n" +
               $"   Detalles: {Genero} | Año: {Anio} | Ed: {Editorial}\n" +
               $"   Físico  : {NumeroDePaginas} págs. | Estado: {EstadoFisico} | ISBN: {ISBN}\n" +
               $"   Estatus : {estado}\n" +
               $"   {new string('-', 55)}";

        
    }
}

