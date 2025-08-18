using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.administradores.Domain.Entities;
using proyecto_cs.src.modules.variedades.application.services;
using proyecto_cs.src.modules.usuarios.application;
using proyecto_cs.src.modules.administradores.application;
using proyecto_cs.src.modules.variedades.domain.models;
using proyecto_cs.src.modules.usuarios.domain.models;

namespace proyecto_cs;

public class MenuAdministrador
{
    private readonly Administrador administrador;
    private readonly string[] opcionesMenu;
    private readonly VariedadService _variedadService;
    private readonly UsuarioService _usuarioService;
    private readonly AdministradorService _administradorService;
    private readonly AppDbContext _context;
    private int opcionSeleccionada = 0;
    
    public MenuAdministrador(Administrador administradorActual)
    {
        administrador = administradorActual;
        _context = DbContextFactory.Create();
        _variedadService = new VariedadService(_context);
        _usuarioService = new UsuarioService(_context);
        _administradorService = new AdministradorService(_context);
        opcionesMenu = new string[]
        {
            "CRUD variedad completa",
            "CRUD usuarios",
            "CRUD administradores",
            "Salir/Cerrar sesion"
        };
    }

    public async Task MostrarMenu()
    {
        MostrarBienvenidaAdmin();

        bool continuar = true;
        do
        {
            DibujarMenu();
            var tecla = Console.ReadKey(true);

            switch (tecla.Key)
            {
                case ConsoleKey.UpArrow:
                    opcionSeleccionada = (opcionSeleccionada - 1 + opcionesMenu.Length) % opcionesMenu.Length;
                    break;

                case ConsoleKey.DownArrow:
                    opcionSeleccionada = (opcionSeleccionada + 1) % opcionesMenu.Length;
                    break;

                case ConsoleKey.Enter:
                    continuar = await EjecutarOpcion(opcionSeleccionada);
                    break;
            }
        } while (continuar);
    }

    private void MostrarBienvenidaAdmin()
    {
        Console.Clear();
        MenuPrincipal.EscribirConPausa("===============================================================", 10);
        MenuPrincipal.EscribirConPausa("  ▒█▀▀█ ▀█▀ ▒█▀▀▀ ▒█▄░▒█ ▒█░░▒█ ▒█▀▀▀ ▒█▄░▒█ ▀█▀ ▒█▀▀▄ ▒█▀▀▀█", 10);
        MenuPrincipal.EscribirConPausa("  ▒█▀▀▄ ▒█░ ▒█▀▀▀ ▒█▒█▒█ ░▒█▒█░ ▒█▀▀▀ ▒█▒█▒█ ▒█░ ▒█░▒█ ▒█░░▒█", 10);
        MenuPrincipal.EscribirConPausa("  ▒█▄▄█ ▄█▄ ▒█▄▄▄ ▒█░░▀█ ░░▀▄▀░ ▒█▄▄▄ ▒█░░▀█ ▄█▄ ▒█▄▄▀ ▒█▄▄▄█", 10);
        MenuPrincipal.EscribirConPausa($" --¡Admin {administrador.Nombre} {administrador.Apellido} 👑!--", 10);
        MenuPrincipal.EscribirConPausa("================================================================", 10);
        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private void DibujarMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("========================================\n");
        Console.WriteLine("▄▀█ █▀▄ █▀▄▀█ █ █▄░█   █▀▄▀█ █▀▀ █▄░█ █░█");
        Console.WriteLine("█▀█ █▄▀ █░▀░█ █ █░▀█   █░▀░█ ██▄ █░▀█ █▄█");
        Console.WriteLine("========================================\n");
        Console.ResetColor();

        for (int i = 0; i < opcionesMenu.Length; i++)
        {
            if (i == opcionSeleccionada)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"🌱 {opcionesMenu[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {opcionesMenu[i]}");
            }
        }
        Console.WriteLine("\nUsa las flechas ↑ ↓ para moverte y Enter para seleccionar.");
    }

    private async Task<bool> EjecutarOpcion(int opcion)
    {
        switch (opcion)
        {
            case 0: // CRUD variedad completa
                await MostrarMenuVariedades();
                break;
                
            case 1: // CRUD usuarios
                await MostrarMenuUsuarios();
                break;
                
            case 2: // CRUD administradores
                await MostrarMenuAdministradores();
                break;
                
            case 3: // Salir/Cerrar sesión
                return false;
                
            default:
                Console.WriteLine("Opción no válida");
                Console.ReadKey();
                break;
        }
        
        return true;
    }

    private async Task MostrarMenuVariedades()
    {
        string[] opcionesVariedades = {
            "Crear una nueva variedad",
            "Actualizar una variedad", 
            "Eliminar una variedad",
            "Mostrar todas las variedades",
            "Volver al menú principal"
        };

        int opcionSeleccionadaVar = 0;
        bool continuarVariedades = true;

        while (continuarVariedades)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  GESTIÓN DE VARIEDADES                      ║");
            Console.WriteLine("║                    🌱 CAFÉ COLOMBIANO 🌱                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            for (int i = 0; i < opcionesVariedades.Length; i++)
            {
                if (i == opcionSeleccionadaVar)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"🌱 {opcionesVariedades[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {opcionesVariedades[i]}");
                }
            }

            Console.WriteLine("\nUsa las flechas ↑ ↓ para moverte y Enter para seleccionar.");
            var tecla = Console.ReadKey(true);

            switch (tecla.Key)
            {
                case ConsoleKey.UpArrow:
                    opcionSeleccionadaVar = (opcionSeleccionadaVar - 1 + opcionesVariedades.Length) % opcionesVariedades.Length;
                    break;
                case ConsoleKey.DownArrow:
                    opcionSeleccionadaVar = (opcionSeleccionadaVar + 1) % opcionesVariedades.Length;
                    break;
                case ConsoleKey.Enter:
                    continuarVariedades = await EjecutarOpcionVariedad(opcionSeleccionadaVar);
                    break;
            }
        }
    }

    private async Task<bool> EjecutarOpcionVariedad(int opcion)
    {
        try
        {
            switch (opcion)
            {
                case 0: // Crear variedad
                    await CrearVariedad();
                    break;
                case 1: // Actualizar variedad
                    await ActualizarVariedad();
                    break;
                case 2: // Eliminar variedad
                    await EliminarVariedad();
                    break;
                case 3: // Mostrar todas
                    await MostrarTodasLasVariedades();
                    break;
                case 4: // Volver
                    return false;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            Console.ReadKey();
        }
        return true;
    }

    private async Task CrearVariedad()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    CREAR NUEVA VARIEDAD                     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        try
        {
            Console.Write("Nombre común: ");
            string nombreComun = Console.ReadLine() ?? "";

            Console.Write("Nombre científico: ");
            string nombreCientifico = Console.ReadLine() ?? "";

            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine() ?? "";

            Console.Write("URL de imagen: ");
            string imagenUrl = Console.ReadLine() ?? "";

            // Mostrar opciones de porte
            var portes = await _context.Portes.ToListAsync();
            Console.WriteLine("\nPortes disponibles:");
            for (int i = 0; i < portes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {portes[i].Nombre}");
            }
            Console.Write("Seleccione porte (número): ");
            int idPorte = int.Parse(Console.ReadLine() ?? "1");

            // Mostrar opciones de tamaño
            var tamanios = await _context.TamaniosGranos.ToListAsync();
            Console.WriteLine("\nTamaños de grano disponibles:");
            for (int i = 0; i < tamanios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tamanios[i].Nombre}");
            }
            Console.Write("Seleccione tamaño (número): ");
            int idTamanio = int.Parse(Console.ReadLine() ?? "1");

            // Mostrar opciones de altitud
            var altitudes = await _context.Altitudes.ToListAsync();
            Console.WriteLine("\nAltitudes disponibles:");
            for (int i = 0; i < altitudes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {altitudes[i]}");
            }
            Console.Write("Seleccione altitud (número): ");
            int idAltitud = int.Parse(Console.ReadLine() ?? "1");

            // Mostrar opciones de rendimiento
            var rendimientos = await _context.Rendimientos.ToListAsync();
            Console.WriteLine("\nRendimientos disponibles:");
            for (int i = 0; i < rendimientos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {rendimientos[i]}");
            }
            Console.Write("Seleccione rendimiento (número): ");
            int idRendimiento = int.Parse(Console.ReadLine() ?? "1");

            // Mostrar opciones de calidad
            var calidades = await _context.CalidadesAltitudes.ToListAsync();
            Console.WriteLine("\nCalidades disponibles:");
            for (int i = 0; i < calidades.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {calidades[i]}");
            }
            Console.Write("Seleccione calidad (número): ");
            int idCalidad = int.Parse(Console.ReadLine() ?? "1");

            var nuevaVariedad = new Variedad(nombreComun, nombreCientifico, descripcion, imagenUrl, 
                                           idPorte, idTamanio, idAltitud, idRendimiento, idCalidad);

            await _variedadService.CrearVariedadAsync(nuevaVariedad);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Variedad creada exitosamente!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error al crear variedad: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task ActualizarVariedad()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                   ACTUALIZAR VARIEDAD                       ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            // Mostrar variedades existentes
            var variedades = await _variedadService.ObtenerTodasLasVariedadesAsync();
            var lista = variedades.ToList();

            Console.WriteLine("\nVariedades existentes:");
            for (int i = 0; i < Math.Min(lista.Count, 10); i++)
            {
                Console.WriteLine($"{lista[i].IdVariedad}. {lista[i].NombreComun}");
            }

            Console.Write("\nIngrese el ID de la variedad a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var variedad = await _variedadService.ObtenerVariedadPorIdAsync(id);
                if (variedad != null)
                {
                    Console.WriteLine($"\nActualizando: {variedad.NombreComun}");
                    
                    Console.Write($"Nuevo nombre común (actual: {variedad.NombreComun}): ");
                    string? nuevoNombre = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoNombre))
                        variedad.NombreComun = nuevoNombre;

                    Console.Write($"Nuevo nombre científico (actual: {variedad.NombreCientifico}): ");
                    string? nuevoNombreCientifico = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoNombreCientifico))
                        variedad.NombreCientifico = nuevoNombreCientifico;

                    Console.Write($"Nueva descripción (actual: {variedad.Descripcion}): ");
                    string? nuevaDescripcion = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevaDescripcion))
                        variedad.Descripcion = nuevaDescripcion;

                    await _variedadService.ActualizarVariedadAsync(variedad);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Variedad actualizada exitosamente!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Variedad no encontrada.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task EliminarVariedad()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    ELIMINAR VARIEDAD                        ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            var variedades = await _variedadService.ObtenerTodasLasVariedadesAsync();
            var lista = variedades.ToList();

            Console.WriteLine("\nVariedades existentes:");
            for (int i = 0; i < Math.Min(lista.Count, 10); i++)
            {
                Console.WriteLine($"{lista[i].IdVariedad}. {lista[i].NombreComun}");
            }

            Console.Write("\nIngrese el ID de la variedad a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var variedad = await _variedadService.ObtenerVariedadPorIdAsync(id);
                if (variedad != null)
                {
                    Console.WriteLine($"\n¿Está seguro de eliminar '{variedad.NombreComun}'? (s/n): ");
                    string confirmacion = Console.ReadLine()?.ToLower() ?? "";
                    
                    if (confirmacion == "s" || confirmacion == "si")
                    {
                        bool eliminado = await _variedadService.EliminarVariedadAsync(id);
                        if (eliminado)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n✅ Variedad eliminada exitosamente!");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("No se pudo eliminar la variedad.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Eliminación cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine("Variedad no encontrada.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task MostrarTodasLasVariedades()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                   TODAS LAS VARIEDADES                      ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            var variedades = await _variedadService.ObtenerTodasLasVariedadesAsync();
            var lista = variedades.ToList();

            if (!lista.Any())
            {
                Console.WriteLine("No hay variedades registradas.");
            }
            else
            {
                foreach (var variedad in lista)
                {
                    Console.WriteLine($"\n🌱 ID: {variedad.IdVariedad}");
                    Console.WriteLine($"   Nombre: {variedad.NombreComun}");
                    Console.WriteLine($"   Científico: {variedad.NombreCientifico}");
                    Console.WriteLine($"   Porte: {variedad.Porte.Nombre}");
                    Console.WriteLine($"   Tamaño: {variedad.TamanioGrano.Nombre}");
                    Console.WriteLine($"   Rendimiento: {variedad.Rendimiento}");
                    Console.WriteLine("   " + new string('-', 50));
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task MostrarMenuUsuarios()
    {
        string[] opcionesUsuarios = {
            "Crear un nuevo usuario",
            "Actualizar un usuario",
            "Eliminar un usuario", 
            "Mostrar todos los usuarios",
            "Volver al menú principal"
        };

        int opcionSeleccionadaUsr = 0;
        bool continuarUsuarios = true;

        while (continuarUsuarios)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   GESTIÓN DE USUARIOS                       ║");
            Console.WriteLine("║                      👥 USUARIOS 👥                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            for (int i = 0; i < opcionesUsuarios.Length; i++)
            {
                if (i == opcionSeleccionadaUsr)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"👥 {opcionesUsuarios[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {opcionesUsuarios[i]}");
                }
            }

            Console.WriteLine("\nUsa las flechas ↑ ↓ para moverte y Enter para seleccionar.");
            var tecla = Console.ReadKey(true);

            switch (tecla.Key)
            {
                case ConsoleKey.UpArrow:
                    opcionSeleccionadaUsr = (opcionSeleccionadaUsr - 1 + opcionesUsuarios.Length) % opcionesUsuarios.Length;
                    break;
                case ConsoleKey.DownArrow:
                    opcionSeleccionadaUsr = (opcionSeleccionadaUsr + 1) % opcionesUsuarios.Length;
                    break;
                case ConsoleKey.Enter:
                    continuarUsuarios = await EjecutarOpcionUsuario(opcionSeleccionadaUsr);
                    break;
            }
        }
    }

    private async Task<bool> EjecutarOpcionUsuario(int opcion)
    {
        try
        {
            switch (opcion)
            {
                case 0: // Crear usuario
                    await CrearUsuario();
                    break;
                case 1: // Actualizar usuario
                    await ActualizarUsuario();
                    break;
                case 2: // Eliminar usuario
                    await EliminarUsuario();
                    break;
                case 3: // Mostrar todos
                    await MostrarTodosLosUsuarios();
                    break;
                case 4: // Volver
                    return false;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            Console.ReadKey();
        }
        return true;
    }

    private async Task CrearUsuario()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                     CREAR NUEVO USUARIO                     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine() ?? "";
            Console.Write("Edad: ");
            int edad = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nacionalidad: ");
            string nacionalidad = Console.ReadLine() ?? "";
            Console.Write("Documento de Identidad: ");
            int documento = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Género: ");
            string genero = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Contraseña: ");
            string password = Console.ReadLine() ?? "";

            await _usuarioService.RegistrarUsuarioAsync(nombre, apellido, edad, nacionalidad, documento, genero, email, password);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Usuario creado exitosamente!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task ActualizarUsuario()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    ACTUALIZAR USUARIO                       ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            var usuarios = await _context.Usuarios.Include(u => u.Persona).ToListAsync();
            
            Console.WriteLine("\nUsuarios existentes:");
            for (int i = 0; i < Math.Min(usuarios.Count, 10); i++)
            {
                Console.WriteLine($"{usuarios[i].Id}. {usuarios[i].Nombre} {usuarios[i].Apellido} - {usuarios[i].Email}");
            }

            Console.Write("\nIngrese el ID del usuario a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var usuario = usuarios.FirstOrDefault(u => u.Id == id);
                if (usuario != null)
                {
                    Console.WriteLine($"\nActualizando: {usuario.Nombre} {usuario.Apellido}");
                    
                    Console.Write($"Nuevo nombre (actual: {usuario.Nombre}): ");
                    string? nuevoNombre = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoNombre))
                        usuario.Nombre = nuevoNombre;

                    Console.Write($"Nuevo apellido (actual: {usuario.Apellido}): ");
                    string? nuevoApellido = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoApellido))
                        usuario.Apellido = nuevoApellido;

                    Console.Write($"Nuevo email (actual: {usuario.Email}): ");
                    string? nuevoEmail = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoEmail))
                        usuario.Email = nuevoEmail;

                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Usuario actualizado exitosamente!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task EliminarUsuario()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                     ELIMINAR USUARIO                        ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            var usuarios = await _context.Usuarios.Include(u => u.Persona).ToListAsync();
            
            Console.WriteLine("\nUsuarios existentes:");
            for (int i = 0; i < Math.Min(usuarios.Count, 10); i++)
            {
                Console.WriteLine($"{usuarios[i].Id}. {usuarios[i].Nombre} {usuarios[i].Apellido} - {usuarios[i].Email}");
            }

            Console.Write("\nIngrese el ID del usuario a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var usuario = usuarios.FirstOrDefault(u => u.Id == id);
                if (usuario != null)
                {
                    Console.WriteLine($"\n¿Está seguro de eliminar a '{usuario.Nombre} {usuario.Apellido}'? (s/n): ");
                    string confirmacion = Console.ReadLine()?.ToLower() ?? "";
                    
                    if (confirmacion == "s" || confirmacion == "si")
                    {
                        _context.Usuarios.Remove(usuario);
                        await _context.SaveChangesAsync();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Usuario eliminado exitosamente!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Eliminación cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task MostrarTodosLosUsuarios()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    TODOS LOS USUARIOS                       ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            var usuarios = await _context.Usuarios.Include(u => u.Persona).ToListAsync();

            if (!usuarios.Any())
            {
                Console.WriteLine("No hay usuarios registrados.");
            }
            else
            {
                foreach (var usuario in usuarios)
                {
                    Console.WriteLine($"\n👤 ID: {usuario.Id}");
                    Console.WriteLine($"   Nombre: {usuario.Nombre} {usuario.Apellido}");
                    Console.WriteLine($"   Email: {usuario.Email}");
                    Console.WriteLine($"   Edad: {usuario.Persona?.Edad ?? 0}");
                    Console.WriteLine($"   Nacionalidad: {usuario.Persona?.Nacionalidad ?? "N/A"}");
                    Console.WriteLine($"   Fecha registro: {usuario.CreatedAt:dd/MM/yyyy}");
                    Console.WriteLine("   " + new string('-', 50));
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task MostrarMenuAdministradores()
    {
        string[] opcionesAdmins = {
            "Crear un nuevo administrador",
            "Actualizar un administrador",
            "Eliminar un administrador",
            "Mostrar todos los administradores", 
            "Volver al menú principal"
        };

        int opcionSeleccionadaAdm = 0;
        bool continuarAdmins = true;

        while (continuarAdmins)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                GESTIÓN DE ADMINISTRADORES                   ║");
            Console.WriteLine("║                      👑 ADMINS 👑                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            for (int i = 0; i < opcionesAdmins.Length; i++)
            {
                if (i == opcionSeleccionadaAdm)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"👑 {opcionesAdmins[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {opcionesAdmins[i]}");
                }
            }

            Console.WriteLine("\nUsa las flechas ↑ ↓ para moverte y Enter para seleccionar.");
            var tecla = Console.ReadKey(true);

            switch (tecla.Key)
            {
                case ConsoleKey.UpArrow:
                    opcionSeleccionadaAdm = (opcionSeleccionadaAdm - 1 + opcionesAdmins.Length) % opcionesAdmins.Length;
                    break;
                case ConsoleKey.DownArrow:
                    opcionSeleccionadaAdm = (opcionSeleccionadaAdm + 1) % opcionesAdmins.Length;
                    break;
                case ConsoleKey.Enter:
                    continuarAdmins = await EjecutarOpcionAdministrador(opcionSeleccionadaAdm);
                    break;
            }
        }
    }

    private async Task<bool> EjecutarOpcionAdministrador(int opcion)
    {
        try
        {
            switch (opcion)
            {
                case 0: // Crear administrador
                    await CrearAdministrador();
                    break;
                case 1: // Actualizar administrador
                    await ActualizarAdministrador();
                    break;
                case 2: // Eliminar administrador
                    await EliminarAdministrador();
                    break;
                case 3: // Mostrar todos
                    await MostrarTodosLosAdministradores();
                    break;
                case 4: // Volver
                    return false;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            Console.ReadKey();
        }
        return true;
    }

    private async Task CrearAdministrador()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                  CREAR NUEVO ADMINISTRADOR                  ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine() ?? "";
            Console.Write("Edad: ");
            int edad = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nacionalidad: ");
            string nacionalidad = Console.ReadLine() ?? "";
            Console.Write("Documento de Identidad: ");
            int documento = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Género: ");
            string genero = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Contraseña: ");
            string password = Console.ReadLine() ?? "";

            await _administradorService.RegistrarAdministradorAsync(nombre, apellido, edad, nacionalidad, documento, genero, email, password);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Administrador creado exitosamente!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task ActualizarAdministrador()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                 ACTUALIZAR ADMINISTRADOR                    ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            var administradores = await _context.Administradors.Include(a => a.Persona).ToListAsync();
            
            Console.WriteLine("\nAdministradores existentes:");
            for (int i = 0; i < Math.Min(administradores.Count, 10); i++)
            {
                Console.WriteLine($"{administradores[i].Id}. {administradores[i].Nombre} {administradores[i].Apellido} - {administradores[i].Email}");
            }

            Console.Write("\nIngrese el ID del administrador a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var admin = administradores.FirstOrDefault(a => a.Id == id);
                if (admin != null)
                {
                    Console.WriteLine($"\nActualizando: {admin.Nombre} {admin.Apellido}");
                    
                    Console.Write($"Nuevo nombre (actual: {admin.Nombre}): ");
                    string? nuevoNombre = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoNombre))
                        admin.Nombre = nuevoNombre;

                    Console.Write($"Nuevo apellido (actual: {admin.Apellido}): ");
                    string? nuevoApellido = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoApellido))
                        admin.Apellido = nuevoApellido;

                    Console.Write($"Nuevo email (actual: {admin.Email}): ");
                    string? nuevoEmail = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoEmail))
                        admin.Email = nuevoEmail;

                    _context.Administradors.Update(admin);
                    await _context.SaveChangesAsync();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Administrador actualizado exitosamente!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Administrador no encontrado.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task EliminarAdministrador()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                  ELIMINAR ADMINISTRADOR                     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            var administradores = await _context.Administradors.Include(a => a.Persona).ToListAsync();
            
            Console.WriteLine("\nAdministradores existentes:");
            for (int i = 0; i < Math.Min(administradores.Count, 10); i++)
            {
                Console.WriteLine($"{administradores[i].Id}. {administradores[i].Nombre} {administradores[i].Apellido} - {administradores[i].Email}");
            }

            Console.Write("\nIngrese el ID del administrador a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var admin = administradores.FirstOrDefault(a => a.Id == id);
                if (admin != null)
                {
                    Console.WriteLine($"\n¿Está seguro de eliminar a '{admin.Nombre} {admin.Apellido}'? (s/n): ");
                    string confirmacion = Console.ReadLine()?.ToLower() ?? "";
                    
                    if (confirmacion == "s" || confirmacion == "si")
                    {
                        _context.Administradors.Remove(admin);
                        await _context.SaveChangesAsync();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Administrador eliminado exitosamente!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Eliminación cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine("Administrador no encontrado.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task MostrarTodosLosAdministradores()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                 TODOS LOS ADMINISTRADORES                   ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        try
        {
            var administradores = await _context.Administradors.Include(a => a.Persona).ToListAsync();

            if (!administradores.Any())
            {
                Console.WriteLine("No hay administradores registrados.");
            }
            else
            {
                foreach (var admin in administradores)
                {
                    Console.WriteLine($"\n👑 ID: {admin.Id}");
                    Console.WriteLine($"   Nombre: {admin.Nombre} {admin.Apellido}");
                    Console.WriteLine($"   Email: {admin.Email}");
                    Console.WriteLine($"   Edad: {admin.Persona?.Edad ?? 0}");
                    Console.WriteLine($"   Nacionalidad: {admin.Persona?.Nacionalidad ?? "N/A"}");
                    Console.WriteLine($"   Fecha registro: {admin.CreatedAt:dd/MM/yyyy}");
                    Console.WriteLine("   " + new string('-', 50));
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }
}
