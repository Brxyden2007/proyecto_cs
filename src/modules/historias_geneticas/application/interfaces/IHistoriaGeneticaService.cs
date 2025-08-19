using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.historias_geneticas.domain.models;

namespace proyecto_cs.src.modules.historias_geneticas.application.interfaces;
public interface IHistoriaGeneticaService
{
    Task<HistoriaGenetica?> CrearHistoriaGeneticaAsync(HistoriaGenetica historiaGenetica);
    Task<HistoriaGenetica?> ObtenerHistoriaGeneticaPorIdAsync(int id);
    Task<IEnumerable<HistoriaGenetica?>> ObtenerTodasLasHistoriasGeneticasAsync();
    Task<IEnumerable<HistoriaGenetica?>> ObtenerHistoriasGeneticasPaginadasAsync(int pagina, int tamanoPagina);
    Task<HistoriaGenetica?> ActualizarHistoriaGeneticaAsync(HistoriaGenetica historiaGenetica);
    Task<bool> EliminarHistoriaGeneticaAsync(int id);
}
