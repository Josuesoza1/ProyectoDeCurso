using System.Text.Json;

/// <summary>
/// Repositorio encargado de la persistencia y control de las transacciones de préstamos utilizando archivos JSON.
/// </summary>
public class LoanJsonRepository : ILoanRepository
{
    private readonly string _rutaArchivo;

    /// <summary>
    /// Inicializa el repositorio y valida la existencia del archivo de persistencia de préstamos.
    /// </summary>
    public LoanJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;

        string directorio = System.IO.Path.GetDirectoryName(_rutaArchivo);

        if (!string.IsNullOrEmpty(directorio) && !System.IO.Directory.Exists(directorio))
            System.IO.Directory.CreateDirectory(directorio);

        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    /// <summary>
    /// Serializa y guarda la lista histórica de transacciones en el archivo JSON.
    /// </summary>
    public void GuardarTodos(List<Loan> prestamos)
    {
        string json = JsonSerializer.Serialize(prestamos,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    /// <summary>
    /// Lee, deserializa y retorna las transacciones de préstamo contenidas en el archivo local.
    /// </summary>
    public List<Loan> LeerArchivo()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Loan>();

        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<Loan>>(json) ?? new List<Loan>();
    }

    /// <summary>
    /// Actualiza de forma persistente un registro de préstamo modificado.
    /// </summary>
    public void Actualizar(Loan prestamoModificado)
    {
        List<Loan> prestamos = LeerArchivo();

        int index = prestamos.FindIndex(p => p.IdPrestamo == prestamoModificado.IdPrestamo);

        if (index == -1)
            throw new ArgumentException("Préstamo No Encontrado.");

        prestamos[index] = prestamoModificado;
        GuardarTodos(prestamos);
    }

    /// <summary>
    /// Registra un nuevo préstamo en la base de datos previniendo duplicidad de ID's.
    /// </summary>
    public void Agregar(Loan loan)
    {
        List<Loan> prestamos = LeerArchivo();

        if (prestamos.Any(p => p.IdPrestamo == loan.IdPrestamo))
            throw new InvalidOperationException("El préstamo ya existe con ese ID");

        prestamos.Add(loan);
        GuardarTodos(prestamos);
    }

    /// <summary>
    /// Busca y retorna el primer registro transaccional que cumpla con el criterio lógico.
    /// </summary>
    public Loan Buscar(Func<Loan, bool> criterio)
    {
        List<Loan> prestamos = LeerArchivo();
        return prestamos.FirstOrDefault(criterio);
    }

    /// <summary>
    /// Elimina físicamente un registro de préstamo utilizando su identificador (ID).
    /// </summary>
    public void Eliminar(int id)
    {
        List<Loan> prestamos = LeerArchivo();

        Loan prestamo = prestamos.FirstOrDefault(p => p.IdPrestamo == id) ??
            throw new InvalidOperationException("El ID del préstamo no existe");

        prestamos.Remove(prestamo);
        GuardarTodos(prestamos);
    }

    /// <summary>
    /// Filtra el historial de préstamos aplicando un predicado o criterio dinámico.
    /// </summary>
    public List<Loan> Filtrar(Func<Loan, bool> criterio)
    {
        List<Loan> prestamos = LeerArchivo();
        return prestamos.Where(criterio).ToList();
    }

    /// <summary>
    /// Devuelve el número total de registros de préstamo existentes.
    /// </summary>
    public int MostrarTotal()
    {
        List<Loan> prestamos = LeerArchivo();
        return prestamos.Count();
    }

    /// <summary>
    /// Retorna la lista histórica completa de préstamos.
    /// </summary>
    public List<Loan> ObtenerTodo()
    {
        return LeerArchivo();
    }

    /// <summary>
    /// Ordena el conjunto de transacciones basándose en una propiedad selector.
    /// </summary>
    public List<Loan> OrdenarTodo(Func<Loan, object> criterio)
    {
        List<Loan> prestamos = LeerArchivo();
        return prestamos.OrderBy(criterio).ToList();
    }
}