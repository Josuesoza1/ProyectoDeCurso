
class EbookJsonRepository : IEbookRepository
{
    private readonly string _rutaArchivo;

    public EbookJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    public void Actualizar(Ebook ebook)
    {
        throw new NotImplementedException();
    }

    public void Agregar(Ebook ebook)
    {
        throw new NotImplementedException();
    }

    public Ebook BuscarPorCodigo(string codigo)
    {
        throw new NotImplementedException();
    }

    public void Eliminar(string codigo)
    {
        throw new NotImplementedException();
    }

    public List<Ebook> Filtrar(decimal valor, int opcionFiltro)
    {
        throw new NotImplementedException();
    }

    public int MostrarTotal()
    {
        throw new NotImplementedException();
    }

    public List<Ebook> ObtenerTodo()
    {
        throw new NotImplementedException();
    }

    public List<Ebook> OrdenarTodo()
    {
        throw new NotImplementedException();
    }
}

