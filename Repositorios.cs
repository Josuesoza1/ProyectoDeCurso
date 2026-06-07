public class Repositorios
{

    private readonly string _rutaArchivo;
    private readonly string _rutaArchivoUsuarios;
    private readonly string _rutaArchivoLibros;
    private readonly string _rutaArchivoEbook;
    private readonly string _rutaArchivoPrestamos;
    public Repositorios(string rutaArchivo, string rutaArchivoUsuarios, string rutaArchivoLibros, string rutaArchivoEbook, string rutaArchivoPrestamos)
    {
        _rutaArchivo = rutaArchivo;
        _rutaArchivoUsuarios = rutaArchivoUsuarios;
        _rutaArchivoLibros = rutaArchivoLibros;
        _rutaArchivoEbook = rutaArchivoEbook;
        _rutaArchivoPrestamos = rutaArchivoPrestamos;
    }
    public List<string> ObtenerDatos(string rutaArchivo)
    {
        List<string> Datos = new();
        if (!File.Exists(rutaArchivo))
            return Datos;
        using (StreamReader lector = File.OpenText(rutaArchivo))
        {
            string? line;
            while ((line = lector.ReadLine()) != null)
            {
                {
                    Datos.Add(line);
                }
            }
            return Datos;
        }
    }


    public void GuardarLibro(Libro libros)
    {
        using (StreamWriter escritor = File.AppendText(_rutaArchivoLibros))
        {
            escritor.WriteLine(libros.ToString());
        }
    }

    public void GuardarEbook(LibroElectronico ebook)
    {
        using (StreamWriter escritor = File.AppendText(_rutaArchivoEbook))
        {
            escritor.WriteLine(ebook.ToString());
        }
    }
    public void GuardarUsuario(Usuario usuario)
    {
        using (StreamWriter escritor = File.AppendText(_rutaArchivoUsuarios))
        {
            escritor.WriteLine(usuario.ToString());
        }
    }
    public void GuardarPrestamo(Prestamo prestamo)
    {
        using (StreamWriter escritor = File.AppendText(_rutaArchivoPrestamos))
        {
            escritor.WriteLine(prestamo.ToString());
        }
    }

}

