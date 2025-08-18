using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.usuarios.application;
using proyecto_cs.src.modules.administradores.application;
using proyecto_cs.src.modules.administradores.Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using proyecto_cs;
using proyecto_cs.src.modules.usuarios.domain.models;

namespace proyecto_cs;
public class MenuPrincipal
{
    private readonly UsuarioService _usuarioService;
    private readonly AdministradorService _administradorService;
    private readonly AppDbContext _context;

    private readonly string[] opcionesLogin =
    {
        "Registrar Usuario",
        "Login Usuario", 
        "Registrar Admin",
        "Login Admin",
        "Salir"
    };

    private int opcionSeleccionadaLogin = 0;

    public MenuPrincipal()
    {
        _context = DbContextFactory.Create();
        _usuarioService = new UsuarioService(_context);
        _administradorService = new AdministradorService(_context);
    }

    public async Task IniciarAplicacion()
    {
        bool salir = false;
        while (!salir)
        {
            DibujarMenuLogin();
            var tecla = Console.ReadKey(true);

            switch (tecla.Key)
            {
                case ConsoleKey.UpArrow:
                    opcionSeleccionadaLogin = (opcionSeleccionadaLogin - 1 + opcionesLogin.Length) % opcionesLogin.Length;
                    break;

                case ConsoleKey.DownArrow:
                    opcionSeleccionadaLogin = (opcionSeleccionadaLogin + 1) % opcionesLogin.Length;
                    break;

                case ConsoleKey.Enter:
                    salir = await EjecutarOpcionLogin(opcionSeleccionadaLogin);
                    break;
            }
        }
    }
    public Task CartelBienvenida()
    {
        Console.Clear();
        Console.WriteLine("=== -------------------------------------------------------------------------------------------------------  ===");
        Console.WriteLine("░█████╗░░█████╗░██╗░░░░░░█████╗░███╗░░░███╗██████╗░██╗░█████╗░  ░█████╗░░█████╗░███████╗███████╗███████╗███████╗");
        Console.WriteLine("██╔══██╗██╔══██╗██║░░░░░██╔══██╗████╗░████║██╔══██╗██║██╔══██╗  ██╔══██╗██╔══██╗██╔════╝██╔════╝██╔════╝██╔════╝");
        Console.WriteLine("██║░░╚═╝██║░░██║██║░░░░░██║░░██║██╔████╔██║██████╦╝██║███████║  ██║░░╚═╝██║░░██║█████╗░░█████╗░░█████╗░░█████╗░░");
        Console.WriteLine("██║░░██╗██║░░██║██║░░░░░██║░░██║██║╚██╔╝██║██╔══██╗██║██╔══██║  ██║░░██╗██║░░██║██╔══╝░░██╔══╝░░██╔══╝░░██╔══╝░░");
        Console.WriteLine("╚█████╔╝╚█████╔╝███████╗╚█████╔╝██║░╚═╝░██║██████╦╝██║██║░░██║  ╚█████╔╝╚█████╔╝██║░░░░░██║░░░░░███████╗███████╗");
        Console.WriteLine("                          --- Catalogando el mejor café de Colombia 🌱 ---                                      ");
        Console.WriteLine("=== -------------------------------------------------------------------------------------------------------  ===");
        Console.WriteLine("¡Bienvenido al sistema de catálogo de café más completo!");
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
        return Task.CompletedTask;
    }

    private void DibujarMenuLogin()
    {
        Console.Clear();
        Console.WriteLine("=== --------------------------------------------------------------------  ===");
        Console.WriteLine("             ▒█▀▄▀█ ░█▀▀█ ▀█▀ ▒█▄░▒█ 　 ▒█▀▄▀█ ▒█▀▀▀ ▒█▄░▒█ ▒█░▒█            "); 
        Console.WriteLine("             ▒█▒█▒█ ▒█▄▄█ ▒█░ ▒█▒█▒█ 　 ▒█▒█▒█ ▒█▀▀▀ ▒█▒█▒█ ▒█░▒█            "); 
        Console.WriteLine("             ▒█░░▒█ ▒█░▒█ ▄█▄ ▒█░░▀█ 　 ▒█░░▒█ ▒█▄▄▄ ▒█░░▀█ ░▀▄▄▀            ");
        Console.WriteLine("                        --- 🌱Registro y Log in🌱 ---                        ");
        Console.WriteLine("=== --------------------------------------------------------------------  ===\n");

        Console.WriteLine("Usa las flechas ↑ ↓ y Enter para seleccionar.\n");

        for (int i = 0; i < opcionesLogin.Length; i++)
        {
            if (i == opcionSeleccionadaLogin)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"🌱 {opcionesLogin[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {opcionesLogin[i]}");
            }
        }
    }

    private async Task<bool> EjecutarOpcionLogin(int opcion)
    {
        switch (opcion)
        {
            case 0:
                await RegistrarUsuario();
                return false;

            case 1:
                var usuarioLogueado = await RealizarLoginUsuario();
                if (usuarioLogueado != null)
                {
                    var menuUsuario = new MenuUsuario(usuarioLogueado);
                    await menuUsuario.MostrarMenu();
                }
                return false;

            case 2:
                await RegistrarAdministrador();
                return false;

            case 3:
                var adminLogueado = await RealizarLoginAdministrador();
                if (adminLogueado != null)
                {
                    var menuAdmin = new MenuAdministrador(adminLogueado);
                    await menuAdmin.MostrarMenu();
                }
                return false;

            case 4:
                return true; // salir

            default:
                return false;
        }
    }

    private async Task RegistrarUsuario()
    {
        Console.Clear();
        Console.WriteLine("📋 Registro de Usuario");
        
        try
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!;
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine()!;
            Console.Write("Edad: ");
            int edad = int.Parse(Console.ReadLine()!);
            Console.Write("Nacionalidad: ");
            string nacionalidad = Console.ReadLine()!;
            Console.Write("Documento de Identidad: ");
            int documento = int.Parse(Console.ReadLine()!);
            Console.Write("Género: ");
            string genero = Console.ReadLine()!;
            Console.Write("Email: ");
            string email = Console.ReadLine()!;
            Console.Write("Contraseña: ");
            string password = LeerPassword();

            await _usuarioService.RegistrarUsuarioAsync(nombre, apellido, edad, nacionalidad, documento, genero, email, password);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Usuario registrado exitosamente.");
            Console.ResetColor();
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: Error a la hora de registrar el usuario.");
            Console.ResetColor();
        }
        
        Console.ReadKey();
    }

    private async Task RegistrarAdministrador()
    {
        Console.Clear();
        Console.WriteLine("📋 Registro de Administrador");
        
        try
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!;
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine()!;
            Console.Write("Edad: ");
            int edad = int.Parse(Console.ReadLine()!);
            Console.Write("Nacionalidad: ");
            string nacionalidad = Console.ReadLine()!;
            Console.Write("Documento de Identidad: ");
            int documento = int.Parse(Console.ReadLine()!);
            Console.Write("Género: ");
            string genero = Console.ReadLine()!;
            Console.Write("Email: ");
            string email = Console.ReadLine()!;
            Console.Write("Contraseña: ");
            string password = LeerPassword();

            await _administradorService.RegistrarAdministradorAsync(nombre, apellido, edad, nacionalidad, documento, genero, email, password);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Administrador registrado exitosamente.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }
        
        Console.ReadKey();
    }

    private async Task<Usuario?> RealizarLoginUsuario()
    {
        Console.Clear();
        Console.WriteLine("🔐 Login USUARIO");
        Console.Write("Email: ");
        string email = Console.ReadLine()!;
        Console.Write("Contraseña: ");
        string password = LeerPassword();

        try
        {
            bool loginExitoso = await _usuarioService.LoginUsuarioAsync(email, password);
            
            if (loginExitoso)
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.Persona)
                    .FirstOrDefaultAsync(u => u.Email == email);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Inicio de sesión exitoso.");
                Console.ResetColor();
                Thread.Sleep(1000);
                return usuario;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Email o contraseña incorrectos.");
                Console.ResetColor();
                Console.ReadKey();
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
            Console.ReadKey();
            return null;
        }
    }

    private async Task<Administrador?> RealizarLoginAdministrador()
    {
        Console.Clear();
        Console.WriteLine("🔐 Login ADMINISTRADOR");
        Console.Write("Email: ");
        string email = Console.ReadLine()!;
        Console.Write("Contraseña: ");
        string password = LeerPassword();

        try
        {
            bool loginExitoso = await _administradorService.LoginAdministradorAsync(email, password);
            
            if (loginExitoso)
            {
                var admin = await _context.Administradors
                    .Include(a => a.Persona)
                    .FirstOrDefaultAsync(a => a.Email == email);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Inicio de sesión exitoso.");
                Console.ResetColor();
                Thread.Sleep(1000);
                return admin;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Email o contraseña incorrectos.");
                Console.ResetColor();
                Console.ReadKey();
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
            Console.ReadKey();
            return null;
        }
    }

    private string LeerPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }

    public static void EscribirConPausa(string texto, int ms)
    {
        Console.WriteLine(texto);
        Thread.Sleep(ms);
    }
}