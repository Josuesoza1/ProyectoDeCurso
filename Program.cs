try
{


    IBookRepository bookRepository = new BookJsonRepository("Book.json");
    BookService service = new BookService(bookRepository);

    IEbookRepository ebookRepository = new EbookJsonRepository("Ebook.json");
    EbookService service1 = new EbookService(ebookRepository);

    ILoanRepository loanRepository = new LoanJsonRepository("Loan.json");
    LoanService service2 = new LoanService(loanRepository);

    IUserRepository userRepository = new UserJsonRepository("User.json");
    UserService service3 = new UserService(userRepository);

    UiConsole uiConsole = new UiConsole();

    Menus menu = new Menus(service, service1, service2, service3, uiConsole);
    LibrarySystem prueba = new LibrarySystem(service, service1, service2, service3, menu);

    prueba.Iniciar();



}

catch (FileNotFoundException)
{
    Console.WriteLine("El archivo no existe.");
}
catch (DirectoryNotFoundException)
{
    Console.WriteLine("La ruta especificada no es válida.");
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
Console.WriteLine();
Console.WriteLine("Presione una tecla para salir...");
Console.ReadKey();