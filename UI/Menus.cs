public class Menus
{
    private readonly LibrarySystem _sistema;
    private readonly UiConsole _uiconsole;
    private readonly BookService _bookservice;
    private readonly EbookService _ebookservice;
    private readonly LoanService _loanservice;
    private readonly UserService _userservice;

    public Menus(BookService bookservice, EbookService ebookservice, LoanService loanservice, UserService userservice, UiConsole uiconsole)
    {
        _bookservice = bookservice;
        _ebookservice = ebookservice;
        _loanservice = loanservice;
        _userservice = userservice;
        _uiconsole = uiconsole;
    }

    public void EditarLibro()
    {
        Console.Clear();
        Console.WriteLine("ACTUALIZAR LIBRO");
        Console.Write("Ingrese el ISBN del libro que desea modificar: ");
        string isbn = Console.ReadLine();

        Book libro = _bookservice.BuscarPorISBN(isbn);

        if (libro == null)
        {
            Console.WriteLine("Libro no encontrado.");
            return;
        }

        Console.WriteLine("\nLibro actual:");
        Console.WriteLine(libro.ToString());

        Console.WriteLine("\n¿Qué dato desea actualizar?");
        Console.WriteLine("1. Título");
        Console.WriteLine("2. Editorial");
        Console.WriteLine("3. Género");
        Console.Write("Opción: ");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                Console.Write("Nuevo título: ");
                libro.ActualizarTitulo(Console.ReadLine());
                break;
            case "2":
                Console.Write("Nueva editorial: ");
                libro.ActualizarEditorial(Console.ReadLine());
                break;
            case "3":
                Console.Write("Nuevo genero");
                libro.ActualizarGenero(Console.ReadLine());
                break;
        }
        _bookservice.ActualizarBook(libro);
        Console.WriteLine("\n¡Libro actualizado correctamente!");
    }

    public void EditarEbook()
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR EBOOK ===");
        Console.Write("Ingrese el DOI del Ebook que desea modificar: ");
        string doi = Console.ReadLine();


        Ebook ebook = _ebookservice.Busqueda(doi);

        if (ebook == null)
        {
            Console.WriteLine("Ebook no encontrado en el sistema.");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("\nDatos actuales del Ebook:");
        Console.WriteLine(ebook.ToString());

        Console.WriteLine("\n¿Qué dato desea actualizar?");
        Console.WriteLine("1. Título");
        Console.WriteLine("2. URL de Descarga");
        Console.WriteLine("3. Idioma");
        Console.WriteLine("4. Formato (PDF/EPUB/MOBI)");
        Console.Write("Opción: ");

        int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

        try
        {
            switch (opcion)
            {
                case 1:
                    Console.Write("Nuevo título: ");
                    ebook.ActualizarTitulo(Console.ReadLine());
                    break;
                case 2:
                    Console.Write("Nueva URL (http/https): ");
                    ebook.ActualizarURL(Console.ReadLine());
                    break;
                case 3:
                    Console.Write("Nuevo idioma (ES/EN/FR/PT): ");
                    ebook.ActualizarIdioma(Console.ReadLine());
                    break;
                case 4:
                    Console.Write("Nuevo formato (PDF/EPUB/MOBI): ");
                    ebook.ActualizarFormato(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    return;
            }


            _ebookservice.ActualizarEbook(ebook);
            Console.WriteLine("\n¡Ebook actualizado correctamente!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nError al actualizar: {ex.Message}");
        }

        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    public void MostrarCatalogoCompleto()
    {
        Console.Clear();
        Console.WriteLine("═══ CATÁLOGO COMPLETO DE LA BIBLIOTECA ═══\n");


        var libros = _bookservice.ObtenerTodo();
        var ebooks = _ebookservice.ObtenerTodo();


        Console.WriteLine($"--- LIBROS FÍSICOS ({libros.Count}) ---");
        if (libros.Count == 0)
        {
            Console.WriteLine("(No hay libros físicos registrados en el sistema)\n");
        }
        else
        {
            foreach (var libro in libros)
            {
                Console.WriteLine(libro.ToString());
                Console.WriteLine();
            }
        }


        Console.WriteLine($"--- LIBROS ELECTRÓNICOS / EBOOKS ({ebooks.Count}) ---");
        if (ebooks.Count == 0)
        {
            Console.WriteLine("(No hay Ebooks registrados en el sistema)\n");
        }
        else
        {
            foreach (var ebook in ebooks)
            {
                Console.WriteLine(ebook.ToString());
                Console.WriteLine();
            }
        }
;
    }


    public void MenuBusqueda()
    {
        bool volver = false;
        do
        {
            _uiconsole.MenuBusqueda();
            Console.Write("\nSeleccione una opción: ");
            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Ingrese el ISBN del libro a buscar:");
                    string isbn = Console.ReadLine().Trim();
                    var libro = _bookservice.BuscarPorISBN(isbn);
                    if (libro != null)
                    {
                        Console.WriteLine("Libro encontrado:");
                        Console.WriteLine(libro.ToString());
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún libro con ese ISBN.");
                    }
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 2:
                    Console.WriteLine("Ingrese el DOI del ebook a buscar:");
                    string doi = Console.ReadLine().Trim();
                    var ebook = _ebookservice.Busqueda(doi);
                    if (ebook != null)
                    {
                        Console.WriteLine("Ebook encontrado:");
                        Console.WriteLine(ebook.ToString());
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún ebook con ese DOI.");
                    }
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 3:
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        } while (!volver);
    }

    public void MenuFiltros()
    {
        bool volver = false;
        do
        {
            _uiconsole.MenuFiltro();
            Console.Write("\nSeleccione una opción: ");
            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Ingrese el autor a filtrar:");
                    string autor = Console.ReadLine().Trim();
                    var librosFiltrados = _bookservice.FiltrarPorAutor(autor);
                    if (librosFiltrados.Count == 0)
                    {
                        Console.WriteLine("No se encontraron libros de ese autor.");
                    }
                    else
                    {
                        Console.WriteLine($"Libros del autor '{autor}':");
                        foreach (var libro in librosFiltrados)
                        {
                            Console.WriteLine(libro.ToString());
                            Console.WriteLine();
                        }
                    }
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 2:
                    Console.WriteLine("Ingrese el título a filtrar:");
                    string titulo = Console.ReadLine().Trim();
                    var ebooksFiltrados = _ebookservice.FiltrarPorTitulo(titulo);
                    if (ebooksFiltrados.Count == 0)
                    {
                        Console.WriteLine("No se encontraron ebooks con ese título.");
                    }
                    else
                    {
                        Console.WriteLine($"Ebooks con título '{titulo}':");
                        foreach (var ebook in ebooksFiltrados)
                        {
                            Console.WriteLine(ebook.ToString());
                            Console.WriteLine();
                        }
                    }
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 3:
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        } while (!volver);
    }

    public void MenuOrdenamiento()
    {
        bool volver = false;
        do
        {
            _uiconsole.MenuOrdenamiento();
            Console.Write("\nSeleccione una opción: ");
            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
            switch (opcion)
            {
                case 1:
                    var librosOrdenados = _bookservice.OrdenarPorTitulo();
                    Console.WriteLine("Libros ordenados por título:");
                    foreach (var libro in librosOrdenados)
                    {
                        Console.WriteLine(libro.ToString());
                        Console.WriteLine();
                    }
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 2:
                    var ebooksOrdenados = _ebookservice.OrdenarPorTitulo();
                    Console.WriteLine("Ebooks ordenados por título:");
                    foreach (var ebook in ebooksOrdenados)
                    {
                        Console.WriteLine(ebook.ToString());
                        Console.WriteLine();
                    }
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 3:
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        } while (!volver);
    }

    public void MenuLibros()
    {
        bool volver = false;
        do
        {
            _uiconsole.MenuLibros();
            Console.Write("\nSeleccione una opción: ");
            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Ingrese el ID del libro:");
                    int id = int.TryParse(Console.ReadLine(), out int idResult) ? idResult : 0;
                    Console.WriteLine("Ingrese el título del libro:");
                    string titulo = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el autor del libro:");
                    string autor = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el género del libro:");
                    string genero = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el año de publicación del libro:");
                    int añoPublicacion = int.TryParse(Console.ReadLine(), out int añoResult) ? añoResult : 0;
                    Console.WriteLine("Ingrese el número de copias disponibles del libro:");
                    int copias = int.TryParse(Console.ReadLine(), out int copiasResult) ? copiasResult : 0;
                    Console.WriteLine("Ingrese el ISBN del libro:");
                    string isbn = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el número de páginas del libro:");
                    int paginas = int.TryParse(Console.ReadLine(), out int paginasResult) ? paginasResult : 0;
                    Console.WriteLine("Ingrese la editoria del libro:");
                    string editoria = Console.ReadLine().Trim();
                    _bookservice.RegistrarBook(id, titulo, autor, genero, añoPublicacion, copias, isbn, paginas, editoria);
                    break;
                case 2:
                    var libros = _bookservice.ObtenerTodo();
                    if (libros.Count == 0)
                    {
                        Console.WriteLine("No hay libros registrados.");
                    }
                    else
                    {
                        Console.WriteLine("=== LISTA DE LIBROS ===");
                        foreach (var libro in libros)
                        {
                            Console.WriteLine(libro.ToString());
                        }
                    }
                    _uiconsole.PresioneParaContinuar();

                    break;
                case 3:
                    EditarLibro();
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 4:

                    Console.Clear();
                    Console.WriteLine("Ingrese el ISBN del libro a eliminar:");
                    string isbnEliminar = Console.ReadLine().Trim();
                    _bookservice.EliminarBook(isbnEliminar);
                    Console.WriteLine($"Lista Actualizada despues de la eliminación del libro con ISBN {isbnEliminar}.");
                    _uiconsole.MostrarLibros(_bookservice.ObtenerTodo());
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 5:
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        }
        while (!volver);
    }


    public void MenuEbooks()
    {
        bool volver = false;
        do
        {
            _uiconsole.MenuEbooks();
            Console.Write("\nSeleccione una opción: ");
            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Ingrese el ID del ebook:");
                    int id = int.TryParse(Console.ReadLine(), out int idResult) ? idResult : 0;
                    Console.WriteLine("Ingrese el título del ebook:");
                    string titulo = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el autor del ebook:");
                    string autor = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el género del ebook:");
                    string genero = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el año de publicación del ebook:");
                    int añoPublicacion = int.TryParse(Console.ReadLine(), out int añoResult) ? añoResult : 0;
                    Console.WriteLine("Ingrese el número de copias disponibles del ebook:");                      //No tiene mucho sentido si es algo digital (Observacion)
                    int copias = int.TryParse(Console.ReadLine(), out int copiasResult) ? copiasResult : 0;
                    Console.WriteLine("Ingrese el DOI del ebook:");
                    string doi = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el formato del ebook (PDF/EPUB/MOBI):");
                    string formato = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el tamaño del ebook en MB:");
                    double tamano = double.TryParse(Console.ReadLine(), out double tamanoResult) ? tamanoResult : 0;
                    Console.WriteLine("Ingrese la URL de descarga del ebook:");
                    string urlDescarga = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el idioma del ebook (ES/EN/FR/PT):");
                    string idioma = Console.ReadLine().Trim();
                    _ebookservice.RegistrarEbook(doi, id, titulo, autor, genero, añoPublicacion, copias, formato, tamano, urlDescarga, idioma);
                    break;
                case 2:

                    var ebooks = _ebookservice.ObtenerTodo();
                    if (ebooks.Count == 0)
                    {
                        Console.WriteLine("No hay ebooks registrados.");
                    }
                    else
                    {
                        Console.WriteLine("=== LISTA DE EBOOKS ===");
                        foreach (var ebook in ebooks)
                        {
                            Console.WriteLine(ebook.ToString());
                        }
                    }
                    _uiconsole.PresioneParaContinuar();



                    break;
                case 3:
                    EditarEbook();
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Ingrese el DOI del ebook a eliminar:");
                    string doiEliminar = Console.ReadLine().Trim();
                    _ebookservice.EliminarEbook(doiEliminar);
                    Console.WriteLine($"Lista Actualizada despues de la eliminación del ebook con DOI {doiEliminar}.");
                    _uiconsole.MostrarEbooks(_ebookservice.ObtenerTodo());
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 5:
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        } while (!volver);
    }




    public void MenuGestionCatalogo()
    {
        bool volver = false;
        do
        {
            _uiconsole.MenuGestionCatalogo();
            Console.Write("\nSeleccione una opción: ");
            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
            switch (opcion)
            {
                case 1:
                    MenuLibros();
                    break;
                case 2:
                    MenuEbooks();
                    break;
                case 3:
                    MostrarCatalogoCompleto();
                    _uiconsole.PresioneParaContinuar();

                    break;
                case 4:
                    MenuBusqueda();
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 5:
                    MenuFiltros();
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 6:
                    MenuOrdenamiento();
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 7:
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        } while (!volver);
    }










    public void EditarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR USUARIO ===");
        Console.Write("Ingrese el ID del usuario que desea modificar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            Console.ReadKey();
            return;
        }

        User usuario = _userservice.BuscarPorId(id);

        if (usuario == null)
        {
            Console.WriteLine("Usuario no encontrado.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\nDatos actuales del usuario:");
        Console.WriteLine(usuario.ToString());

        Console.WriteLine("\n¿Qué dato desea actualizar?");
        Console.WriteLine("1. Nombre");
        Console.WriteLine("2. Apellido");
        Console.WriteLine("3. Correo Electrónico");
        Console.WriteLine("4. Teléfono");
        Console.Write("Opción: ");

        int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

        try
        {
            switch (opcion)
            {
                case 1:
                    Console.Write("Nuevo nombre: ");
                    usuario.ActualizarNombre(Console.ReadLine());
                    break;
                case 2:
                    Console.Write("Nuevo apellido: ");
                    usuario.ActualizarApellido(Console.ReadLine());
                    break;
                case 3:
                    Console.Write("Nuevo correo: ");
                    usuario.ActualizarCorreo(Console.ReadLine());
                    break;
                case 4:
                    Console.Write("Nuevo teléfono (8 dígitos): ");
                    usuario.ActualizarTelefono(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    return;
            }
            _userservice.ActualizarUser(usuario);
            Console.WriteLine("\n¡Usuario actualizado correctamente!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nError en la validación: {ex.Message}");
        }

        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }



    public void GestionarDevolucionOEditarPrestamo()
    {
        Console.Clear();
        Console.WriteLine("=== GESTIÓN DE PRÉSTAMO / DEVOLUCIÓN ===");
        Console.Write("Ingrese el ID del préstamo: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            Console.ReadKey();
            return;
        }


        Loan prestamo = _loanservice.BuscarPorId(id);

        if (prestamo == null)
        {
            Console.WriteLine("Registro de préstamo no encontrado.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\nInformación del préstamo:");
        Console.WriteLine(prestamo.ToString());

        Console.WriteLine("\n¿Qué acción desea realizar?");
        Console.WriteLine("1. Registrar Devolución Exitosa (Hoy)");
        Console.WriteLine("2. Actualizar Observaciones / Notas");
        Console.Write("Opción: ");

        int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

        switch (opcion)
        {
            case 1:
                Console.Write("Ingrese observaciones de entrega (ej. 'Entregado a tiempo', 'Portada rayada'): ");
                string notasDevolucion = Console.ReadLine();

                prestamo.RegistrarDevolucion(DateTime.Now, notasDevolucion);

                break;

            case 2:
                Console.Write("Nuevas observaciones generales: ");
                prestamo.ActualizarObservaciones(Console.ReadLine());
                break;

            default:
                Console.WriteLine("Opción cancelada.");
                return;
        }

        
        _loanservice.ActualizarLoan(prestamo);
        Console.WriteLine("\n¡El estado del préstamo se actualizó con éxito!");
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    public void MostrarMenuPrincipal()
    {
        bool salir = false;
        do
        {
            _uiconsole.MostrarMenuPrincipal();
            Console.Write("\nSeleccione una opción: ");

            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            switch (opcion)
            {
                case 1:
                    MenuGestionCatalogo();
                    break;
                case 2:
                    // MenuUsuarios();                   //Cocinando (Aún falta)
                    break;
                case 3:
                    // MenuPrestamos();                  //Lo mismo que arriba
                    break;
                case 4:
                    salir = true;
                    Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        } while (!salir);
    }
}