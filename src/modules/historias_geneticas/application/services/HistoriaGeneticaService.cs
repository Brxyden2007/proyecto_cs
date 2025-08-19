using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.historias_geneticas.application.interfaces;
using proyecto_cs.src.modules.historias_geneticas.domain.models;

namespace proyecto_cs.src.modules.historias_geneticas.application.services;

public class HistoriaGeneticaService : IHistoriaGeneticaService
{
    private readonly IHistoriaGeneticaRepository _repository;
    public HistoriaGeneticaService(IHistoriaGeneticaRepository repository) => _repository = repository;
    public async Task<HistoriaGenetica?> CrearHistoriaGeneticaAsync(HistoriaGenetica historiaGenetica)
    {
        _repository.Add(historiaGenetica);
        await _repository.SaveAsync();
        return historiaGenetica;
    }

    public async Task<HistoriaGenetica?> ObtenerHistoriaGeneticaPorIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<HistoriaGenetica?>> ObtenerTodasLasHistoriasGeneticasAsync() => await _repository.GetAllAsync();

    public async Task<IEnumerable<HistoriaGenetica?>> ObtenerHistoriasGeneticasPaginadasAsync(int pagina, int tamanoPagina)
    {
        var historias = await _repository.GetAllAsync();
        return historias
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToList();
    }

    public async Task<HistoriaGenetica?> ActualizarHistoriaGeneticaAsync(HistoriaGenetica historiaGenetica)
    {
        _repository.Update(historiaGenetica);
        await _repository.SaveAsync();
        return historiaGenetica;
    }

    public async Task<bool> EliminarHistoriaGeneticaAsync(int id)
    {
        var historiaGenetica = await _repository.GetByIdAsync(id);
        if (historiaGenetica != null)
        {
            _repository.Remove(historiaGenetica);
            await _repository.SaveAsync();
            return true;
        }
        return false;
    }
}
