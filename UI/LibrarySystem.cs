/// <summary>
/// Clase de orquestación inicial del sistema de biblioteca.
/// </summary>
public class LibrarySystem
{
    private readonly Menus _menus;

    /// <summary>
    /// Inicializa el sistema inyectando la capa de menús.
    /// </summary>
    /// <param name="menus">Instancia de la clase Menus.</param>
    public LibrarySystem(Menus menus)
    {
        _menus = menus;
    }

    /// <summary>
    /// Pone en marcha la aplicación llamando al menú principal.
    /// </summary>
    public void Iniciar()
    {
        _menus.MostrarMenuPrincipal();
    }
}