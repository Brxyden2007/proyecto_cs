using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Application.Interfaces;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.administradores.Application.Services
{
    public class AdminService
    {
        private readonly IAdminService _repo;

        public AdminService(IAdminService repo)
        {
            _repo = repo;
        }   
        public Task RegistrarAdminAsync(Admin admin)
        {
            // Lógica para registrar un administrador
            return _repo.RegistrarAdminAsync(admin);
        }

        public Task ActualizarAdminAsync(Admin admin)
        {
            // Lógica para actualizar un administrador
            return _repo.ActualizarAdminAsync(admin);
        }

        public Task EliminarAdminAsync(int id)
        {
            // Lógica para eliminar un administrador
            return _repo.EliminarAdminAsync(id);
        }

        public Task<Admin?> ObtenerAdminPorIdAsync(int id)
        {
            // Lógica para obtener un administrador por ID
            return _repo.ObtenerAdminPorIdAsync(id);
        }

        public Task<IEnumerable<Admin>> ObtenerTodosLosAdminsAsync()
        {
            // Lógica para obtener todos los administradores
            return _repo.ObtenerTodosLosAdminsAsync();
        }   
    }
}