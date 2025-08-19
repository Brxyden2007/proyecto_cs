using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.atributos_agronomicos.domain.models;

namespace proyecto_cs.src.modules.atributos_agronomicos.application.interfaces;
public interface IAtributoAgronomicoRepository
{
    void Add(AtributoAgronomico atributoAgronomico);
    void Update(AtributoAgronomico entity);
    void Remove(AtributoAgronomico entity);
    Task<IEnumerable<AtributoAgronomico?>> GetAllAsync();
    Task<AtributoAgronomico?> GetByIdAsync(int id);
    Task<AtributoAgronomico?> GetByTiempoCosechaAsync(string tiempoCosecha);
    Task<AtributoAgronomico?> GetByMaduracionAsync(string maduracion);
    Task<AtributoAgronomico?> GetByNutricionAsync(string nutricion);
    Task<AtributoAgronomico?> GetByDensidadSiembraAsync(string densidadSiembra);
    Task SaveAsync();
}

