using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.administradores.Application.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetAdminByIdAsync(int id);
        Task<IEnumerable<Admin>> GetAllAdminsAsync();
        void Add(Admin admin);
        void Update(Admin admin);
        void Delete(Admin admin);
        Task SaveAsync();
        Task<Admin?> GetByEmailAsync(string email);
        Task<Admin?> GetByEmailAndPasswordAsync(string email, string password);
        /*Task<bool> EmailExistsAsync(string email);
        Task<bool> IsValidPasswordAsync(string email, string password);     
        Task<bool> IsValidUserAsync(string email, string password);
        Task<bool> IsAdminAsync(string email, string password);
        Task<bool> IsAdminAsync(int id);
        Task<bool> IsAdminAsync(Admin admin);*/
    }
}