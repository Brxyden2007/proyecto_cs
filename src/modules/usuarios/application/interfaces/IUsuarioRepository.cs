using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.usuarios.domain.models;

namespace proyecto_cs.src.modules.usuarios.application.interfaces;
public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    void Add(Usuario usuario);
    void Update(Usuario usuario);
    void Delete(Usuario usuario);
    /*Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario?> GetByEmailAndPasswordAsync(string email, string password);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> IsValidPasswordAsync(string email, string password);
    Task<bool> IsValidUserAsync(string email, string password); */
    Task SaveAsync();
}