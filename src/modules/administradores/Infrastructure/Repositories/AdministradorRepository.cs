using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.administradores.Application.Interfaces;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.administradores.Infrastructure.Repositories;
public class AdministradorRepository : IAdministradorRepository
{
    private readonly AppDbContext _context;
    // genera el constructor con el contexto de la base de datos
    public AdministradorRepository(AppDbContext context) => _context = context;
    // genera el repositorio para obtener un administrador por id
    public async Task<Administrador?> GetAdministradorByIdAsync(int id) => await _context.Administradors.FirstOrDefaultAsync(a => a.Id == id);
    // obtiene todos los administradores
    public async Task<IEnumerable<Administrador>> GetAllAdministradorsAsync() => await _context.Administradors.ToListAsync();
    public void Add(Administrador administrador) =>_context.Administradors.Add(administrador);

    public void Update(Administrador administrador)
    {
        var existingAdministrador = _context.Administradors.FirstOrDefault(a => a.Id == administrador.Id);
        if (existingAdministrador != null)
        {
            existingAdministrador.Email = administrador.Email;
            existingAdministrador.PasswordHash = administrador.PasswordHash;  
            // Update other properties as needed
        }
    }
    public void Delete(Administrador administrador) => _context.Administradors.Remove(administrador);
    public Task SaveAsync() => Task.CompletedTask;
    public Task<Administrador?> GetByEmailAsync(string email) => Task.FromResult(_context.Administradors.FirstOrDefault(a => a.Email == email));
    public Task<Administrador?> GetByEmailAndPasswordAsync(string email, string password_hash) => Task.FromResult(_context.Administradors.FirstOrDefault(a => a.Email == email && a.PasswordHash == password_hash));
}
