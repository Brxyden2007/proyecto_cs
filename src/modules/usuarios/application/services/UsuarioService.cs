using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class UsuarioService
{
    private readonly IUsuarioRepository _repo;

    public UsuarioService(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task RegistrarUsuarioAsync(Usuario usuario)
    {
        _repo.Add(usuario);
        await _repo.SaveAsync();
    }

    public async Task ActualizarUsuarioAsync(Usuario usuario)
    {
        _repo.Update(usuario);
        await _repo.SaveAsync();
    }

    public async Task EliminarUsuarioAsync(int id)
    {
        var usuario = await _repo.GetByIdAsync(id);
        if (usuario != null)
        {
            _repo.Delete(usuario);
            await _repo.SaveAsync();
        }
    }

    public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodosLosUsuariosAsync()
    {
        return await _repo.GetAllAsync();
    }
}