public class Menus
{
    private readonly LibrarySystem _sistema;

    public Menus(LibrarySystem sistema)
    {
        _sistema = sistema;
    }

    public void MostrarMenuPrincipal()
    {
        bool salir = false;
        do
        {
            System.Console.Clear();
            System.Console.WriteLine("═══ SISTEMA DE BIBLIOTECA UNI ═══");
            System.Console.WriteLine("1. Gestión de Usuarios");
            System.Console.WriteLine("2. Gestión de Catálogo (Libros/Ebooks)");
            System.Console.WriteLine("3. Gestión de Préstamos");
            System.Console.WriteLine("4. Salir");
            System.Console.Write("\nSeleccione una opción: ");

            string opcion = System.Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    // MenuUsuarios();
                    break;
                case "2":
                    // MenuCatalogo();
                    break;
                case "3":
                    // MenuPrestamos();
                    break;
                case "4":
                    salir = true;
                    System.Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    System.Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                    System.Console.ReadKey();
                    break;
            }
        } while (!salir);
    }
}