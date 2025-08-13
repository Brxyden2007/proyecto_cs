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

    public AdministradorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Administrador?> GetAdministradorByIdAsync(int id)
    {
    return await _context.Administradors
        .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Administrador>> GetAllAdministradorsAsync()
    {
        return await _context.Administradors.ToListAsync();
    }

    public void Add(Administrador administrador)
    {
        _context.Administradors.Add(administrador);
    }

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

    public void Delete(Administrador administrador)
    {
        _context.Administradors.Remove(administrador);
    }

    public Task SaveAsync()
    {
        // In a real application, this would save changes to the database
        return Task.CompletedTask;
    }

    public Task<Administrador?> GetByEmailAsync(string email)
    {
        return Task.FromResult(_context.Administradors.FirstOrDefault(a => a.Email == email));
    }

    public Task<Administrador?> GetByEmailAndPasswordAsync(string email, string password_hash)
    {
        return Task.FromResult(_context.Administradors.FirstOrDefault(a => a.Email == email && a.PasswordHash == password_hash));
    }    
}
