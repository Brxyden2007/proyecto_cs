using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs.src.modules.variedades.application.interfaces;

public interface IVariedadRepository
{
    void Add(Variedad variedad);
    void Update(Variedad entity);
    void Remove(Variedad entity);
    Task<IEnumerable<Variedad?>> GetAllAsync();
    Task<Variedad?> GetByIdAsync(int id);
    Task<Variedad?> GetByNameAsync(string nombre);
    Task SaveAsync();
}