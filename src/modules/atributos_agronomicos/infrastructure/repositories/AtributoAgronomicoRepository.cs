using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.atributos_agronomicos.application.interfaces;
using proyecto_cs.src.modules.atributos_agronomicos.domain.models;
using proyecto_cs.src.modules.variedades.application.interfaces;

namespace proyecto_cs.src.modules.atributos_agronomicos.infrastructure.repositories;
public class AtributoAgronomicoRepository : IAtributoAgronomicoRepository
{
    private readonly AppDbContext _context;
    public AtributoAgronomicoRepository(AppDbContext context) => _context = context;
  // se agrega pero luego toca guardar los cambios manuamente con el metodo SaveChanges
    public void Add(AtributoAgronomico atributoAgronomico) => _context.AtributosAgronomicos.Add(atributoAgronomico);
    public void Update(AtributoAgronomico entity) => _context.SaveChanges();
    public void Remove(AtributoAgronomico entity) => _context.AtributosAgronomicos.Remove(entity);
    public async Task<IEnumerable<AtributoAgronomico?>> GetAllAsync() => await _context.AtributosAgronomicos.ToListAsync();
    public async Task<AtributoAgronomico?> GetByIdAsync(int id) => await _context.AtributosAgronomicos.FirstOrDefaultAsync(aa => aa.IdAtributo == id);
    public async Task<AtributoAgronomico?> GetByTiempoCosechaAsync(string tiempoCosecha) => await _context.AtributosAgronomicos.FirstOrDefaultAsync(aa => aa.TiempoCosecha == tiempoCosecha);
    public async Task<AtributoAgronomico?> GetByMaduracionAsync(string maduracion) => await _context.AtributosAgronomicos.FirstOrDefaultAsync(aa => aa.Maduracion == maduracion);
    public async Task<AtributoAgronomico?> GetByNutricionAsync(string nutricion) => await _context.AtributosAgronomicos.FirstOrDefaultAsync(aa => aa.Nutricion == nutricion);
    public async Task<AtributoAgronomico?> GetByDensidadSiembraAsync(string densidadSiembra) => await _context.AtributosAgronomicos.FirstOrDefaultAsync(aa => aa.DensidadSiembra == densidadSiembra);
    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
