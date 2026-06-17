public class LibrarySystem
{
    private readonly Menus _menus;

    public LibrarySystem(Menus menus)
    {
        _menus = menus;
    }

    public void Iniciar()
    {
        _menus.MostrarMenuPrincipal();
    }
}