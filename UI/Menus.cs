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


    public void MenuConsultasCatalogo()
    {
        bool volver = false;
        do
        {
            Console.Clear();
            Console.WriteLine("═══ MOTOR DE CONSULTAS GENERALES (CATÁLOGO) ═══");
            Console.WriteLine("1. Buscar por Título (Coincidencia parcial en Ambos)");
            Console.WriteLine("2. Buscar por Autor (Coincidencia parcial en Ambos)");
            Console.WriteLine("3. Buscar Libro Físico por ISBN (Exclusivo Físico)");
            Console.WriteLine("4. Buscar Ebook por código DOI (Exclusivo Digital)");
            Console.WriteLine("5. Ordenar Todo el Catálogo por Título (A-Z)");
            Console.WriteLine("6. Ordenar Todo el Catálogo por Año de Publicación");
            Console.WriteLine("7. Volver al menú de gestión");
            Console.Write("\nSeleccione una opción: ");

            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            List<Catalog> todoElCatalogo = new List<Catalog>();
            todoElCatalogo.AddRange(_bookservice.ObtenerTodo());
            todoElCatalogo.AddRange(_ebookservice.ObtenerTodo());

            List<Catalog> resultados = new List<Catalog>();

            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    Console.Write("Ingrese las palabras clave del título: ");
                    string filtroTitulo = Console.ReadLine().Trim();
                    resultados = todoElCatalogo
                        .Where(c => c.Titulo != null && c.Titulo.Contains(filtroTitulo, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    ImprimirResultadosMixtos(resultados);
                    break;

                case 2:
                    Console.Clear();
                    Console.Write("Ingrese el nombre del autor: ");
                    string filtroAutor = Console.ReadLine().Trim();
                    resultados = todoElCatalogo
                        .Where(c => c.Autor != null && c.Autor.Contains(filtroAutor, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    ImprimirResultadosMixtos(resultados);
                    break;

                case 3:
                    Console.Clear();
                    Console.Write("Ingrese el ISBN de 13 dígitos: ");
                    string isbnBuscado = Console.ReadLine().Trim();
                    var libroFisico = _bookservice.BuscarPorISBN(isbnBuscado);
                    if (libroFisico != null) resultados.Add(libroFisico);
                    ImprimirResultadosMixtos(resultados);
                    break;

                case 4:
                    Console.Clear();
                    Console.Write("Ingrese el DOI del documento electrónico: ");
                    string doiBuscado = Console.ReadLine().Trim();
                    var ebookDigital = _ebookservice.Busqueda(doiBuscado);
                    if (ebookDigital != null) resultados.Add(ebookDigital);
                    ImprimirResultadosMixtos(resultados);
                    break;

                case 5:
                    Console.Clear();
                    Console.WriteLine("=== TODO EL CATÁLOGO MIXTO ORDENADO POR TÍTULO ===");
                    resultados = todoElCatalogo.OrderBy(c => c.Titulo ?? string.Empty).ToList();
                    ImprimirResultadosMixtos(resultados);
                    break;

                case 6:
                    Console.Clear();
                    Console.WriteLine("=== TODO EL CATÁLOGO MIXTO ORDENADO POR AÑO (MÁS ANTIGUOS PRIMERO) ===");
                    resultados = todoElCatalogo.OrderBy(c => c.Anio).ToList();
                    ImprimirResultadosMixtos(resultados);
                    break;

                case 7:
                    volver = true;
                    break;

                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    _uiconsole.PresioneParaContinuar();
                    break;
            }
        } while (!volver);
    }


    private void ImprimirResultadosMixtos(List<Catalog> lista)
    {
        Console.WriteLine($"\n--- Coincidencias encontradas: ({lista.Count}) ---");
        if (lista.Count == 0)
        {
            Console.WriteLine("No se registraron coincidencias bajo los parámetros indicados.");
        }
        else
        {
            foreach (var item in lista)
            {

                Console.WriteLine(item.ToString());
                Console.WriteLine();
            }
        }
        _uiconsole.PresioneParaContinuar();
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
                    MenuConsultasCatalogo();
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

        Console.WriteLine("\nInformación actual del préstamo:");
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
                Console.WriteLine("Opción cancelada o no válida.");
                _uiconsole.PresioneParaContinuar();
                return;
        }

        
        _loanservice.ActualizarLoan(prestamo);

        Console.WriteLine("\n¡El estado del préstamo se actualizó con éxito en el sistema!");
        _uiconsole.PresioneParaContinuar();
    }

    public void MenuUsuarios()
    {
        bool volver = false;
        do
        {
            _uiconsole.MostrarMenuUsuarios();
            Console.Write("\nSeleccione una opción: ");
            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Ingrese el ID del usuario:");
                    int id = int.TryParse(Console.ReadLine(), out int idResult) ? idResult : 0;
                    Console.WriteLine("Ingrese el nombre del usuario:");
                    string nombre = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el apellido del usuario:");
                    string apellido = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el correo electrónico del usuario:");
                    string correo = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese el teléfono del usuario (8 dígitos):");
                    string telefono = Console.ReadLine().Trim();
                    _userservice.RegistrarUser(id, nombre, apellido, correo, telefono);
                    break;
                case 2:
                    var usuarios = _userservice.ObtenerTodo();
                    if (usuarios.Count == 0)
                    {
                        Console.WriteLine("No hay usuarios registrados.");
                    }
                    else
                    {
                        Console.WriteLine("=== LISTA DE USUARIOS ===");
                        foreach (var user in usuarios)
                        {
                            Console.WriteLine(user.ToString());
                        }
                    }
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 3:
                    EditarUsuario();
                    _uiconsole.PresioneParaContinuar();
                    break;
                case 4:
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Ingrese el ID del usuario a eliminar:");
                        if (!int.TryParse(Console.ReadLine(), out int idEliminar))
                        {

                            Console.WriteLine("ID inválido.");
                            Console.ReadKey();
                            return;
                        }

                        _userservice.EliminarUser(idEliminar);
                        Console.WriteLine($"Lista Actualizada despues de la eliminación del usuario con ID {idEliminar}.");
                        _uiconsole.MostrarUser(_userservice.ObtenerTodo());
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
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


    public void MenuPrestamos()
    {
        bool volver = false;
        do
        {
            _uiconsole.MostrarMenuPrestamos();
            Console.Write("\nSeleccione una opción: ");
            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("=== REGISTRAR NUEVO PRÉSTAMO ===");

                    
                    Console.Write("Ingrese el ID del Usuario: ");
                    if (!int.TryParse(Console.ReadLine(), out int idUsuario)) { Console.WriteLine("ID inválido."); break; }

                    var usuario = _userservice.BuscarPorId(idUsuario);
                    if (usuario == null)
                    {
                        Console.WriteLine("\n No existe ningún usuario registrado con ese ID.");
                        _uiconsole.PresioneParaContinuar();
                        break; 
                    }

                    
                    Console.Write("Ingrese el ID del Libro o Ebook a prestar: ");
                    if (!int.TryParse(Console.ReadLine(), out int idItem)) { Console.WriteLine("ID inválido."); break; }

                    string tipoItem = "";
                    var libroFisico = _bookservice.BuscarPorId(idItem);
                    var ebookDigital = _ebookservice.BuscarPorId(idItem);

                    if (libroFisico != null) tipoItem = "Libro Físico";
                    else if (ebookDigital != null) tipoItem = "Ebook";
                    else
                    {
                        Console.WriteLine("\n No existe ningún artículo en el catálogo con ese ID.");
                        _uiconsole.PresioneParaContinuar();
                        break; 
                    }

                    
                    Console.Write("\nIngrese un ID único para este nuevo registro de préstamo: ");
                    if (!int.TryParse(Console.ReadLine(), out int idPrestamo)) { Console.WriteLine("ID inválido."); break; }

                    try
                    {
                        
                        _loanservice.RegistrarLoan(idPrestamo, idUsuario, idItem, tipoItem, 14);
                        Console.WriteLine($"\n¡Éxito! Préstamo registrado a nombre de: {usuario.NombreCompleto}");
                        Console.WriteLine($"Artículo prestado: {(libroFisico != null ? libroFisico.Titulo : ebookDigital.Titulo)}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError al guardar: {ex.Message}");
                    }

                    _uiconsole.PresioneParaContinuar();
                    break;

                case 2:
                    Console.Clear();
                    var prestamos = _loanservice.ObtenerTodo();
                    if (prestamos.Count == 0) Console.WriteLine("No hay préstamos registrados.");
                    else
                    {
                        Console.WriteLine("=== LISTA DE PRÉSTAMOS ===");
                        foreach (var p in prestamos) { Console.WriteLine(p.ToString()); Console.WriteLine(); }
                    }
                    _uiconsole.PresioneParaContinuar();
                    break;

                case 3:
                    volver = true;
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    _uiconsole.PresioneParaContinuar();
                    break;
            }
        } while (!volver);
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
                    MenuUsuarios();
                    break;
                case 3:
                    MenuPrestamos();
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