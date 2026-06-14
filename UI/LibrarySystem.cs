
public class LibrarySystem
{
    private readonly BookService _bookservice;
    private readonly EbookService _ebookservice;
    private readonly LoanService _loanservice;
    private readonly UserService _userservice;

    public LibrarySystem(BookService bookservice, EbookService ebookservice, LoanService loanservice, UserService userservice)
    {
        _bookservice = bookservice;
        _ebookservice = ebookservice;
        _loanservice = loanservice;
        _userservice = userservice;
    }

    public void Iniciar()
    {
        _bookservice.RegistrarBook(1,"Juan", "juan","Juan", 2015, 5 , "1234567891011",400,"Micasa");

    }
}
