using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.historias_geneticas.application.interfaces;
using proyecto_cs.src.modules.historias_geneticas.domain.models;

namespace proyecto_cs.src.modules.historias_geneticas.infrastructure.repositories;
public class HistoriaGeneticaRepository : IHistoriaGeneticaRepository
{
    private readonly AppDbContext _context;
    public HistoriaGeneticaRepository(AppDbContext context) => _context = context;
  // se agrega pero luego toca guardar los cambios manuamente con el metodo SaveChanges
    public void Add(HistoriaGenetica historiaGenetica) => _context.HistoriasGeneticas.Add(historiaGenetica);
    public void Update(HistoriaGenetica entity) => _context.SaveChanges();
    public void Remove(HistoriaGenetica entity) => _context.HistoriasGeneticas.Remove(entity);
    public async Task<IEnumerable<HistoriaGenetica?>> GetAllAsync() => await _context.HistoriasGeneticas.ToListAsync();
    public async Task<HistoriaGenetica?> GetByIdAsync(int id) => await _context.HistoriasGeneticas.FirstOrDefaultAsync(hg => hg.IdHistoria == id);
    public async Task<HistoriaGenetica?> GetByObtentorAsync(string obtentor) => await _context.HistoriasGeneticas.FirstOrDefaultAsync(hg => hg.Obtentor == obtentor);
    public async Task<HistoriaGenetica?> GetByFamiliaAsync(string familia) => await _context.HistoriasGeneticas.FirstOrDefaultAsync(hg => hg.Familia == familia);
    public async Task<HistoriaGenetica?> GetByGrupoAsync(string grupo) => await _context.HistoriasGeneticas.FirstOrDefaultAsync(hg => hg.Grupo == grupo);
    public async Task<HistoriaGenetica?> GetByDescripcionAsync(string descripcion) => await _context.HistoriasGeneticas.FirstOrDefaultAsync(hg => hg.Descripcion == descripcion);        
    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
