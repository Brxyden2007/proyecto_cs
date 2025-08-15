using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.personas.application.interfaces;
public interface IPersonaService
{
    Task RegisterPersonaAsync(Persona persona);
    Task UpdatePersonaAsync(Persona persona);
    Task DeletePersonaAsync(int id);
    Task<Persona?> GetPersonaByIdAsync(int id);
    Task<IEnumerable<Persona>> GetAllPersonasAsync();
}
