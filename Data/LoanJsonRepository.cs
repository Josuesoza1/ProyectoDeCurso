using System.Text.Json;
public class LoanJsonRepository : ILoanRepository
{
    private readonly string _rutaArchivo;

    public LoanJsonRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
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

    public List<Loan> ObtenerTodos()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Loan>();

        string json = File.ReadAllText(_rutaArchivo);
        return JsonSerializer.Deserialize<List<Loan>>(json) ?? new List<Loan>();
    }
}
