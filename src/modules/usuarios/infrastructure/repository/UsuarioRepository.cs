using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.usuarios.domain.models;

namespace proyecto_cs.src.modules.usuarios.infrastructure.repository;
public class UsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    // private readonly List<Usuario> _usuarios = new List<Usuario>();

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios
        .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Usuario>>GetAllAsync()
    {
       return await _context.Usuarios.ToListAsync();
    }

    public void Add(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
    }

    public void Update(Usuario usuario)
    {
        /*var existing = _context.Usuarios.FirstOrDefault(u => u.id == usuario.id);
        if (existing != null)
        {
            existing.nombre = usuario.nombre;
            existing.email = usuario.email;
            existing.password_hash = usuario.password_hash;
            existing.created_at = usuario.created_at;
        }*/
        _context.Usuarios.Update(usuario);
    }

    public void Delete(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }  
}
