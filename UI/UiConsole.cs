/// <summary>
/// Clase encargada de renderizar la interfaz de usuario por consola, imprimiendo menús, catálogos y mensajes de estado.
/// </summary>
public class UiConsole
{
    /// <summary>
    /// Muestra por consola la lista detallada de libros físicos.
    /// </summary>
    /// <param name="books">Colección de objetos Book.</param>
    public void MostrarLibros(List<Book> books)
    {
        Console.ForegroundColor = ConsoleColor.White;
        foreach (Book e in books)
        {
            Console.WriteLine(e.ToString());
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Muestra por consola la lista detallada de libros electrónicos.
    /// </summary>
    /// <param name="ebooks">Colección de objetos Ebook.</param>
    public void MostrarEbooks(List<Ebook> ebooks)
    {
        Console.ForegroundColor = ConsoleColor.White;
        foreach (Ebook e in ebooks)
        {
            Console.WriteLine(e.ToString());
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Muestra por consola el listado de usuarios registrados.
    /// </summary>
    /// <param name="users">Colección de objetos User.</param>
    public void MostrarUser(List<User> users)
    {
        Console.ForegroundColor = ConsoleColor.White;
        foreach (User e in users)
        {
            Console.WriteLine(e.ToString());
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Muestra por consola el historial de transacciones de préstamo.
    /// </summary>
    /// <param name="loans">Colección de objetos Loan.</param>
    public void MostrarPrestamos(List<Loan> loans)
    {
        Console.ForegroundColor = ConsoleColor.White;
        foreach (Loan e in loans)
        {
            Console.WriteLine(e.ToString());
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Pausa la ejecución de la consola esperando confirmación del operador.
    /// </summary>
    public void PresioneParaContinuar()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ResetColor();
        Console.ReadKey();
    }

    /// <summary>
    /// Pinta en consola el menú principal del sistema de biblioteca.
    /// </summary>
    /// <param name="totalLibros">Conteo de libros físicos.</param>
    /// <param name="totalEbooks">Conteo de libros digitales.</param>
    /// <param name="totalUsuarios">Conteo de lectores.</param>
    /// <param name="totalPrestamos">Conteo histórico de transacciones.</param>
    public void MostrarMenuPrincipal(int totalLibros, int totalEbooks, int totalUsuarios, int totalPrestamos)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║            SISTEMA DE GESTIÓN DE BIBLIOTECA              ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║[Libros: {totalLibros} | Ebooks: {totalEbooks}]  [Lectores: {totalUsuarios}]  [Historial Préstamos: {totalPrestamos}]║ ");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔═════════════════════════════════════════════╗");
        Console.WriteLine("║   1. Gestión de Catálogo (Libros/Ebooks)    ║");
        Console.WriteLine("║   2. Gestión de Usuarios                    ║");
        Console.WriteLine("║   3. Gestión de Préstamos                   ║");
        Console.WriteLine("║   4. Consultar reportes generales           ║");
        Console.WriteLine("║   5. Salir                                  ║");
        Console.WriteLine("╚═════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    /// <summary>
    /// Renderiza el submenú de gestión de catálogo.
    /// </summary>
    public void MenuGestionCatalogo()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║             GESTIÓN DE CATÁLOGO              ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║   1. Administrar Libros Físicos              ║");
        Console.WriteLine("║   2. Administrar Libros Electrónicos/Ebooks  ║");
        Console.WriteLine("║   3. Listar Todo el Catálogo                 ║");
        Console.WriteLine("║   4. Búsquedas y Filtros Avanzados           ║");
        Console.WriteLine("║   5. Volver al Menú Principal                ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.ResetColor();
    }
    /// <summary>
    /// Renderiza el submenú de administración de libros físicos.
    /// </summary>
    public void MenuLibros()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║              GESTIÓN DE LIBROS               ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║      1. Agregar Libro                        ║");
        Console.WriteLine("║      2. Listar Libros                        ║");
        Console.WriteLine("║      3. Actualizar Libro                     ║");
        Console.WriteLine("║      4. Eliminar Libro                       ║");
        Console.WriteLine("║      5. Volver al Menú Anterior              ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ResetColor();
    }

    /// <summary>
    /// Renderiza el submenú de administración de Ebooks.
    /// </summary>
    public void MenuEbooks()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║              GESTIÓN DE EBOOKS               ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║      1. Agregar Ebook                        ║");
        Console.WriteLine("║      2. Listar Ebooks                        ║");
        Console.WriteLine("║      3. Actualizar Ebook                     ║");
        Console.WriteLine("║      4. Eliminar Ebook                       ║");
        Console.WriteLine("║      5. Volver al Menú Anterior              ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ResetColor();
    }
    /// <summary>
    /// Renderiza el submenú de administración de usuarios.
    /// </summary>
    public void MostrarMenuUsuarios()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║             GESTIÓN DE USUARIOS              ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║     1. Agregar Usuario                       ║");
        Console.WriteLine("║     2. Listar Usuarios                       ║");
        Console.WriteLine("║     3. Actualizar Usuario                    ║");
        Console.WriteLine("║     4. Eliminar Usuario                      ║");
        Console.WriteLine("║     5. Consultas y Filtros                   ║");
        Console.WriteLine("║     6. Volver al Menú Principal              ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ResetColor();
    }

    /// <summary>
    /// Renderiza el submenú de administración de préstamos.
    /// </summary>
    public void MostrarMenuPrestamos()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║            GESTIÓN DE PRÉSTAMOS              ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║     1. Registrar Préstamo                    ║");
        Console.WriteLine("║     2. Listar Préstamos                      ║");
        Console.WriteLine("║     3. Registrar Devolución                  ║");
        Console.WriteLine("║     4. Consultas y Filtros                   ║");
        Console.WriteLine("║     5. Volver al Menú Principal              ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ResetColor();
    }
    /// <summary>
    /// Renderiza el menú de reportes consolidados del sistema.
    /// </summary>
    public void MenuConsultasGlobales()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║          REPORTES CONSOLIDADOS               ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║     1. Resumen General del Catálogo          ║");
        Console.WriteLine("║     2. Préstamos Activos                     ║");
        Console.WriteLine("║     3. Préstamos Vencidos                    ║");
        Console.WriteLine("║     4. Volver al Menú Principal              ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        Console.ResetColor();
    }
}