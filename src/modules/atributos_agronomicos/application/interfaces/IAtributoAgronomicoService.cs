using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.atributos_agronomicos.domain.models;

namespace proyecto_cs.src.modules.atributos_agronomicos.application.interfaces;
public interface IAtributoAgronomicoService
{
    Task<AtributoAgronomico?> CrearAtributoAgronomicoAsync(AtributoAgronomico atributoAgronomico);
    Task<AtributoAgronomico?> ObtenerAtributoAgronomicoPorIdAsync(int id);
    Task<IEnumerable<AtributoAgronomico?>> ObtenerTodasLasAtributosAgronomicosAsync();
    Task<IEnumerable<AtributoAgronomico?>> ObtenerAtributosAgronomicosPaginadasAsync(int pagina, int tamanoPagina);
    Task<AtributoAgronomico?> ActualizarAtributoAgronomicoAsync(AtributoAgronomico atributoAgronomico);
    Task<bool> EliminarAtributoAgronomicoAsync(int id); 
}
