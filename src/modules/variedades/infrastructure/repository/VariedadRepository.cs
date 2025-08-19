using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.variedades.application.interfaces;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs.src.modules.variedades.infrastructure.repository;

public class VariedadRepository : IVariedadRepository
{
    private readonly AppDbContext _context;
    public VariedadRepository(AppDbContext context) => _context = context;
  // se agrega pero luego toca guardar los cambios manuamente con el metodo SaveChanges
    public void Add(Variedad variedad) => _context.Variedades.Add(variedad);
    public void Update(Variedad entity) => _context.SaveChanges();
    public void Remove(Variedad entity) => _context.Variedades.Remove(entity);
    public async Task<IEnumerable<Variedad?>> GetAllAsync() => await _context.Variedades.ToListAsync();
    public async Task<Variedad?> GetByIdAsync(int id) => await _context.Variedades.FirstOrDefaultAsync(v => v.IdVariedad == id);
    public async Task<Variedad?> GetByNameAsync(string nombre) => await _context.Variedades.FirstOrDefaultAsync(v => v.NombreComun == nombre);
    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
