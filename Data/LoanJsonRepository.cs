using System.Text.Json;

public class LoanJsonRepository : ILoanRepository
{
    private readonly string _rutaArchivo;

    public LoanJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;

        if (!File.Exists(_rutaArchivo))
            File.WriteAllText(_rutaArchivo, "[]");
    }

    public void GuardarTodos(List<Loan> prestamos)
    {
        string json = JsonSerializer.Serialize(prestamos,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
        File.WriteAllText(_rutaArchivo, json);
    }

    public List<Loan> LeerArchivo()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Loan>();

        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<Loan>>(json) ?? new List<Loan>();
    }

    public void Actualizar(Loan prestamoModificado)
    {
        List<Loan> prestamos = LeerArchivo();

        int index = prestamos.FindIndex(p => p.IdPrestamo == prestamoModificado.IdPrestamo);

        if (index == -1)
            throw new ArgumentException("Préstamo No Encontrado.");

        prestamos[index] = prestamoModificado;

        GuardarTodos(prestamos);
    }
    public void Agregar(Loan loan)
    {
        List<Loan> prestamos = LeerArchivo();

        if (prestamos.Any(p => p.IdPrestamo == loan.IdPrestamo))
            throw new InvalidOperationException("El préstamo ya existe con ese ID");

        prestamos.Add(loan);
        GuardarTodos(prestamos);
    }

    public Loan Buscar(Func<Loan, bool> criterio)
    {
        List<Loan> prestamos = LeerArchivo();
        return prestamos.FirstOrDefault(criterio);
    }

    
    public void Eliminar(int id)
    {
        List<Loan> prestamos = LeerArchivo();

        Loan prestamo = prestamos.FirstOrDefault(p => p.IdPrestamo == id) ??
            throw new InvalidOperationException("El ID del préstamo no existe");

        prestamos.Remove(prestamo);
        GuardarTodos(prestamos);
    }

    public List<Loan> Filtrar(Func<Loan, bool> criterio)
    {
        List<Loan> prestamos = LeerArchivo();
        return prestamos.Where(criterio).ToList();
    }

    public int MostrarTotal()
    {
        List<Loan> prestamos = LeerArchivo();
        return prestamos.Count();
    }

    public List<Loan> ObtenerTodo()
    {
        return LeerArchivo();
    }

    public List<Loan> OrdenarTodo(Func<Loan, object> criterio)
    {
        List<Loan> prestamos = LeerArchivo();
        return prestamos.OrderBy(criterio).ToList();
    }
}