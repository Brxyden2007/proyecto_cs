using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.atributos_agronomicos.application.interfaces;
using proyecto_cs.src.modules.atributos_agronomicos.domain.models;

namespace proyecto_cs.src.modules.atributos_agronomicos.application.services;
public class AtributoAgronomicoService : IAtributoAgronomicoService
{
    private readonly IAtributoAgronomicoRepository _repository;

    public AtributoAgronomicoService(IAtributoAgronomicoRepository repository) =>_repository = repository;
    public async Task<AtributoAgronomico?> CrearAtributoAgronomicoAsync(AtributoAgronomico atributoAgronomico)
    {
        _repository.Add(atributoAgronomico);
        await _repository.SaveAsync();
        return atributoAgronomico;
    }

    public async Task<AtributoAgronomico?> ObtenerAtributoAgronomicoPorIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<AtributoAgronomico?>> ObtenerTodasLasAtributosAgronomicosAsync() => await _repository.GetAllAsync();
    

    public async Task<IEnumerable<AtributoAgronomico?>> ObtenerAtributosAgronomicosPaginadasAsync(int pagina, int tamanoPagina)
    {
        var atributos = await _repository.GetAllAsync();
        return atributos
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToList();
    }

    public async Task<AtributoAgronomico?> ActualizarAtributoAgronomicoAsync(AtributoAgronomico atributoAgronomico)
    {
        _repository.Update(atributoAgronomico);
        await _repository.SaveAsync();
        return atributoAgronomico;
    }

    public async Task<bool> EliminarAtributoAgronomicoAsync(int id)
    {
        var atributoAgronomico = await _repository.GetByIdAsync(id);
        if (atributoAgronomico != null)
        {
            _repository.Remove(atributoAgronomico);
            await _repository.SaveAsync();
            return true;
        }
        return false;
    }
}
