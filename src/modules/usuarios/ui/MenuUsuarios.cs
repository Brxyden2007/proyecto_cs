using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.usuarios.application;

namespace proyecto_cs.src.modules.usuarios.ui;
public class MenuUsuarios
{
    private readonly UsuarioService _usuarioService;

    public MenuUsuarios(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task RegistrarUsuarioAsync()
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? "";

        Console.Write("Apellido: ");
        string apellido = Console.ReadLine() ?? "";

        Console.Write("Edad: ");
        int edad = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Nacionalidad: ");
        string nacionalidad = Console.ReadLine() ?? "";

        Console.Write("Documento de identidad: ");
        int documento = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Género: ");
        string genero = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Contraseña: ");
        string password = LeerPassword();

        await _usuarioService.RegistrarUsuarioAsync(
            nombre, apellido, edad, nacionalidad, documento, genero, email, password
        );

        Console.WriteLine("✅ Usuario registrado correctamente.");
    }

    private string LeerPassword()
    {
        string pass = "";
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && pass.Length > 0)
            {
                pass = pass[0..^1];
                Console.Write("\b \b");
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                pass += keyInfo.KeyChar;
                Console.Write("*");
            }
        } while (key != ConsoleKey.Enter);
        Console.WriteLine();
        return pass;
    }
    public async Task LoginUsuarioAsync()
    {
        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Contraseña: ");
        string password = LeerPassword();

        bool loginOk = await _usuarioService.LoginUsuarioAsync(email, password);

        if (loginOk)
            Console.WriteLine("✅ Login exitoso, bienvenido usuario!");
        else
            Console.WriteLine("❌ Credenciales incorrectas.");
    }
}