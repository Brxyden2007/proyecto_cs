using Microsoft.EntityFrameworkCore;
using proyecto_cs;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs.src.modules.administradores.application
{
    public class AdministradorService
    {
        private readonly AppDbContext _context;

        public AdministradorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarAdministradorAsync(
            string nombre,
            string apellido,
            int edad,
            string nacionalidad,
            int documentoIdentidad,
            string genero,
            string email,
            string password)
        {
            // Validar email único
            if (await _context.Administradors.AnyAsync(a => a.Email == email))
                throw new Exception("El email ya está registrado.");

            // Hashear contraseña
            var hash = PasswordHasher.HashPassword(password);

            // Crear entidad Persona
            var persona = new Persona
            {
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                Nacionalidad = nacionalidad,
                DocumentoIdentidad = documentoIdentidad,
                Genero = genero
            };

            // Crear entidad Administrador
            var admin = new Administrador
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                PasswordHash = hash,
                CreatedAt = DateTime.Now,
                Persona = persona
            };

            _context.Administradors.Add(admin);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> LoginAdministradorAsync(string email, string password)
        {
            var admin = await _context.Administradors
                .Include(a => a.Persona)
                .FirstOrDefaultAsync(a => a.Email == email);

            if (admin == null)
                return false;

            return PasswordHasher.VerifyPassword(password, admin.PasswordHash);
        }

    }
}
