
public interface IUsuarioRepository
{
    void GuardarTodos(List<Usuario> usuarios);
    List<Usuario> ObtenerTodos();
}
