using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.administradores.Application.Interfaces;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.administradores.Infrastructure.Repositories;
public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Admin?> GetAdminByIdAsync(int id)
    {
    return await _context.Admins
        .FirstOrDefaultAsync(a => a.id == id);
    }

    public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
    {
        return await _context.Admins.ToListAsync();
    }

    public void Add(Admin admin)
    {
        _context.Admins.Add(admin);
    }

    public void Update(Admin admin)
    {
        var existingAdmin = _context.Admins.FirstOrDefault(a => a.id == admin.id);
        if (existingAdmin != null)
        {
            existingAdmin.email = admin.email;
            existingAdmin.password_hash = admin.password_hash;
            // Update other properties as needed
        }
    }

    public void Delete(Admin admin)
    {
        _context.Admins.Remove(admin);
    }

    public Task SaveAsync()
    {
        // In a real application, this would save changes to the database
        return Task.CompletedTask;
    }

    public Task<Admin?> GetByEmailAsync(string email)
    {
        return Task.FromResult(_context.Admins.FirstOrDefault(a => a.email == email));
    }

    public Task<Admin?> GetByEmailAndPasswordAsync(string email, string password_hash)
    {
        return Task.FromResult(_context.Admins.FirstOrDefault(a => a.email == email && a.password_hash == password_hash));
    }    
}
