using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{
    static Usuario? usuarioActual;
    static List<Usuario> usuarios = new List<Usuario>
    {
        new Usuario { Nombre = "admin", Password = "admin123", Rol = "admin" },
        new Usuario { Nombre = "user", Password = "user123", Rol = "user" }
        // Puedes agregar más usuarios de ejemplo aquí
    };
    static readonly string[] opcionesLogin =
    {
        "Registrar Usuario",
        "Login Usuario",
        "Registrar Admin",
        "Login Admin",
        "Salir"
    };
    static int opcionSeleccionadaLogin = 0;

    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

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

    // ==== Dibuja el menú de login ====
    static void DibujarMenuLogin()
    {
        Console.Clear();
        EscribirConPausa("=== --------------------------------------------------------------------  ===", 10);
        EscribirConPausa("          ░█▀▀█ ░█▀▀█ 　 ── 　 ░█─── ░█▀▀▀█ ░█▀▀█ ▀█▀ ░█▄─░█ ", 10);
        EscribirConPausa("          ░█─── ░█─── 　 ▀▀ 　 ░█─── ░█──░█ ░█─▄▄ ░█─ ░█░█░█ ", 10);
        EscribirConPausa("          ░█▄▄█ ░█▄▄█ 　 ── 　 ░█▄▄█ ░█▄▄▄█ ░█▄▄█ ▄█▄ ░█──▀█ ", 10);
        EscribirConPausa("=== --------------------------------------------------------------------  ===\n", 10);

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

    // ==== Ejecuta la opción seleccionada en login ====
    static async Task<bool> EjecutarOpcionLogin(int opcion)
    {
        switch (opcion)
        {
            case 0:
                Console.Clear();
                Console.WriteLine("📋 Registro de Usuario (pendiente de implementar)");
                Console.ReadKey();
                return false;

            case 1:
                if (Login("user"))
                {
                    await MostrarMenuPrincipal();
                }
                return false;

            case 2:
                Console.Clear();
                Console.WriteLine("📋 Registro de Admin (pendiente de implementar)");
                Console.ReadKey();
                return false;

            case 3:
                if (Login("admin"))
                {
                    await MostrarMenuPrincipal();
                }
                return false;

            case 4:
                return true; // salir

            default:
                return false;
        }
    }

    // ==== Login real ====
    static bool Login(string tipo)
    {
        Console.Clear();
        Console.WriteLine($"🔐 Login {tipo.ToUpper()}");
        Console.Write("Usuario: ");
        string nombre = Console.ReadLine()!;
        Console.Write("Contraseña: ");
        string pass = LeerPassword();

        var usuario = usuarios.Find(u => u.Nombre == nombre && u.Password == pass && u.Rol == tipo);

        if (usuario != null)
        {
            usuarioActual = usuario;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Inicio de sesión exitoso.");
            Console.ResetColor();
            Thread.Sleep(1000);
            return true;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Usuario o contraseña incorrectos.");
            Console.ResetColor();
            Console.ReadKey();
            return false;
        }
    }

    // ==== Menú principal con flechas ====
    static async Task MostrarMenuPrincipal()
    {
        var menu = new MenuPrincipal(usuarioActual);
        menu.MostrarBienvenida();
        await menu.EjecutarMenuMain();
    }

    public static void EscribirConPausa(string texto, int ms)
    {
        Console.WriteLine(texto);
        Thread.Sleep(ms);
    }

    // ==== Leer password ocultando caracteres ====
    public static string LeerPassword()
    {
        string password = "";
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Enter)
                break;
            if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                password += keyInfo.KeyChar;
                Console.Write("*");
            }
        } while (true);
        Console.WriteLine();
        return password;
    }
}

// ==== Clase Usuario ====
public class Usuario
{
    public string? Nombre { get; set; }
    public string? Password { get; set; }
    public string? Rol { get; set; }
}

// ==== Menú principal con flechas ====
public class MenuPrincipal
{
    private readonly Usuario usuario;
    private readonly string[] opcionesMenu;
    private int opcionSeleccionada = 0;

    public MenuPrincipal(Usuario usuarioActual)
    {
        usuario = usuarioActual;
        if (usuario.Rol == "admin")
        {
            opcionesMenu = new string[]
            {
                "Consultar Variedades de café",
                "Recomendar café según preferencias",
                "Ficha Técnica de café",
                "Consultar Proveedores",
                "Consultar Precios",
                "Consultar Beneficios del café",
                "Recomendaciones Para Usuarios",
                "Panel Administrativo (CRUD)",
                "Salir del programa"
            };
        }
        else
        {
            opcionesMenu = new string[]
            {
                "Consultar Variedades de café",
                "Recomendar café según preferencias",
                "Ficha Técnica de café",
                "Consultar Proveedores",
                "Consultar Precios",
                "Consultar Beneficios del café",
                "Recomendaciones Para Usuarios",
                "Salir del programa"
            };
        }
    }

  public void MostrarBienvenida()
  {
    Console.Clear();
    Program.EscribirConPausa("=====================================================", 10);
    Program.EscribirConPausa("█▀▀ █▀█ █░░ █▀█ █▀▄▀█ █▄▄ █ ▄▀█   █▀▀ █▀█ █▀▀ █▀▀ █▀▀", 10);
    Program.EscribirConPausa("█▄▄ █▄█ █▄▄ █▄█ █░▀░█ █▄█ █ █▀█   █▄▄ █▄█ █▀░ █▀░ ██▄", 10);
    Program.EscribirConPausa("    --- Catalogando el mejor café de Colombia 🌱 --- ", 10);
    Program.EscribirConPausa("=====================================================", 10);

  }

    private void DibujarMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("========== MENÚ PRINCIPAL ==========\n");
        Console.ResetColor();

        for (int i = 0; i < opcionesMenu.Length; i++)
        {
            if (i == opcionSeleccionada)
            {
                Console.ForegroundColor = ConsoleColor.Green;
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

    public async Task EjecutarMenuMain()
    {
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

    private async Task<bool> EjecutarOpcion(int opcion)
    {
        Console.Clear();
        string seleccion = opcionesMenu[opcion];

        if (seleccion.Contains("Salir"))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ¡GRACIAS POR ELEGIRNOS! 🌱🌱🌱 ");
            Console.ResetColor();
            Console.ReadKey();
            return false;
        }
        else
        {
            Console.WriteLine($"Opción seleccionada: {seleccion}");
            Console.ReadKey(true);
            return true;
        }
    }
  }