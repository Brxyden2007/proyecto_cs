using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.historias_geneticas.domain.models;

namespace proyecto_cs.src.modules.historias_geneticas.application.interfaces;
public interface IHistoriaGeneticaRepository
{
    void Add(HistoriaGenetica historiaGenetica);
    void Update(HistoriaGenetica entity);
    void Remove(HistoriaGenetica entity);
    Task<IEnumerable<HistoriaGenetica?>> GetAllAsync();
    Task<HistoriaGenetica?> GetByIdAsync(int id);
    Task<HistoriaGenetica?> GetByObtentorAsync(string obtentor);
    Task<HistoriaGenetica?> GetByFamiliaAsync(string familia);
    Task<HistoriaGenetica?> GetByGrupoAsync(string grupo);
    Task<HistoriaGenetica?> GetByDescripcionAsync(string descripcion);
    Task SaveAsync();
}
