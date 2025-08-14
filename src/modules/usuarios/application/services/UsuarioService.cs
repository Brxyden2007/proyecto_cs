using Microsoft.EntityFrameworkCore;

namespace proyecto_cs.src.modules.usuarios.application
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarUsuarioAsync(
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
            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
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

            // Crear entidad Usuario
            var usuario = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                PasswordHash = hash,
                CreatedAt = DateTime.Now,
                Persona = persona
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> LoginUsuarioAsync(string email, string password)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Persona)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
                return false;

            // Aquí asumimos que guardaste también el Salt en la DB
            return usuario.PasswordHash != null && 
                PasswordHasher.VerifyPassword(password, usuario.PasswordHash);

        }

    }
}
