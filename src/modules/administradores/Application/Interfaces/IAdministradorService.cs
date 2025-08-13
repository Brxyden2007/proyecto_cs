using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.administradores.Application.Interfaces
{
    public interface IAdministradorService
    {
        Task RegistrarAdministradorAsync(Administrador administrador);
        Task ActualizarAdministradorAsync(Administrador administrador);
        Task EliminarAdministradorAsync(int id);
        Task<Administrador?> ObtenerAdministradorPorIdAsync(int id);
        Task<IEnumerable<Administrador>> ObtenerTodosLosAdministradorsAsync();   
    }
}