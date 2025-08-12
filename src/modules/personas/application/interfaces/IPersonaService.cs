using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public interface IPersonaService
{
    Task RegisterPersonaAsync(Persona persona);
    Task UpdatePersonaAsync(Persona persona);
    Task DeletePersonaAsync(int id);
    Task<Persona?> GetPersonaByIdAsync(int id);
    Task<IEnumerable<Persona>> GetAllPersonasAsync();
}
