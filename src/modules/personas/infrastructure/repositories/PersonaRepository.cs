using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.administradores.Domain.Entities;
using proyecto_cs.src.modules.personas.application.interfaces;

namespace proyecto_cs.src.modules.personas.infrastructure.repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly AppDbContext _context;

    public PersonaRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas.ToListAsync();
    }
    public async Task<Persona?> GetByIdAsync(int id)
    {
    return await _context.Personas
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public void Add(Persona persona) =>
    _context.Personas.Add(persona);
    
    public void Update(Persona persona) =>
    _context.Personas.Update(persona);
    
    public void Remove(Persona persona) =>
    _context.Personas.Remove(persona);

    public async Task SaveAsync() =>
    await _context.SaveChangesAsync();

}
