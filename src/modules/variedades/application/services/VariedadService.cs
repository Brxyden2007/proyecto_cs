using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.variedades.application.interfaces;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs.src.modules.variedades.application.services;
public class VariedadService : IVariedadService
{
    private readonly AppDbContext _context;

    public VariedadService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Variedad> CrearVariedadAsync(Variedad variedad)
    {
        _context.Variedades.Add(variedad);
        await _context.SaveChangesAsync();
        return variedad;
    }

    public async Task<Variedad?> ObtenerVariedadPorIdAsync(int id)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Include(v => v.CalidadAltitud)
            .Include(v => v.HistoriasGeneticas)
            .Include(v => v.AtributosAgronomicos)
            .Include(v => v.VariedadResistencias)
                .ThenInclude(vr => vr.Resistencia)
            .FirstOrDefaultAsync(v => v.IdVariedad == id);
    }

    public async Task<IEnumerable<Variedad>> ObtenerTodasLasVariedadesAsync()
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Include(v => v.CalidadAltitud)
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> ObtenerVariedadesPaginadasAsync(int pagina, int tamanoPagina)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Include(v => v.CalidadAltitud)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync();
    }

    public async Task<Variedad> ActualizarVariedadAsync(Variedad variedad)
    {
        _context.Variedades.Update(variedad);
        await _context.SaveChangesAsync();
        return variedad;
    }

    public async Task<bool> EliminarVariedadAsync(int id)
    {
        var variedad = await _context.Variedades.FindAsync(id);
        if (variedad == null) return false;

        _context.Variedades.Remove(variedad);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorNombreAsync(string nombre)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Where(v => v.NombreComun.Contains(nombre))
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorNombreCientificoAsync(string nombreCientifico)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Where(v => v.NombreCientifico.Contains(nombreCientifico))
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorPorteAsync(int idPorte)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Where(v => v.IdPorte == idPorte)
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorTamanioGranoAsync(int idTamanio)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Where(v => v.IdTamanio == idTamanio)
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorAltitudAsync(int idAltitud)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Where(v => v.IdAltitud == idAltitud)
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorRendimientoAsync(int idRendimiento)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Where(v => v.IdRendimiento == idRendimiento)
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorResistenciaAsync(int idResistencia)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Include(v => v.VariedadResistencias)
            .Where(v => v.VariedadResistencias.Any(vr => vr.IdResistencia == idResistencia))
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorTipoVariedadAsync(string tipo)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Where(v => v.Descripcion.Contains(tipo))
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorAtributoAgronomicoAsync(int idAtributo)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Include(v => v.AtributosAgronomicos)
            .Where(v => v.AtributosAgronomicos.Any(aa => aa.IdAtributo == idAtributo))
            .ToListAsync();
    }

    public async Task<IEnumerable<Variedad>> FiltrarPorHistoriaGeneticaAsync(int idHistoria)
    {
        return await _context.Variedades
            .Include(v => v.Porte)
            .Include(v => v.TamanioGrano)
            .Include(v => v.Altitud)
            .Include(v => v.Rendimiento)
            .Include(v => v.HistoriasGeneticas)
            .Where(v => v.HistoriasGeneticas.Any(hg => hg.IdHistoria == idHistoria))
            .ToListAsync();
    }
}
