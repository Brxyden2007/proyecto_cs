using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public interface IPersonaRepository
{
    Task<Persona?> GetByIdAsync(int id);
    Task<IEnumerable<Persona>> GetAllAsync();
    void Add(Persona persona);
    void Update(Persona persona);
    void Remove(Persona persona);
    Task SaveAsync();
}
