using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class PersonaService
{
    private readonly IPersonaRepository _repo;

    public PersonaService(IPersonaRepository repo)
    {
        _repo = repo;
    }

    public async Task RegisterPersonaAsync(Persona persona)
    {
        _repo.Add(persona);
        await _repo.SaveAsync();
    }

    public async Task UpdatePersonaAsync(Persona persona)
    {
        _repo.Update(persona);
        await _repo.SaveAsync();
    }

    public async Task DeletePersonaAsync(int id)
    {
        var persona = await _repo.GetByIdAsync(id);
        if (persona != null)
        {
            _repo.Remove(persona);
            await _repo.SaveAsync();
        }
    }

    public async Task<Persona?> GetPersonaByIdAsync(int id) =>
        await _repo.GetByIdAsync(id);

    public async Task<IEnumerable<Persona>> GetAllPersonasAsync() =>
        await _repo.GetAllAsync();    
}