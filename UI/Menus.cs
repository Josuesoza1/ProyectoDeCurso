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
        try
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
            Console.WriteLine("1. Título: ");
            Console.WriteLine("2. Editorial: ");
            Console.WriteLine("3. Género: ");
            Console.WriteLine("4. Cantidad en stock: ");
            Console.WriteLine("5. Regresar");
            Console.Write("Opción: ");

            int opcion = int.TryParse(Console.ReadLine(), out int resultado) ? resultado : 0;

            switch (opcion)
            {
                case 1:
                    Console.Write("Nuevo título: ");
                    libro.ActualizarTitulo(Console.ReadLine());
                    break;
                case 2:
                    Console.Write("Nueva editorial: ");
                    libro.ActualizarEditorial(Console.ReadLine());
                    break;
                case 3:
                    Console.Write("Nuevo genero: ");
                    libro.ActualizarGenero(Console.ReadLine());
                    break;
                case 4:
                    Console.Write("Nueva cantidad en stock: ");
                    if (!int.TryParse(Console.ReadLine(), out int nuevaCantidad))
                    {
                        Console.WriteLine("Cantidad inválida. Operación cancelada.");
                        _uiconsole.PresioneParaContinuar();
                        return;
                    }
                    libro.ActualizarCantidad(nuevaCantidad);
                    break;
                case 5:
                    Console.WriteLine("Operacion cancelada");
                    _uiconsole.PresioneParaContinuar();
                    return;
                default:
                    Console.WriteLine("Opcion no valda, Cancelando Operación...");
                    _uiconsole.PresioneParaContinuar();
                    return;
            }
            _bookservice.ActualizarBook(libro);
            Console.WriteLine("\n¡Libro actualizado correctamente!");
            _uiconsole.PresioneParaContinuar();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nError al actualizar: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError inesperado: {ex.Message}");
        }
    }

    public void EditarEbook()
    {
        try
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
            Console.WriteLine("5. Editar stock de licencias (Cantidad de copias disponibles)");
            Console.WriteLine("6. Regresar");
            Console.Write("Opción: ");

            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

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
                case 5:
                    Console.Write("Nueva cantidad de licencias: ");
                    if (!int.TryParse(Console.ReadLine(), out int nuevaCantidad))
                    {
                        Console.WriteLine("Cantidad inválida. Operación cancelada.");
                        _uiconsole.PresioneParaContinuar();
                        return;
                    }
                    ebook.ActualizarCantidad(nuevaCantidad);
                    break;
                case 6:
                    Console.WriteLine("Operación Cancelada");
                    _uiconsole.PresioneParaContinuar();
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    _uiconsole.PresioneParaContinuar();
                    return;
            }


            _ebookservice.ActualizarEbook(ebook);
            Console.WriteLine("\n¡Ebook actualizado correctamente!");

            _uiconsole.PresioneParaContinuar();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nError al actualizar: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError inesperado: {ex.Message}");
        }
    }

    public void MostrarCatalogoCompleto()
    {
        try
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
                _uiconsole.MostrarLibros(libros);
            }


            Console.WriteLine($"--- LIBROS ELECTRÓNICOS / EBOOKS ({ebooks.Count}) ---");
            if (ebooks.Count == 0)
            {
                Console.WriteLine("(No hay Ebooks registrados en el sistema)\n");
            }
            else
            {

                _uiconsole.MostrarEbooks(ebooks);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError al mostrar el catálogo: {ex.Message}");
        }
    }


    public void MenuConsultasCatalogo()
    {
        bool volver = false;
        do
        {
            try
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
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError en la consulta: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nError en la operación: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError inesperado: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
        } while (!volver);
    }


    private void ImprimirResultadosMixtos(List<Catalog> lista)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"\nError al imprimir resultados: {ex.Message}");
        }
    }


    public void MenuLibros()
    {
        bool volver = false;
        do
        {
            try
            {
                _uiconsole.MenuLibros();
                Console.Write("\nSeleccione una opción: ");
                int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese el ISBN del libro:");
                        string isbn = Console.ReadLine().Trim();
                        if (string.IsNullOrWhiteSpace(isbn))
                        {
                            isbn = "978" + new Random().Next(100000000, 999999999).ToString() + "1";
                            Console.WriteLine($"[Sistema] ISBN autogenerado para pruebas: {isbn}");
                        }

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
                        Console.WriteLine("Ingrese el número de páginas del libro:");
                        int paginas = int.TryParse(Console.ReadLine(), out int paginasResult) ? paginasResult : 0;
                        Console.WriteLine("Ingrese la editorial del libro:");
                        string editoria = Console.ReadLine().Trim();
                        _bookservice.RegistrarBook(titulo, autor, genero, añoPublicacion, copias, isbn, paginas, editoria);

                        Console.WriteLine("\n¡Éxito! El libro físico ha sido registrado en el catálogo de forma segura.");
                        Console.WriteLine($"Título: {titulo} | Stock Inicial: {copias} copias.");
                        _uiconsole.PresioneParaContinuar();
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
                        Console.WriteLine("Cancelando operación");
                        _uiconsole.PresioneParaContinuar();
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        _uiconsole.PresioneParaContinuar();
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError inesperado: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
        }
        while (!volver);
    }


    public void MenuEbooks()
    {
        bool volver = false;
        do
        {
            try
            {
                _uiconsole.MenuEbooks();
                Console.Write("\nSeleccione una opción: ");
                int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese el DOI del ebook:");
                        string doi = Console.ReadLine().Trim();
                        if (string.IsNullOrWhiteSpace(doi))
                        {
                            doi = $"10.{new Random().Next(1000, 9999)}/ebook-{new Random().Next(100, 999)}";
                            Console.WriteLine($"[Sistema] DOI autogenerado para pruebas: {doi}");
                        }
                        Console.WriteLine("Ingrese el título del ebook:");
                        string titulo = Console.ReadLine().Trim();
                        Console.WriteLine("Ingrese el autor del ebook:");
                        string autor = Console.ReadLine().Trim();
                        Console.WriteLine("Ingrese el género del ebook:");
                        string genero = Console.ReadLine().Trim();
                        Console.WriteLine("Ingrese el año de publicación del ebook:");
                        int añoPublicacion = int.TryParse(Console.ReadLine(), out int añoResult) ? añoResult : 0;
                        Console.WriteLine("Ingrese el número de copias disponibles del ebook:");
                        int copias = int.TryParse(Console.ReadLine(), out int copiasResult) ? copiasResult : 0;
                        Console.WriteLine("Ingrese el formato del ebook (PDF/EPUB/MOBI):");
                        string formato = Console.ReadLine().Trim();
                        Console.WriteLine("Ingrese el tamaño del ebook en MB:");
                        double tamano = double.TryParse(Console.ReadLine(), out double tamanoResult) ? tamanoResult : 0;
                        Console.WriteLine("Ingrese la URL de descarga del ebook:");
                        string urlDescarga = Console.ReadLine().Trim();
                        Console.WriteLine("Ingrese el idioma del ebook (ES/EN/FR/PT):");
                        string idioma = Console.ReadLine().Trim();
                        _ebookservice.RegistrarEbook(doi, titulo, autor, genero, añoPublicacion, copias, formato, tamano, urlDescarga, idioma);

                        Console.WriteLine("\n¡Éxito! El libro electrónico (Ebook) ha sido registrado y sus licencias están activas.");
                        Console.WriteLine($"Título: {titulo} | Formato: {formato.ToUpper()} | Licencias: {copias}.");
                        _uiconsole.PresioneParaContinuar();
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
                        Console.WriteLine("Opción no válida.");
                        _uiconsole.PresioneParaContinuar();
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError inesperado: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
        } while (!volver);
    }






    public void MenuGestionCatalogo()
    {
        bool volver = false;
        do
        {
            try
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
                        Console.WriteLine("Cancelando operación");
                        _uiconsole.PresioneParaContinuar();
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        _uiconsole.PresioneParaContinuar();
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError inesperado: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
        } while (!volver);
    }

    public void EditarUsuario()
    {

        try
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR USUARIO ===");
            Console.Write("Ingrese el ID del usuario que desea modificar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                _uiconsole.PresioneParaContinuar();
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
            Console.WriteLine("5. Salir");
            Console.Write("Opción: ");

            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
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
                case 5:
                    Console.WriteLine("Cancelando operación");
                    _uiconsole.PresioneParaContinuar();
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    _uiconsole.PresioneParaContinuar();
                    return;
            }
            _userservice.ActualizarUser(usuario);
            Console.WriteLine("\n¡Usuario actualizado correctamente!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nError en la validación: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\nError en la operación: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError inesperado: {ex.Message}");
        }

        _uiconsole.PresioneParaContinuar();
    }

    public void GestionarDevolucionOEditarPrestamo()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE PRÉSTAMO / DEVOLUCIÓN ===");
            Console.Write("Ingrese el ID del préstamo: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                _uiconsole.PresioneParaContinuar();
                return;
            }

            Loan prestamo = _loanservice.BuscarPorId(id);

            if (prestamo == null)
            {
                Console.WriteLine("Registro de préstamo no encontrado.");
                _uiconsole.PresioneParaContinuar();
                return;
            }

            Console.WriteLine("\nInformación actual del préstamo:");
            Console.WriteLine(prestamo.ToString());

            Console.WriteLine("\n¿Qué acción desea realizar?");
            Console.WriteLine("1. Registrar Devolución Exitosa (Hoy)");
            Console.WriteLine("2. Actualizar Observaciones / Notas");
            Console.WriteLine("3. Cancelar / Volver al menú anterior");
            Console.Write("Opción: ");

            int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            switch (opcion)
            {
                case 1:
                    if (prestamo.FechaDevolucionReal.HasValue)
                    {
                        Console.WriteLine("\n[AVISO] Este préstamo ya había sido devuelto.");
                        break;
                    }

                    Console.Write("Ingrese observaciones de entrega (ej. 'Entregado a tiempo', 'Portada rayada'): ");
                    string notasDevolucion = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(notasDevolucion)) notasDevolucion = "Devuelto sin novedades.";

                    Catalog itemDevuelto = null;

                    if (prestamo.TipoItem == "Libro Físico")
                    {
                        itemDevuelto = _bookservice.BuscarPorId(prestamo.ItemID);
                    }
                    else if (prestamo.TipoItem == "Ebook")
                    {
                        itemDevuelto = _ebookservice.BuscarPorId(prestamo.ItemID);
                    }

                    if (itemDevuelto != null)
                    {
                        itemDevuelto.DevolverItem();

                        if (itemDevuelto is Book libro)
                            _bookservice.ActualizarBook(libro);
                        else if (itemDevuelto is Ebook ebook)
                            _ebookservice.ActualizarEbook(ebook);

                        Console.WriteLine($"\n[SISTEMA] Stock actualizado. Copias actuales del artículo: {itemDevuelto.Cantidad}");
                    }
                    else
                    {
                        Console.WriteLine("\n[ADVERTENCIA] El artículo original ya no existe en el catálogo principal, pero se registrará la devolución del usuario.");
                    }

                    prestamo.RegistrarDevolucion(DateTime.Today, notasDevolucion);
                    break;

                case 2:
                    Console.Write("Nuevas observaciones generales: ");
                    prestamo.ActualizarObservaciones(Console.ReadLine());
                    break;

                case 3:
                    Console.WriteLine("Operación cancelada por el usuario.");
                    _uiconsole.PresioneParaContinuar();
                    return;

                default:
                    Console.WriteLine("Opción cancelada o no válida.");
                    _uiconsole.PresioneParaContinuar();
                    return;
            }

            _loanservice.ActualizarLoan(prestamo);

            Console.WriteLine("\n¡El estado del préstamo se actualizó con éxito en el sistema!");
            _uiconsole.PresioneParaContinuar();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nError en la validación: {ex.Message}");
            _uiconsole.PresioneParaContinuar();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\nError en la operación: {ex.Message}");
            _uiconsole.PresioneParaContinuar();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError inesperado: {ex.Message}");
            _uiconsole.PresioneParaContinuar();
        }
    }
    public void MenuUsuarios()
    {
        bool volver = false;
        do
        {
            try
            {
                _uiconsole.MostrarMenuUsuarios();
                Console.Write("\nSeleccione una opción: ");
                int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese el nombre del usuario:");
                        string nombre = Console.ReadLine().Trim();
                        Console.WriteLine("Ingrese el apellido del usuario:");
                        string apellido = Console.ReadLine().Trim();
                        Console.WriteLine("Ingrese el correo electrónico del usuario:");
                        string correo = Console.ReadLine().Trim();
                        Console.WriteLine("Ingrese el teléfono del usuario (8 dígitos):");
                        string telefono = Console.ReadLine().Trim();
                        _userservice.RegistrarUser(nombre, apellido, correo, telefono);


                        Console.WriteLine("\n¡Éxito! El perfil de usuario ha sido creado correctamente en el sistema.");
                        Console.WriteLine($"Lector: {nombre} {apellido} | Contacto: {telefono}.");
                        _uiconsole.PresioneParaContinuar();
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

                        break;
                    case 4:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("Ingrese el ID del usuario a eliminar:");
                            if (!int.TryParse(Console.ReadLine(), out int idEliminar))
                            {

                                Console.WriteLine("ID inválido.");
                                _uiconsole.PresioneParaContinuar();
                                break;
                            }

                            _userservice.EliminarUser(idEliminar);
                            Console.WriteLine($"Lista Actualizada despues de la eliminación del usuario con ID {idEliminar}.");
                            _uiconsole.MostrarUser(_userservice.ObtenerTodo());
                            _uiconsole.PresioneParaContinuar();
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
                        Console.WriteLine("Opción no válida.");
                        _uiconsole.PresioneParaContinuar();

                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError inesperado: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
        } while (!volver);
    }







    public void MenuPrestamos()
    {
        bool volver = false;
        do
        {
            try
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
                            Console.WriteLine("\n[ERROR] No existe ningún usuario registrado con ese ID.");
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        bool tieneDeudas = _loanservice.ObtenerTodo().
                            Any(p => p.UsuarioID == idUsuario && p.EstaVencido && !p.FechaDevolucionReal.HasValue);

                        if (tieneDeudas)
                        {
                            Console.WriteLine("\nTransacción denegada.");
                            Console.WriteLine($"El usuario {usuario.NombreCompleto} tiene préstamos VENCIDOS.");
                            Console.WriteLine("Debe devolver sus artículos pendientes antes de solicitar uno nuevo.");
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        Console.WriteLine("\n¿Qué tipo de artículo desea prestar?");
                        Console.WriteLine("1. Libro Físico");
                        Console.WriteLine("2. Ebook");
                        Console.Write("Opción: ");
                        int tipoOpcion = int.TryParse(Console.ReadLine(), out int to) ? to : 0;

                        Console.Write("Ingrese el ID del artículo: ");
                        if (!int.TryParse(Console.ReadLine(), out int idItem)) { Console.WriteLine("ID inválido."); break; }

                        string tipoItem = "";
                        Catalog itemAPrestar = null;

                        if (tipoOpcion == 1)
                        {
                            itemAPrestar = _bookservice.BuscarPorId(idItem);
                            tipoItem = "Libro Físico";
                        }
                        else if (tipoOpcion == 2)
                        {
                            itemAPrestar = _ebookservice.BuscarPorId(idItem);
                            tipoItem = "Ebook";
                        }
                        else
                        {
                            Console.WriteLine("\n[ERROR] Tipo de artículo no válido.");
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        if (itemAPrestar == null)
                        {
                            Console.WriteLine("\n[ERROR] No existe ningún artículo en el catálogo con ese ID para el tipo seleccionado.");
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        if (itemAPrestar.Cantidad <= 0)
                        {
                            Console.WriteLine("\n[ERROR] Transacción denegada: Stock agotado. No hay copias disponibles de este artículo.");
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        itemAPrestar.PrestarItem();

                        if (itemAPrestar is Book libro)
                            _bookservice.ActualizarBook(libro);
                        else if (itemAPrestar is Ebook ebook)
                            _ebookservice.ActualizarEbook(ebook);

                        _loanservice.RegistrarLoan(idUsuario, idItem, tipoItem, 14);

                        Console.WriteLine($"\n¡Éxito! Préstamo registrado a nombre de: {usuario.NombreCompleto}");
                        Console.WriteLine($"Artículo prestado: {itemAPrestar.Titulo}");
                        Console.WriteLine($"Stock restante: {itemAPrestar.Cantidad}");

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
                        GestionarDevolucionOEditarPrestamo();
                        break;

                    case 4:
                        volver = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        _uiconsole.PresioneParaContinuar();
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nError en la operación: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError inesperado: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
        } while (!volver);
    }

    public void MostrarMenuPrincipal()
    {
        bool salir = false;
        do
        {
            try
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
                        Console.WriteLine("Opción no válida.");
                        _uiconsole.PresioneParaContinuar();
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nError en la operación: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError inesperado: {ex.Message}");
                _uiconsole.PresioneParaContinuar();
            }
        } while (!salir);
    }
}