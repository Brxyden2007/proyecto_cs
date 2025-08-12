using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public interface IUsuarioService
{
    Task RegistrarUsuarioAsync(Usuario usuario);
    Task ActualizarUsuarioAsync(Usuario usuario);
    Task EliminarUsuarioAsync(int id);
    Task<Usuario?> ObtenerUsuarioPorIdAsync(int id);
    Task<IEnumerable<Usuario>> ObtenerTodosLosUsuariosAsync();
}
