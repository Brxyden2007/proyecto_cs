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
        // Falta fixearlo, no esta del todo claro.
        _repo.Add(persona);
        await _repo.SaveAsync();
        /*var existingPersona = await _repo.GetByIdAsync(persona.id);
        if (existingPersona != null && existingPersona.id == persona.id)
        {
            throw new InvalidOperationException("Persona with this ID already exists.");
        }
        var newPersona = new Persona
        {
            id = persona.id,
            nombre = persona.nombre,
            apellido = persona.apellido,
            edad = persona.edad,
            nacionalidad = persona.nacionalidad,
            documento_identidad = persona.documento_identidad,
            genero = persona.genero
        };*/
    }

    public async Task UpdatePersonaAsync(Persona persona)
    {
        /*_repo.Update(persona);
        await _repo.SaveAsync();*/
        var existingPersona = await _repo.GetByIdAsync(persona.Id);
        if (existingPersona == null)
        {
            throw new InvalidOperationException($"Persona {persona} no encontrada.");
        }
        existingPersona.Nombre = persona.Nombre;
        existingPersona.Apellido = persona.Apellido;
        existingPersona.Edad = persona.Edad;
        existingPersona.Nacionalidad = persona.Nacionalidad;
        existingPersona.DocumentoIdentidad = persona.DocumentoIdentidad;
        existingPersona.Genero = persona.Genero;
        _repo.Update(existingPersona);
        await _repo.SaveAsync();
    }
    public async Task RemovePersonaAsync(int id)
    {
        /*var persona = await _repo.GetByIdAsync(id);
        if (persona != null)
        {
            _repo.Remove(persona);
            await _repo.SaveAsync();
        }*/
        var existingPersona = await _repo.GetByIdAsync(id);
        if (existingPersona == null)
        {
            throw new InvalidOperationException($"Persona con ID {id} no encontrada.");
        }
        _repo.Remove(existingPersona);
        await _repo.SaveAsync();
    }
    public async Task<Persona?> GetPersonaByIdAsync(int id) =>
        await _repo.GetByIdAsync(id);

    public async Task<IEnumerable<Persona>> GetAllPersonasAsync() =>
        await _repo.GetAllAsync();    
}