using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Application.Interfaces;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.administradores.Application.Services
{
    public class AdministradorService
    {
        private readonly IAdministradorService _repo;

        public AdministradorService(IAdministradorService repo)
        {
            _repo = repo;
        }   
        public Task RegistrarAdministradorAsync(Administrador administrador)
        {
            // Lógica para registrar un administrador
            return _repo.RegistrarAdministradorAsync(administrador);
        }

        public Task ActualizarAdministradorAsync(Administrador administrador)
        {
            // Lógica para actualizar un administrador
            return _repo.ActualizarAdministradorAsync(administrador);
        }

        public Task EliminarAdministradorAsync(int id)
        {
            // Lógica para eliminar un administrador
            return _repo.EliminarAdministradorAsync(id);
        }

        public Task<Administrador?> ObtenerAdministradorPorIdAsync(int id)
        {
            // Lógica para obtener un administrador por ID
            return _repo.ObtenerAdministradorPorIdAsync(id);
        }

        public Task<IEnumerable<Administrador>> ObtenerTodosLosAdministradorsAsync()
        {
            // Lógica para obtener todos los administradores
            return _repo.ObtenerTodosLosAdministradorsAsync();
        }   
    }
}