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
            string isbn;
            while (true)
            {
                Console.Write("Ingrese el ISBN del libro que desea modificar (o Enter para cancelar): ");
                isbn = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(isbn))
                {
                    Console.WriteLine("Operación cancelada.");
                    return;
                }

                if (isbn.Length != 13 || !isbn.All(char.IsDigit))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] El ISBN debe contener exactamente 13 dígitos numéricos.\n");
                    Console.ResetColor();
                }
                else break;
            }

            Book libro = _bookservice.BuscarPorISBN(isbn);

            if (libro == null)
            {
                Console.WriteLine("Libro no encontrado.");
                return;
            }

            bool salirEdicion = false;
            do
            {
                Console.Clear();
                Console.WriteLine("\nLibro actual:");
                Console.WriteLine(libro.ToString());

                Console.WriteLine("\n¿Qué dato desea actualizar?");
                Console.WriteLine("1. Título ");
                Console.WriteLine("2. Editorial ");
                Console.WriteLine("3. Género ");
                Console.WriteLine("4. Cantidad en stock ");
                Console.WriteLine("5. Regresar al menú anterior");
                Console.Write("Opción: ");

                int opcion = int.TryParse(Console.ReadLine(), out int resultado) ? resultado : 0;

                switch (opcion)
                {
                    case 1:
                        while (true)
                        {
                            Console.Write("Nuevo título: ");
                            string nuevoTitulo = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(nuevoTitulo) || nuevoTitulo.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] El título es inválido o muy corto.\n"); Console.ResetColor();
                            }
                            else { libro.ActualizarTitulo(nuevoTitulo); break; }
                        }
                        _bookservice.ActualizarBook(libro);
                        break;
                    case 2:
                        while (true)
                        {
                            Console.Write("Nueva editorial: ");
                            string nuevaEditorial = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(nuevaEditorial) || nuevaEditorial.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] La editorial es inválida o muy corta.\n"); Console.ResetColor();
                            }
                            else { libro.ActualizarEditorial(nuevaEditorial); break; }
                        }
                        _bookservice.ActualizarBook(libro);
                        break;
                    case 3:
                        while (true)
                        {
                            Console.Write("Nuevo género: ");
                            string nuevoGenero = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(nuevoGenero) || nuevoGenero.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] El género es inválido o muy corto.\n"); Console.ResetColor();
                            }
                            else { libro.ActualizarGenero(nuevoGenero); break; }
                        }
                        _bookservice.ActualizarBook(libro);
                        break;
                    case 4:
                        while (true)
                        {
                            Console.Write("Nueva cantidad en stock: ");
                            if (!int.TryParse(Console.ReadLine(), out int nuevaCantidad) || nuevaCantidad < 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] La cantidad debe ser un número entero mayor o igual a 0.\n"); Console.ResetColor();
                            }
                            else { libro.ActualizarCantidad(nuevaCantidad); break; }
                        }
                        _bookservice.ActualizarBook(libro);
                        break;
                    case 5:
                        salirEdicion = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida.");
                        Console.ResetColor();
                        _uiconsole.PresioneParaContinuar();
                        break;
                }
            } while (!salirEdicion);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nError al actualizar: {ex.Message}");
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
    public void EditarEbook()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR EBOOK ===");
            string doi;
            while (true)
            {
                Console.Write("Ingrese el DOI del Ebook que desea modificar (o Enter para cancelar): ");
                doi = (Console.ReadLine() ?? "").Trim();

                if (string.IsNullOrWhiteSpace(doi))
                {
                    Console.WriteLine("Operación cancelada.");
                    return;
                }


                if (!doi.StartsWith("10.") || !doi.Contains("/") || doi.EndsWith("/"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] DOI inválido. Formato esperado ej. 10.1000/xyz123\n");
                    Console.ResetColor();
                }
                else break;
            }
            Ebook ebook = _ebookservice.Busqueda(doi);

            if (ebook == null)
            {
                Console.WriteLine("Ebook no encontrado en el sistema.");
                Console.ReadKey();
                return;
            }

            bool salirEdicion = false;
            do
            {
                Console.Clear();
                Console.WriteLine("\nDatos actuales del Ebook:");
                Console.WriteLine(ebook.ToString());

                Console.WriteLine("\n¿Qué dato desea actualizar?");
                Console.WriteLine("1. Título");
                Console.WriteLine("2. URL de Descarga");
                Console.WriteLine("3. Idioma");
                Console.WriteLine("4. Formato (PDF/EPUB/MOBI)");
                Console.WriteLine("5. Editar stock de licencias");
                Console.WriteLine("6. Regresar al menú anterior");
                Console.Write("Opción: ");

                int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
                switch (opcion)
                {
                    case 1:
                        while (true)
                        {
                            Console.Write("Nuevo título: ");
                            string nuevoTitulo = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(nuevoTitulo) || nuevoTitulo.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] El título no puede quedar en blanco ni ser tan corto.\n"); Console.ResetColor();
                            }
                            else { ebook.ActualizarTitulo(nuevoTitulo); break; }
                        }
                        _ebookservice.ActualizarEbook(ebook);
                        break;
                    case 2:
                        while (true)
                        {
                            Console.Write("Nueva URL (http/https): ");
                            string nuevaUrl = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(nuevaUrl) || (!nuevaUrl.StartsWith("http://") && !nuevaUrl.StartsWith("https://")))
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] URL inválida. Asegúrese de incluir http:// o https://\n"); Console.ResetColor();
                            }
                            else { ebook.ActualizarURL(nuevaUrl); break; }
                        }
                        _ebookservice.ActualizarEbook(ebook);
                        break;
                    case 3:
                        while (true)
                        {
                            Console.Write("Nuevo idioma (ES/EN/FR/PT): ");
                            string nuevoIdioma = Console.ReadLine()?.Trim().ToUpper();
                            if (nuevoIdioma != "ES" && nuevoIdioma != "EN" && nuevoIdioma != "FR" && nuevoIdioma != "PT")
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] Solo se admiten los idiomas ES, EN, FR o PT.\n"); Console.ResetColor();
                            }
                            else { ebook.ActualizarIdioma(nuevoIdioma); break; }
                        }
                        _ebookservice.ActualizarEbook(ebook);
                        break;
                    case 4:
                        while (true)
                        {
                            Console.Write("Nuevo formato (PDF/EPUB/MOBI): ");
                            string nuevoFormato = Console.ReadLine()?.Trim().ToUpper();
                            if (nuevoFormato != "PDF" && nuevoFormato != "EPUB" && nuevoFormato != "MOBI")
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] Solo se admiten formatos PDF, EPUB o MOBI.\n"); Console.ResetColor();
                            }
                            else { ebook.ActualizarFormato(nuevoFormato); break; }
                        }
                        _ebookservice.ActualizarEbook(ebook);
                        break;
                    case 5:
                        while (true)
                        {
                            Console.Write("Nueva cantidad de licencias: ");
                            if (!int.TryParse(Console.ReadLine(), out int nuevaCantidad) || nuevaCantidad < 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] La cantidad debe ser un número entero mayor o igual a 0.\n"); Console.ResetColor();
                            }
                            else { ebook.ActualizarCantidad(nuevaCantidad); break; }
                        }
                        _ebookservice.ActualizarEbook(ebook);
                        break;
                    case 6:
                        salirEdicion = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida.");
                        Console.ResetColor();
                        _uiconsole.PresioneParaContinuar();
                        break;
                }
            } while (!salirEdicion);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nError al actualizar: {ex.Message}");
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
                        string filtroTitulo;
                        while (true)
                        {
                            Console.Write("Ingrese las palabras clave del título: ");
                            filtroTitulo = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(filtroTitulo))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El texto de búsqueda no puede estar vacío.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        resultados = todoElCatalogo
                            .Where(c => c.Titulo != null && c.Titulo.Contains(filtroTitulo, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        ImprimirResultadosMixtos(resultados);
                        break;
                    case 2:

                        Console.Clear();
                        string filtroAutor;
                        while (true)
                        {
                            Console.Write("Ingrese el nombre del autor: ");
                            filtroAutor = (Console.ReadLine() ?? "").Trim();
                            if (string.IsNullOrWhiteSpace(filtroAutor))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[Error] El texto de filtro no puede estar vacío");
                                Console.ResetColor();
                            }
                            else break;
                        }
                        resultados = todoElCatalogo
                            .Where(c => c.Autor != null && c.Autor.Contains(filtroAutor, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        ImprimirResultadosMixtos(resultados);
                        break;

                    case 3:
                        Console.Clear();
                        string isbnBuscado;
                        while (true)
                        {
                            Console.Write("Ingrese el ISBN de 13 dígitos: ");
                            isbnBuscado = (Console.ReadLine() ?? "").Trim();
                            if (string.IsNullOrWhiteSpace(isbnBuscado))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[Error] El texto de busqueda no puede estar vacío");
                                Console.ResetColor();
                            }
                            else break;
                        }
                        var libroFisico = _bookservice.BuscarPorISBN(isbnBuscado);
                        if (libroFisico != null) resultados.Add(libroFisico);
                        ImprimirResultadosMixtos(resultados);
                        break;

                    case 4:
                        Console.Clear();
                        string doiBuscado;
                        while (true)
                        {
                            Console.Write("Ingrese el DOI del documento electrónico: ");
                            doiBuscado = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(doiBuscado) || !doiBuscado.StartsWith("10.") || !doiBuscado.Contains("/"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[Error] DOI inválido. Formato esperado ej. 10.1000/xyz123");
                                Console.ResetColor();
                            }
                            else break;
                        }
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


    /// <summary>
    /// Recibe una lista genérica de elementos del catálogo, formatea su salida imprimiendo 
    /// sus descripciones correspondientes por consola y pausa el flujo para lectura del operador.
    /// </summary>
    /// <param name="lista">Colección heterogénea de objetos Catalog.</param>
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

    /// <summary>
    /// Administra el ciclo iterativo del submenú de libros físicos, enrutando las operaciones 
    /// de registro, listado, actualización, eliminación lógica o retorno al panel superior.
    /// </summary>
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
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== REGISTRAR NUEVO LIBRO FÍSICO ===");
                        Console.ResetColor();

                        string isbn;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el ISBN de 13 dígitos del libro (Presione Enter para autogenerar):");
                            isbn = Console.ReadLine()?.Trim();

                            if (string.IsNullOrWhiteSpace(isbn))
                            {
                                isbn = "978" + new Random().Next(100000000, 999999999).ToString() + "1";
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"[Sistema] ISBN autogenerado para pruebas: {isbn}\n");
                                Console.ResetColor();
                                break;
                            }

                            if (isbn.Length != 13 || !isbn.All(char.IsDigit) || !isbn.StartsWith("978"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El ISBN debe contener exactamente 13 dígitos numéricos .\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        if (_bookservice.BuscarPorISBN(isbn) != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[ERROR] Ya existe un libro registrado con este ISBN. Transacción abortada.");
                            Console.ResetColor();
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        string titulo;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el título del libro:");
                            titulo = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(titulo) || titulo.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El título no puede estar vacío ni ser tan corto.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        string autor;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el autor del libro:");
                            autor = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(autor) || autor.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El autor no puede estar vacío ni ser tan corto.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        string genero;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el género del libro:");
                            genero = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(genero) || genero.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El género no puede estar vacío ni ser tan corto.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        int añoPublicacion;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el año de publicación:");
                            if (!int.TryParse(Console.ReadLine(), out añoPublicacion) || añoPublicacion < 0 || añoPublicacion > DateTime.Now.Year)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] Año inválido. Ingrese un año numérico lógico (no mayor al actual).\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        int copias;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el número de copias disponibles:");
                            if (!int.TryParse(Console.ReadLine(), out copias) || copias < 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] Las copias deben ser un número mayor o igual a 0.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        int paginas;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el número de páginas:");
                            if (!int.TryParse(Console.ReadLine(), out paginas) || paginas <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El libro no puede tener páginas negativas ni en cero.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        string editorial;
                        while (true)
                        {
                            Console.WriteLine("Ingrese la editorial del libro:");
                            editorial = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(editorial) || editorial.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] La editorial no puede estar vacía ni ser tan corta.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        _bookservice.RegistrarBook(titulo, autor, genero, añoPublicacion, copias, isbn, paginas, editorial);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n¡Éxito! El libro físico ha sido registrado en el catálogo de forma segura.");
                        Console.WriteLine($"Título: {titulo} | Stock Inicial: {copias} copias.");
                        Console.ResetColor();
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
                        string isbnEliminar = string.Empty;
                        bool cancelarLibro = false;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el ISBN del libro a eliminar (o Enter para cancelar):");
                            isbnEliminar = (Console.ReadLine() ?? "").Trim();

                            if (string.IsNullOrWhiteSpace(isbnEliminar))
                            {
                                Console.WriteLine("Operación cancelada.");
                                cancelarLibro = true;
                                break;
                            }

                            if (isbnEliminar.Length != 13 || !isbnEliminar.All(char.IsDigit))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El ISBN debe contener exactamente 13 dígitos numéricos.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        if (cancelarLibro) break;

                        _bookservice.EliminarBook(isbnEliminar);
                        Console.WriteLine($"\nLista Actualizada después de la eliminación del libro con ISBN {isbnEliminar}.");
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


    /// <summary>
    /// Administra el ciclo iterativo del submenú de libros electrónicos, enrutando las operaciones 
    /// de alta, listado, edición, baja transaccional o retorno al panel superior.
    /// </summary>
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
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== REGISTRAR NUEVO EBOOK ===");
                        Console.ResetColor();

                        string doi;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el DOI (Debe empezar con '10.' y contener '/'):");
                            doi = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(doi) || !doi.StartsWith("10.") || !doi.Contains("/"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] DOI inválido. Formato esperado ej. 10.1000/xyz123\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        if (_ebookservice.Busqueda(doi) != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[ERROR] Ya existe un Ebook registrado con este DOI. Transacción abortada.");
                            Console.ResetColor();
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        string titulo;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el título del Ebook:");
                            titulo = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(titulo) || titulo.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El título no puede estar vacío y debe tener al menos 2 caracteres.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        string autor;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el autor:");
                            autor = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(autor) || autor.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] Autor inválido.\n"); Console.ResetColor();
                            }
                            else break;
                        }

                        string genero;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el género:");
                            genero = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(genero) || genero.Length < 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] Género inválido.\n"); Console.ResetColor();
                            }
                            else break;
                        }

                        int añoPublicacion;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el año de publicación:");
                            if (!int.TryParse(Console.ReadLine(), out añoPublicacion) || añoPublicacion < 0 || añoPublicacion > DateTime.Now.Year)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] Año inválido o superior al actual.\n"); Console.ResetColor();
                            }
                            else break;
                        }

                        int copias;
                        while (true)
                        {
                            Console.WriteLine("Ingrese la cantidad de licencias adquiridas:");
                            if (!int.TryParse(Console.ReadLine(), out copias) || copias < 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] Las licencias deben ser un número positivo.\n"); Console.ResetColor();
                            }
                            else break;
                        }

                        string formato;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el formato (PDF, EPUB, MOBI):");
                            formato = Console.ReadLine()?.Trim().ToUpper();
                            if (formato != "PDF" && formato != "EPUB" && formato != "MOBI")
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] Solo se admiten formatos PDF, EPUB o MOBI.\n"); Console.ResetColor();
                            }
                            else break;
                        }

                        double tamano;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el tamaño del archivo en MB (ej. 2.5):");
                            if (!double.TryParse(Console.ReadLine(), out tamano) || tamano <= 0 || tamano > 200)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El tamaño debe ser numérico y estar entre 0.1 y 200 MB.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        string urlDescarga;
                        while (true)
                        {
                            Console.WriteLine("Ingrese la URL de descarga (http:// o https://):");
                            urlDescarga = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(urlDescarga) || (!urlDescarga.StartsWith("http://") && !urlDescarga.StartsWith("https://")))
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] URL inválida. Asegúrese de incluir http:// o https://\n"); Console.ResetColor();
                            }
                            else break;
                        }

                        string idioma;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el idioma (ES, EN, FR, PT):");
                            idioma = Console.ReadLine()?.Trim().ToUpper();
                            if (idioma != "ES" && idioma != "EN" && idioma != "FR" && idioma != "PT")
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[ERROR] Solo se admiten los idiomas ES, EN, FR o PT.\n"); Console.ResetColor();
                            }
                            else break;
                        }

                        _ebookservice.RegistrarEbook(doi, titulo, autor, genero, añoPublicacion, copias, formato, tamano, urlDescarga, idioma);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n¡Éxito! El libro electrónico (Ebook) ha sido registrado y sus licencias están activas.");
                        Console.WriteLine($"Título: {titulo} | Formato: {formato.ToUpper()} | Licencias: {copias}.");
                        Console.ResetColor();
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
                        string doiEliminar;
                        bool cancelarEbook = false;
                        while (true)
                        {
                            Console.Write("Ingrese el DOI del ebook a eliminar (o Enter para cancelar): ");
                            doiEliminar = (Console.ReadLine() ?? "").Trim();

                            if (string.IsNullOrWhiteSpace(doiEliminar))
                            {
                                Console.WriteLine("Operación cancelada.");
                                cancelarEbook = true;
                                break;
                            }

                            if (!doiEliminar.StartsWith("10.") || !doiEliminar.Contains("/") || doiEliminar.EndsWith("/"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] DOI inválido. Formato esperado ej. 10.1000/xyz123\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        if (cancelarEbook) break;

                        _ebookservice.EliminarEbook(doiEliminar);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nEbook eliminado exitosamente.");
                        Console.ResetColor();
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





    /// <summary>
    /// Renderiza y gestiona las opciones de la primera jerarquía de inventarios, direccionando 
    /// el control hacia la manipulación de libros impresos, recursos digitales, visualización 
    /// consolidada o motores de búsqueda avanzados.
    /// </summary>
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


    /// <summary>
    /// Solicita de forma validada el identificador único de un usuario, busca la entidad en la base 
    /// de datos y expone un formulario interactivo para modificar de forma segura sus datos personales.
    /// </summary>
    public void EditarUsuario()
    {

        try
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR USUARIO ===");
            int idBusqueda;
            while (true)
            {
                Console.Write("Ingrese el ID del usuario que desea modificar (o Enter para cancelar): ");
                string entrada = (Console.ReadLine() ?? "").Trim();

                // Salida de emergencia
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Operación cancelada.");
                    return;
                }

                if (!int.TryParse(entrada, out idBusqueda) || idBusqueda <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] El ID debe ser un número entero positivo.\n");
                    Console.ResetColor();
                }
                else break;
            }
            User usuario = _userservice.BuscarPorId(idBusqueda);

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
                    while (true)
                    {
                        Console.Write("Nuevo nombre: ");
                        string nuevoNombre = Console.ReadLine()?.Trim();
                        if (string.IsNullOrWhiteSpace(nuevoNombre) || nuevoNombre.Length < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("[ERROR] El nombre no puede estar vacío.\n");
                            Console.ResetColor();
                        }
                        else { usuario.ActualizarNombre(nuevoNombre); break; }
                    }
                    break;
                case 2:
                    while (true)
                    {
                        Console.Write("Nuevo apellido: ");
                        string nuevoApellido = Console.ReadLine()?.Trim();
                        if (string.IsNullOrWhiteSpace(nuevoApellido) || nuevoApellido.Length < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("[ERROR] El apellido no puede estar vacío.\n");
                            Console.ResetColor();
                        }
                        else { usuario.ActualizarApellido(nuevoApellido); break; }
                    }
                    break;
                case 3:
                    while (true)
                    {
                        Console.Write("Nuevo correo: ");
                        string nuevoCorreo = Console.ReadLine()?.Trim();
                        if (string.IsNullOrWhiteSpace(nuevoCorreo) || !nuevoCorreo.Contains("@") || !nuevoCorreo.Contains("."))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("[ERROR] El correo debe contener '@' y '.'. Intente de nuevo.\n");
                            Console.ResetColor();
                        }
                        else { usuario.ActualizarCorreo(nuevoCorreo); break; }
                    }
                    break;
                case 4:
                    while (true)
                    {
                        Console.Write("Nuevo teléfono (8 dígitos): ");
                        string nuevoTelefono = Console.ReadLine()?.Trim();
                        bool esValido = !string.IsNullOrWhiteSpace(nuevoTelefono) &&
                                        nuevoTelefono.Length == 8 &&
                                        nuevoTelefono.All(char.IsDigit) &&
                                        "578".Contains(nuevoTelefono[0]);

                        if (!esValido)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("[ERROR] Teléfono inválido. Debe tener 8 números y empezar con 5, 7 u 8.\n");
                            Console.ResetColor();
                        }
                        else { usuario.ActualizarTelefono(nuevoTelefono); break; }
                    }
                    break;
                case 5:
                    Console.WriteLine("Cancelando operación");
                    _uiconsole.PresioneParaContinuar();
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción no válida.");
                    Console.ResetColor();
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


    /// <summary>
    /// Intercepta la transacción de un préstamo en curso para formalizar su devolución y actualizar 
    /// el stock de copias físicas o licencias digitales, o en su defecto, permite editar y añadir 
    /// observaciones sobre el estado del registro.
    /// </summary>
    public void GestionarDevolucionOEditarPrestamo()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE PRÉSTAMO / DEVOLUCIÓN ===");

            int id;
            while (true)
            {
                Console.Write("Ingrese el ID del préstamo: ");
                if (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] ID inválido. Debe ingresar un número mayor a 0.\n");
                    Console.ResetColor();
                }
                else break;
            }

            Loan prestamo = _loanservice.BuscarPorId(id);

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
                    string nuevasObservaciones;
                    while (true)
                    {
                        Console.Write("Nuevas observaciones generales: ");
                        nuevasObservaciones = Console.ReadLine()?.Trim();
                        if (string.IsNullOrWhiteSpace(nuevasObservaciones) || nuevasObservaciones.Length < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("[ERROR] Las observaciones no pueden estar vacías ni ser tan cortas.\n");
                            Console.ResetColor();
                        }
                        else break;
                    }
                    prestamo.ActualizarObservaciones(nuevasObservaciones);
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
    /// <summary>
    /// Orquesta el módulo administrativo para lectores de la biblioteca, dirigiendo al operador hacia 
    /// los formularios de alta, listados de auditoría, paneles de actualización, eliminación o motores 
    /// de consulta locales.
    /// </summary>
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
                        string nombre;
                        while (true)
                        {

                            Console.WriteLine("Ingrese el nombre del usuario:");
                            nombre = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(nombre))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[Error] El nombre no puede estar vacío");
                                Console.ResetColor();
                            }
                            else break;
                        }
                        string apellido;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el apellido del usuario:");
                            apellido = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(apellido))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[Error] El apellido no puede estar vacío");
                                Console.ResetColor();
                            }
                            else break;
                        }


                        string correo;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el correo electrónico del usuario:");
                            correo = (Console.ReadLine() ?? "").Trim();

                            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains("@") || !correo.Contains("."))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El correo debe contener '@' y '.'. Intente de nuevo.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        string telefono;
                        while (true)
                        {
                            Console.WriteLine("Ingrese el teléfono del usuario (8 dígitos):");
                            telefono = (Console.ReadLine() ?? "").Trim();

                            bool esValido = !string.IsNullOrWhiteSpace(telefono) &&
                                            telefono.Length == 8 &&
                                            telefono.All(char.IsDigit) &&
                                            "578".Contains(telefono[0]);

                            if (!esValido)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] Teléfono inválido. Debe tener 8 números y empezar con 5, 7 u 8.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        _userservice.RegistrarUser(nombre, apellido, correo, telefono);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n¡Éxito! El perfil de usuario ha sido creado correctamente en el sistema.");
                        Console.WriteLine($"Lector: {nombre} {apellido} | Contacto: {telefono}.");
                        Console.ResetColor();
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
                        int idEliminar = 0;
                        bool cancelarUsuario = false;
                        while (true)
                        {
                            Console.Write("Ingrese el ID del usuario a eliminar (o Enter para cancelar): ");
                            string entradaEliminar = (Console.ReadLine() ?? "").Trim();

                            if (string.IsNullOrWhiteSpace(entradaEliminar))
                            {
                                Console.WriteLine("Operación cancelada.");
                                cancelarUsuario = true;
                                break;
                            }

                            if (!int.TryParse(entradaEliminar, out idEliminar) || idEliminar <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] El ID debe ser un número entero positivo.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }
                        if (cancelarUsuario)
                        { break; }

                        _userservice.EliminarUser(idEliminar);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nUsuario eliminado exitosamente.");
                        Console.ResetColor();
                        _uiconsole.PresioneParaContinuar();
                        break;
                    case 5:
                        MenuConsultasUsuarios();
                        break;
                    case 6:
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





    /// <summary>
    /// Orquesta el módulo transaccional de la biblioteca, validando de forma previa restricciones 
    /// por morosidad y enrutando las peticiones de nuevos préstamos, listados de tránsito, 
    /// devoluciones o filtros avanzados.
    /// </summary>
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
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== REGISTRAR NUEVO PRÉSTAMO ===");
                        Console.ResetColor();

                        int idUsuario;
                        while (true)
                        {
                            Console.Write("Ingrese el ID del Usuario: ");
                            if (!int.TryParse(Console.ReadLine(), out idUsuario) || idUsuario <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] ID inválido. Debe ser numérico y mayor a 0.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        var usuario = _userservice.BuscarPorId(idUsuario);
                        if (usuario == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[ERROR] No existe ningún usuario registrado con ese ID.");
                            Console.ResetColor();
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        bool tieneDeudas = _loanservice.ObtenerTodo()
                            .Any(p => p.UsuarioID == idUsuario && p.EstaVencido && !p.FechaDevolucionReal.HasValue);

                        if (tieneDeudas)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[SISTEMA BLOQUEADO] Transacción denegada.");
                            Console.WriteLine($"El usuario {usuario.NombreCompleto} tiene préstamos VENCIDOS pendientes de entrega.");
                            Console.WriteLine("Debe devolver sus artículos atrasados antes de poder solicitar un nuevo préstamo.");
                            Console.ResetColor();
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        int prestamosActivos = _loanservice.ObtenerTodo()
                            .Count(p => p.UsuarioID == idUsuario && !p.FechaDevolucionReal.HasValue);
                        if (prestamosActivos >= 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\n[SISTEMA BLOQUEADO] El usuario ya tiene {prestamosActivos} préstamos activos.");
                            Console.WriteLine("Ha alcanzado el límite máximo permitido por la biblioteca.");
                            Console.ResetColor();
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }


                        int tipoOpcion;
                        while (true)
                        {
                            Console.WriteLine("\n¿Qué tipo de artículo desea prestar?");
                            Console.WriteLine("1. Libro Físico");
                            Console.WriteLine("2. Ebook");
                            Console.Write("Opción: ");
                            if (!int.TryParse(Console.ReadLine(), out tipoOpcion) || (tipoOpcion != 1 && tipoOpcion != 2))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] Opción inválida. Seleccione 1 o 2.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }

                        int idItem;
                        while (true)
                        {
                            Console.Write("Ingrese el ID del artículo: ");
                            if (!int.TryParse(Console.ReadLine(), out idItem) || idItem <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] ID inválido. Debe ser numérico y mayor a 0.\n");
                                Console.ResetColor();
                            }
                            else break;
                        }
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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[ERROR] Tipo de artículo no válido.");
                            Console.ResetColor();
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        if (itemAPrestar == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[ERROR] No existe ningún artículo en el catálogo con ese ID para el tipo seleccionado.");
                            Console.ResetColor();
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        if (itemAPrestar.Cantidad <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[ERROR] Transacción denegada: Stock agotado. No hay copias disponibles de este artículo.");
                            Console.ResetColor();
                            _uiconsole.PresioneParaContinuar();
                            break;
                        }

                        itemAPrestar.PrestarItem();

                        if (itemAPrestar is Book libro)
                            _bookservice.ActualizarBook(libro);
                        else if (itemAPrestar is Ebook ebook)
                            _ebookservice.ActualizarEbook(ebook);

                        _loanservice.RegistrarLoan(idUsuario, idItem, tipoItem, 14);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n¡Éxito! Préstamo registrado a nombre de: {usuario.NombreCompleto}");
                        Console.WriteLine($"Artículo prestado: {itemAPrestar.Titulo}");
                        Console.WriteLine($"Stock restante: {itemAPrestar.Cantidad}");
                        Console.ResetColor();

                        _uiconsole.PresioneParaContinuar();
                        break;

                    case 2:
                        Console.Clear();
                        var prestamos = _loanservice.ObtenerTodo();
                        if (prestamos.Count == 0) Console.WriteLine("No hay préstamos registrados.");
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("=== LISTA DE PRÉSTAMOS ===");
                            Console.ResetColor();
                            foreach (var p in prestamos) { Console.WriteLine(p.ToString()); Console.WriteLine(); }
                        }
                        _uiconsole.PresioneParaContinuar();
                        break;

                    case 3:
                        GestionarDevolucionOEditarPrestamo();
                        break;

                    case 4:
                        MenuConsultasPrestamos();
                        break;
                    case 5:
                        volver = true;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida.");
                        Console.ResetColor();
                        _uiconsole.PresioneParaContinuar();
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {ex.Message}");
                Console.ResetColor();
                _uiconsole.PresioneParaContinuar();
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError en la operación: {ex.Message}");
                Console.ResetColor();
                _uiconsole.PresioneParaContinuar();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError inesperado: {ex.Message}");
                Console.ResetColor();
                _uiconsole.PresioneParaContinuar();
            }
        } while (!volver);
    }

    /// <summary>
    /// Despliega el submenú interactivo para filtrar usuarios por coincidencia de texto (nombre o apellido) 
    /// o visualizar el listado completo ordenado alfabéticamente de forma ascendente.
    /// </summary>
    public void MenuConsultasUsuarios()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== CONSULTAS Y ORDENAMIENTO DE USUARIOS ===");
        Console.ResetColor();
        Console.WriteLine("1. Buscar usuario por Nombre o Apellido");
        Console.WriteLine("2. Ver todos los usuarios ordenados alfabéticamente (A-Z)");
        Console.WriteLine("3. Volver");
        Console.Write("\nOpción: ");

        int opcion = int.TryParse(Console.ReadLine(), out int r) ? r : 0;

        if (opcion == 1)
        {
            string termino;
            while (true)
            {
                Console.Write("Ingrese el nombre o apellido a buscar: ");
                termino = (Console.ReadLine() ?? "").Trim();
                if (string.IsNullOrWhiteSpace(termino))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] Debe ingresar un texto para buscar.\n");
                    Console.ResetColor();
                }
                else break;
            }
            var resultados = _userservice.FiltrarUsuarios(u =>
                (u.Nombre != null && u.Nombre.ToLower().Contains(termino)) ||
                (u.Apellido != null && u.Apellido.ToLower().Contains(termino)));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n--- Resultados Encontrados: {resultados.Count} ---");
            Console.ResetColor();
            _uiconsole.MostrarUser(resultados);
            _uiconsole.PresioneParaContinuar();
        }
        else if (opcion == 2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- Usuarios Ordenados (A-Z) ---");
            Console.ResetColor();
            var ordenados = _userservice.OrdenarUsuarios(u => u.Nombre);
            _uiconsole.MostrarUser(ordenados);
            _uiconsole.PresioneParaContinuar();
        }
    }

    /// <summary>
    /// Despliega el submenú de reportes transaccionales permitiendo consultar el historial de 
    /// solicitudes de un usuario específico mediante su ID o listar todos los préstamos ordenados 
    /// por su fecha próxima de vencimiento.
    /// </summary>
    public void MenuConsultasPrestamos()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== CONSULTAS DE PRÉSTAMOS ===");
        Console.ResetColor();
        Console.WriteLine("1. Buscar el historial completo de un Usuario específico (Por ID)");
        Console.WriteLine("2. Ver todos los préstamos ordenados por Fecha de Vencimiento (Más próximos primero)");
        Console.WriteLine("3. Volver");
        Console.Write("\nOpción: ");

        int opcion = int.TryParse(Console.ReadLine(), out int r) ? r : 0;

        if (opcion == 1)
        {
            int idBuscado;
            while (true)
            {
                Console.Write("Ingrese el ID del Usuario: ");
                if (!int.TryParse(Console.ReadLine(), out idBuscado) || idBuscado <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] ID inválido. Debe ser numérico y mayor a 0.\n");
                    Console.ResetColor();
                }
                else break;
            }

            var historial = _loanservice.FiltrarPrestamos(p => p.UsuarioID == idBuscado);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n--- Historial del Usuario ID {idBuscado}: {historial.Count} registros ---");
            Console.ResetColor();
            _uiconsole.MostrarPrestamos(historial);

            _uiconsole.PresioneParaContinuar();
        }
        else if (opcion == 2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- Préstamos ordenados por Fecha de Vencimiento ---");
            Console.ResetColor();
            var ordenados = _loanservice.OrdenarPrestamos(p => p.FechaDevolucionEsperada);
            _uiconsole.MostrarPrestamos(ordenados);
            _uiconsole.PresioneParaContinuar();
        }
    }

    /// <summary>
    /// Controla el ciclo principal de la aplicación, invocando la renderización de la interfaz 
    /// de usuario y enrutando el flujo de navegación hacia la gestión de catálogos, perfiles, 
    /// transacciones, reportes o la salida segura del programa.
    /// </summary>
    public void MostrarMenuPrincipal()
    {
        bool salir = false;
        do
        {
            try
            {
                _uiconsole.MostrarMenuPrincipal(
                _bookservice.MostrarTotalDeLibros(),
                _ebookservice.MostrarTotalDeEbooks(),
                _userservice.MostrarTotalDeUsuarios(),
                _loanservice.MostrarTotalDePrestamos()
                );
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
                        ConsultarTodo();
                        break;
                    case 5:
                        salir = true;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Saliendo del sistema...");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida.");
                        Console.ResetColor();
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

    /// <summary>
    /// Despacha un submenú de auditoría y reportes consolidados, permitiendo visualizar bajo 
    /// demanda el inventario mixto completo, las transacciones vigentes o los registros vencidos 
    /// (morosos) filtrados mediante expresiones LINQ.
    /// </summary>
    public void ConsultarTodo()
    {
        bool volver = false;
        do
        {
            try
            {
                _uiconsole.MenuConsultasGlobales();
                Console.Write("\nSeleccione una opción: ");
                int opcion = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

                switch (opcion)
                {
                    case 1:
                        // Reutiliza tu método mixto que ya imprime ambos catálogos
                        MostrarCatalogoCompleto();
                        _uiconsole.PresioneParaContinuar();
                        break;

                    case 2:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("═══ LISTADO DE PRÉSTAMOS ACTIVOS (VIGENTES) ═══");
                        Console.ResetColor();

                        // Filtramos préstamos que NO se han devuelto y NO están vencidos
                        var activos = _loanservice.ObtenerTodo()
                            .Where(p => !p.FechaDevolucionReal.HasValue && !p.EstaVencido)
                            .ToList();

                        if (activos.Count == 0)
                        {
                            Console.WriteLine("(No se registran préstamos activos en tiempo en este momento)");
                        }
                        else
                        {
                            foreach (var p in activos)
                            {
                                Console.WriteLine(p.ToString());
                                Console.WriteLine();
                            }
                        }
                        _uiconsole.PresioneParaContinuar();
                        break;

                    case 3:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("═══ LISTADO DE PRÉSTAMOS VENCIDOS (MOROSOS) ═══");
                        Console.ResetColor();

                        // Filtramos préstamos que están vencidos y NO se han devuelto
                        var vencidos = _loanservice.ObtenerTodo()
                            .Where(p => p.EstaVencido && !p.FechaDevolucionReal.HasValue)
                            .ToList();

                        if (vencidos.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("(¡Excelente! No hay registros de préstamos vencidos en el sistema)");
                            Console.ResetColor();
                        }
                        else
                        {
                            foreach (var p in vencidos)
                            {
                                Console.WriteLine(p.ToString());
                                Console.WriteLine();
                            }
                        }
                        _uiconsole.PresioneParaContinuar();
                        break;

                    case 4:
                        volver = true;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida.");
                        Console.ResetColor();
                        _uiconsole.PresioneParaContinuar();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError en el módulo de reportes: {ex.Message}");
                Console.ResetColor();
                _uiconsole.PresioneParaContinuar();
            }
        } while (!volver);
    }
}