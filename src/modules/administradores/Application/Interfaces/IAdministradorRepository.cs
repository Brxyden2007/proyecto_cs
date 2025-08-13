using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.administradores.Application.Interfaces
{
    public interface IAdministradorRepository
    {
        Task<Administrador?> GetAdministradorByIdAsync(int id);
        Task<IEnumerable<Administrador>> GetAllAdministradorsAsync();
        void Add(Administrador administrador);
        void Update(Administrador administrador);
        void Delete(Administrador administrador);
        Task SaveAsync();
        Task<Administrador?> GetByEmailAsync(string email);
        Task<Administrador?> GetByEmailAndPasswordAsync(string email, string password);
        /*Task<bool> EmailExistsAsync(string email);
        Task<bool> IsValidPasswordAsync(string email, string password);     
        Task<bool> IsValidUserAsync(string email, string password);
        Task<bool> IsAdministradorAsync(string email, string password);
        Task<bool> IsAdministradorAsync(int id);
        Task<bool> IsAdministradorAsync(Administrador administrador);*/
    }
}