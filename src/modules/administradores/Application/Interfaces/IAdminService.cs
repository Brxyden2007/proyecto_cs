using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.administradores.Application.Interfaces
{
    public interface IAdminService
    {
        Task RegistrarAdminAsync(Admin admin);
        Task ActualizarAdminAsync(Admin admin);
        Task EliminarAdminAsync(int id);
        Task<Admin?> ObtenerAdminPorIdAsync(int id);
        Task<IEnumerable<Admin>> ObtenerTodosLosAdminsAsync();   
    }
}