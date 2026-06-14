
public class LibrarySystem
{
    private readonly BookService _bookservice;
    private readonly EbookService _ebookservice;
    private readonly LoanService _loanservice;
    private readonly UserService _userservice;
    private readonly Menus _menus;

    public LibrarySystem(BookService bookservice, EbookService ebookservice, LoanService loanservice, UserService userservice, Menus menus)
    {
        _bookservice = bookservice;
        _ebookservice = ebookservice;
        _loanservice = loanservice;
        _userservice = userservice;
        _menus = menus;
    }

    public void Iniciar()
    {
        _menus.MostrarMenuPrincipal();

    }
}
