try
{


    IBookRepository bookRepository = new BookJsonRepository(@"Book/Book.json");
    BookService bookService = new BookService(bookRepository);

    IEbookRepository ebookRepository = new EbookJsonRepository(@"Ebook/Ebook.json");
    EbookService ebookService = new EbookService(ebookRepository);

    ILoanRepository loanRepository = new LoanJsonRepository(@"Loan/Loan.json");
    LoanService loanService = new LoanService(loanRepository);

    IUserRepository userRepository = new UserJsonRepository(@"User/User.json");
    UserService userService = new UserService(userRepository);

    UiConsole uiConsole = new UiConsole();

    Menus menu = new Menus(bookService, ebookService, loanService, userService, uiConsole);

    LibrarySystem Biblioteca = new LibrarySystem(menu);


    Biblioteca.Iniciar();



}

catch (FileNotFoundException)
{
    Console.WriteLine("El archivo no existe.");
}
catch (DirectoryNotFoundException)
{
    Console.WriteLine("La ruta especificada no es válida.");
}
catch (System.Text.Json.JsonException ex)
{
    Console.WriteLine($"[ERROR CRÍTICO] La base de datos JSON está corrupta. {ex.Message}");
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("No posee los permisos suficientes.");
}
catch (IOException ex)
{
    Console.WriteLine($"Error de acceso al archivo: {ex.Message}");
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Operación no válida: {ex.Message}");
}
Console.WriteLine();
Console.WriteLine("Presione una tecla para salir...");
Console.ReadKey();