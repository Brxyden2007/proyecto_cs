using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.personas.application.interfaces;
public interface IPersonaRepository
{
    Task<Persona?> GetByIdAsync(int id);
    Task<IEnumerable<Persona>> GetAllAsync();
    void Add(Persona persona);
    void Update(Persona persona);
    void Remove(Persona persona);
    Task SaveAsync();
}
