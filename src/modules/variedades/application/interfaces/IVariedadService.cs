using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs.src.modules.variedades.application.interfaces;
public interface IVariedadService
{
    Task<Variedad> CrearVariedadAsync(Variedad variedad);
    Task<Variedad?> ObtenerVariedadPorIdAsync(int id);
    Task<IEnumerable<Variedad>> ObtenerTodasLasVariedadesAsync();
    Task<IEnumerable<Variedad>> ObtenerVariedadesPaginadasAsync(int pagina, int tamanoPagina);
    Task<Variedad> ActualizarVariedadAsync(Variedad variedad);
    Task<bool> EliminarVariedadAsync(int id);
    
    // Métodos de filtrado
    Task<IEnumerable<Variedad>> FiltrarPorNombreAsync(string nombre);
    Task<IEnumerable<Variedad>> FiltrarPorNombreCientificoAsync(string nombreCientifico);
    Task<IEnumerable<Variedad>> FiltrarPorPorteAsync(int idPorte);
    Task<IEnumerable<Variedad>> FiltrarPorTamanioGranoAsync(int idTamanio);
    Task<IEnumerable<Variedad>> FiltrarPorAltitudAsync(int idAltitud);
    Task<IEnumerable<Variedad>> FiltrarPorRendimientoAsync(int idRendimiento);
    Task<IEnumerable<Variedad>> FiltrarPorResistenciaAsync(int idResistencia);
    Task<IEnumerable<Variedad>> FiltrarPorTipoVariedadAsync(string tipo);
    Task<IEnumerable<Variedad>> FiltrarPorAtributoAgronomicoAsync(int idAtributo);
    Task<IEnumerable<Variedad>> FiltrarPorHistoriaGeneticaAsync(int idHistoria);
}
