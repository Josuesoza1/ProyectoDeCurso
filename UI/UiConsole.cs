public class UiConsole
{
    public void MostrarLibros(
    List<Book> books)
    {
        foreach (Book e in books)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public void MostrarEbooks(
        List<Ebook> ebooks)
    {
        foreach (Ebook e in ebooks)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void MostrarUser(
        List<User> users)
    {
        foreach (User e in users)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void MostrarPrestamos(
        List<Loan> loans)
    {
        foreach (Loan e in loans)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void PresioneParaContinuar()
    {
        Console.WriteLine();
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }


    public void MostrarMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("=== SISTEMA DE GESTIÓN DE BIBLIOTECA ===");
        Console.WriteLine("1. Gestión de Catálogo (Libros/Ebooks)");
        Console.WriteLine("2. Gestión de Usuarios");
        Console.WriteLine("3. Gestión de Préstamos");
        Console.WriteLine("4. Salir");
    }

    public void MenuGestionCatalogo()
    {
        Console.Clear();
        Console.WriteLine("=== GESTIÓN DE CATÁLOGO ===");
        Console.WriteLine("1. Administrar Libros Físicos ");
        Console.WriteLine("2. Administrar Libros Electrónicos / Ebooks");
        Console.WriteLine("3. Listar Todo el Catálogo Registrado");
        Console.WriteLine("4. Motor de Búsquedas, Filtros y Ordenamientos Avanzados");
        Console.WriteLine("5. Volver al menú principal");
    }
    public void MenuLibros()
    {
        Console.Clear();
        Console.WriteLine("=== GESTIÓN DE LIBROS ===");
        Console.WriteLine("1. Agregar Libro");
        Console.WriteLine("2. Listar Libros");
        Console.WriteLine("3. Actualizar Libro");
        Console.WriteLine("4. Eliminar Libro");
        Console.WriteLine("5. Volver al menú anterior");
    }




    public void MenuEbooks()
    {
        Console.Clear();
        Console.WriteLine("=== GESTIÓN DE EBOOKS ===");
        Console.WriteLine("1. Agregar Ebook");
        Console.WriteLine("2. Listar Ebooks");
        Console.WriteLine("3. Actualizar Ebook");
        Console.WriteLine("4. Eliminar Ebook");
        Console.WriteLine("5. Volver al menú anterior");
    }


    public void MostrarMenuUsuarios()
    {
        Console.Clear();
        Console.WriteLine("=== GESTIÓN DE USUARIOS ===");
        Console.WriteLine("1. Agregar Usuario");
        Console.WriteLine("2. Listar Usuarios");
        Console.WriteLine("3. Actualizar Usuario");
        Console.WriteLine("4. Eliminar Usuario");
        Console.WriteLine("5. Volver al menú principal");
    }


    public void MostrarMenuPrestamos()
    {
        Console.Clear();
        Console.WriteLine("=== GESTIÓN DE PRÉSTAMOS ===");
        Console.WriteLine("1. Agregar Préstamo");
        Console.WriteLine("2. Listar Préstamos");
        Console.WriteLine("3. Registrar Devolución / Editar Préstamo");
        Console.WriteLine("4. Volver al menú principal");
    }



}

