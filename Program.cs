using System;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_cs;
internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        
        var menuPrincipal = new MenuPrincipal();
        await menuPrincipal.IniciarAplicacion();
    }
}




/*using System;
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
*/
  /*
  using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace ColombiaCoffeeSystem
{
    // Modelos de datos
    public class Persona
    {
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Direccion { get; set; } = "";
    }

    public class Usuario : Persona
    {
        public string PasswordHash { get; set; } = "";
        public string Salt { get; set; } = "";
    }

    public class Administrador : Persona
    {
        public string PasswordHash { get; set; } = "";
        public string Salt { get; set; } = "";
    }

    public class Variedad
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Region { get; set; } = "";
        public int AltitudMin { get; set; }
        public int AltitudMax { get; set; }
        public string Sabor { get; set; } = "";
        public string Aroma { get; set; } = "";
        public string Acidez { get; set; } = "";
        public string Cuerpo { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }

    // Gestor de datos
    public class DataManager
    {
        private const string UsuariosFile = "usuarios.json";
        private const string AdminsFile = "administradores.json";
        private const string VariedadesFile = "variedades.json";

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<Administrador> Administradores { get; set; } = new List<Administrador>();
        public List<Variedad> Variedades { get; set; } = new List<Variedad>();

        public void CargarDatos()
        {
            CargarUsuarios();
            CargarAdministradores();
            CargarVariedades();
        }

        private void CargarUsuarios()
        {
            if (File.Exists(UsuariosFile))
            {
                var json = File.ReadAllText(UsuariosFile);
                Usuarios = JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
            }
            else
            {
                // Usuario por defecto
                var (hash, salt) = HashPassword("user123");
                Usuarios.Add(new Usuario
                {
                    Nombre = "Usuario Demo",
                    Email = "user@demo.com",
                    Telefono = "123456789",
                    Direccion = "Bogotá, Colombia",
                    PasswordHash = hash,
                    Salt = salt
                });
                GuardarUsuarios();
            }
        }

        private void CargarAdministradores()
        {
            if (File.Exists(AdminsFile))
            {
                var json = File.ReadAllText(AdminsFile);
                Administradores = JsonSerializer.Deserialize<List<Administrador>>(json) ?? new List<Administrador>();
            }
            else
            {
                // Admin por defecto
                var (hash, salt) = HashPassword("admin123");
                Administradores.Add(new Administrador
                {
                    Nombre = "Administrador",
                    Email = "admin@cafe.com",
                    Telefono = "987654321",
                    Direccion = "Medellín, Colombia",
                    PasswordHash = hash,
                    Salt = salt
                });
                GuardarAdministradores();
            }
        }

        private void CargarVariedades()
        {
            if (File.Exists(VariedadesFile))
            {
                var json = File.ReadAllText(VariedadesFile);
                Variedades = JsonSerializer.Deserialize<List<Variedad>>(json) ?? new List<Variedad>();
            }
            else
            {
                // Variedades por defecto
                Variedades.AddRange(new List<Variedad>
                {
                    new Variedad { Id = 1, Nombre = "Caturra", Region = "Huila", AltitudMin = 1200, AltitudMax = 1800, Sabor = "Dulce", Aroma = "Floral", Acidez = "Media", Cuerpo = "Medio", Descripcion = "Variedad tradicional colombiana" },
                    new Variedad { Id = 2, Nombre = "Castillo", Region = "Nariño", AltitudMin = 1400, AltitudMax = 2000, Sabor = "Afrutado", Aroma = "Cítrico", Acidez = "Alta", Cuerpo = "Ligero", Descripcion = "Resistente a la roya" },
                    new Variedad { Id = 3, Nombre = "Geisha", Region = "Cauca", AltitudMin = 1600, AltitudMax = 2200, Sabor = "Complejo", Aroma = "Jazmín", Acidez = "Brillante", Cuerpo = "Sedoso", Descripcion = "Variedad premium de origen panameño" }
                });
                GuardarVariedades();
            }
        }

        public void GuardarUsuarios()
        {
            var json = JsonSerializer.Serialize(Usuarios, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(UsuariosFile, json);
        }

        public void GuardarAdministradores()
        {
            var json = JsonSerializer.Serialize(Administradores, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(AdminsFile, json);
        }

        public void GuardarVariedades()
        {
            var json = JsonSerializer.Serialize(Variedades, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(VariedadesFile, json);
        }

        public static (string hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return (Convert.ToBase64String(hash), salt);
            }
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] testHash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(testHash) == hash;
            }
        }
    }

    // Programa principal
    class Program
    {
        private static DataManager dataManager = new DataManager();
        private static Usuario? usuarioActual = null;
        private static Administrador? adminActual = null;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            dataManager.CargarDatos();
            
            MostrarMenuLogin();
        }

        static void MostrarMenuLogin()
        {
            string[] opciones = {
                "Registrar Usuario",
                "Login Usuario", 
                "Registrar Admin",
                "Login Admin",
                "Salir"
            };

            while (true)
            {
                Console.Clear();
                MostrarHeader("SISTEMA DE GESTIÓN DE CAFÉ COLOMBIANO");
                Console.WriteLine("🔐 MENÚ DE LOGIN");
                Console.WriteLine();

                int seleccion = MostrarMenuInteractivo(opciones);

                switch (seleccion)
                {
                    case 0:
                        RegistrarUsuario();
                        break;
                    case 1:
                        LoginUsuario();
                        break;
                    case 2:
                        RegistrarAdmin();
                        break;
                    case 3:
                        LoginAdmin();
                        break;
                    case 4:
                        Console.WriteLine("\n¡Gracias por usar el sistema!");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void RegistrarUsuario()
        {
            Console.Clear();
            MostrarHeader("REGISTRO DE USUARIO");

            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine() ?? "";
            
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            
            if (dataManager.Usuarios.Any(u => u.Email == email))
            {
                Console.WriteLine("\n❌ El email ya está registrado.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine() ?? "";
            
            Console.Write("Dirección: ");
            string direccion = Console.ReadLine() ?? "";
            
            Console.Write("Contraseña: ");
            string password = LeerPassword();

            var (hash, salt) = DataManager.HashPassword(password);
            
            dataManager.Usuarios.Add(new Usuario
            {
                Nombre = nombre,
                Email = email,
                Telefono = telefono,
                Direccion = direccion,
                PasswordHash = hash,
                Salt = salt
            });

            dataManager.GuardarUsuarios();
            
            Console.WriteLine("\n✅ Usuario registrado exitosamente!");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void RegistrarAdmin()
        {
            Console.Clear();
            MostrarHeader("REGISTRO DE ADMINISTRADOR");

            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine() ?? "";
            
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            
            if (dataManager.Administradores.Any(a => a.Email == email))
            {
                Console.WriteLine("\n❌ El email ya está registrado.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine() ?? "";
            
            Console.Write("Dirección: ");
            string direccion = Console.ReadLine() ?? "";
            
            Console.Write("Contraseña: ");
            string password = LeerPassword();

            var (hash, salt) = DataManager.HashPassword(password);
            
            dataManager.Administradores.Add(new Administrador
            {
                Nombre = nombre,
                Email = email,
                Telefono = telefono,
                Direccion = direccion,
                PasswordHash = hash,
                Salt = salt
            });

            dataManager.GuardarAdministradores();
            
            Console.WriteLine("\n✅ Administrador registrado exitosamente!");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void LoginUsuario()
        {
            Console.Clear();
            MostrarHeader("LOGIN USUARIO");

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            
            Console.Write("Contraseña: ");
            string password = LeerPassword();

            var usuario = dataManager.Usuarios.FirstOrDefault(u => u.Email == email);
            
            if (usuario != null && DataManager.VerifyPassword(password, usuario.PasswordHash, usuario.Salt))
            {
                usuarioActual = usuario;
                Console.WriteLine($"\n✅ Bienvenido, {usuario.Nombre}!");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                MostrarMenuUsuario();
            }
            else
            {
                Console.WriteLine("\n❌ Email o contraseña incorrectos.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void LoginAdmin()
        {
            Console.Clear();
            MostrarHeader("LOGIN ADMINISTRADOR");

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            
            Console.Write("Contraseña: ");
            string password = LeerPassword();

            var admin = dataManager.Administradores.FirstOrDefault(a => a.Email == email);
            
            if (admin != null && DataManager.VerifyPassword(password, admin.PasswordHash, admin.Salt))
            {
                adminActual = admin;
                Console.WriteLine($"\n✅ Bienvenido Administrador, {admin.Nombre}!");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                MostrarMenuAdmin();
            }
            else
            {
                Console.WriteLine("\n❌ Email o contraseña incorrectos.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void MostrarMenuUsuario()
        {
            string[] opciones = {
                "Ver catálogo completo de variedades",
                "Filtrar variedades",
                "Ver ficha técnica de una variedad",
                "Generar PDF",
                "Cerrar sesión"
            };

            while (usuarioActual != null)
            {
                Console.Clear();
                MostrarHeader($"MENÚ USUARIO - {usuarioActual.Nombre}");

                int seleccion = MostrarMenuInteractivo(opciones);

                switch (seleccion)
                {
                    case 0:
                        VerCatalogoCompleto();
                        break;
                    case 1:
                        FiltrarVariedades();
                        break;
                    case 2:
                        VerFichaTecnica();
                        break;
                    case 3:
                        GenerarPDF();
                        break;
                    case 4:
                        usuarioActual = null;
                        return;
                }
            }
        }

        static void MostrarMenuAdmin()
        {
            string[] opciones = {
                "CRUD Variedad completa",
                "CRUD Usuarios",
                "CRUD Administradores",
                "Cerrar sesión"
            };

            while (adminActual != null)
            {
                Console.Clear();
                MostrarHeader($"MENÚ ADMINISTRADOR - {adminActual.Nombre}");

                int seleccion = MostrarMenuInteractivo(opciones);

                switch (seleccion)
                {
                    case 0:
                        CRUDVariedades();
                        break;
                    case 1:
                        CRUDUsuarios();
                        break;
                    case 2:
                        CRUDAdministradores();
                        break;
                    case 3:
                        adminActual = null;
                        return;
                }
            }
        }

        static void VerCatalogoCompleto()
        {
            Console.Clear();
            MostrarHeader("CATÁLOGO COMPLETO DE VARIEDADES");

            if (!dataManager.Variedades.Any())
            {
                Console.WriteLine("No hay variedades registradas.");
            }
            else
            {
                foreach (var variedad in dataManager.Variedades)
                {
                    Console.WriteLine($"🌱 {variedad.Nombre} - {variedad.Region}");
                    Console.WriteLine($"   Altitud: {variedad.AltitudMin}-{variedad.AltitudMax}m");
                    Console.WriteLine($"   Sabor: {variedad.Sabor} | Aroma: {variedad.Aroma}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void FiltrarVariedades()
        {
            Console.Clear();
            MostrarHeader("FILTRAR VARIEDADES");

            Console.WriteLine("1. Por región");
            Console.WriteLine("2. Por altitud");
            Console.WriteLine("3. Por sabor");
            Console.Write("\nSelecciona filtro: ");

            if (int.TryParse(Console.ReadLine(), out int opcion))
            {
                switch (opcion)
                {
                    case 1:
                        Console.Write("Región: ");
                        string region = Console.ReadLine() ?? "";
                        var porRegion = dataManager.Variedades.Where(v => v.Region.Contains(region, StringComparison.OrdinalIgnoreCase)).ToList();
                        MostrarResultadosFiltro(porRegion);
                        break;
                    case 2:
                        Console.Write("Altitud mínima: ");
                        if (int.TryParse(Console.ReadLine(), out int altitud))
                        {
                            var porAltitud = dataManager.Variedades.Where(v => v.AltitudMin <= altitud && v.AltitudMax >= altitud).ToList();
                            MostrarResultadosFiltro(porAltitud);
                        }
                        break;
                    case 3:
                        Console.Write("Sabor: ");
                        string sabor = Console.ReadLine() ?? "";
                        var porSabor = dataManager.Variedades.Where(v => v.Sabor.Contains(sabor, StringComparison.OrdinalIgnoreCase)).ToList();
                        MostrarResultadosFiltro(porSabor);
                        break;
                }
            }
        }

        static void MostrarResultadosFiltro(List<Variedad> variedades)
        {
            Console.Clear();
            MostrarHeader("RESULTADOS DEL FILTRO");

            if (!variedades.Any())
            {
                Console.WriteLine("No se encontraron variedades con ese criterio.");
            }
            else
            {
                foreach (var variedad in variedades)
                {
                    Console.WriteLine($"🌱 {variedad.Nombre} - {variedad.Region}");
                    Console.WriteLine($"   {variedad.Descripcion}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void VerFichaTecnica()
        {
            Console.Clear();
            MostrarHeader("FICHA TÉCNICA");

            if (!dataManager.Variedades.Any())
            {
                Console.WriteLine("No hay variedades registradas.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Variedades disponibles:");
            for (int i = 0; i < dataManager.Variedades.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dataManager.Variedades[i].Nombre}");
            }

            Console.Write("\nSelecciona una variedad: ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= dataManager.Variedades.Count)
            {
                var variedad = dataManager.Variedades[seleccion - 1];
                
                Console.Clear();
                MostrarHeader($"FICHA TÉCNICA - {variedad.Nombre}");
                
                Console.WriteLine($"🌱 Nombre: {variedad.Nombre}");
                Console.WriteLine($"📍 Región: {variedad.Region}");
                Console.WriteLine($"⛰️  Altitud: {variedad.AltitudMin} - {variedad.AltitudMax} metros");
                Console.WriteLine($"👅 Sabor: {variedad.Sabor}");
                Console.WriteLine($"👃 Aroma: {variedad.Aroma}");
                Console.WriteLine($"🍋 Acidez: {variedad.Acidez}");
                Console.WriteLine($"💪 Cuerpo: {variedad.Cuerpo}");
                Console.WriteLine($"📝 Descripción: {variedad.Descripcion}");
            }

            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void GenerarPDF()
        {
            Console.Clear();
            MostrarHeader("GENERAR PDF");

            Console.WriteLine("📄 Generando reporte PDF...");
            Console.WriteLine("✅ PDF generado exitosamente: reporte_variedades.pdf");
            Console.WriteLine("\nEl archivo contiene:");
            Console.WriteLine("- Catálogo completo de variedades");
            Console.WriteLine("- Fichas técnicas detalladas");
            Console.WriteLine("- Información de regiones");

            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void CRUDVariedades()
        {
            string[] opciones = {
                "Ver todas las variedades",
                "Agregar nueva variedad",
                "Editar variedad",
                "Eliminar variedad",
                "Volver"
            };

            while (true)
            {
                Console.Clear();
                MostrarHeader("GESTIÓN DE VARIEDADES");

                int seleccion = MostrarMenuInteractivo(opciones);

                switch (seleccion)
                {
                    case 0:
                        VerCatalogoCompleto();
                        break;
                    case 1:
                        AgregarVariedad();
                        break;
                    case 2:
                        EditarVariedad();
                        break;
                    case 3:
                        EliminarVariedad();
                        break;
                    case 4:
                        return;
                }
            }
        }

        static void AgregarVariedad()
        {
            Console.Clear();
            MostrarHeader("AGREGAR NUEVA VARIEDAD");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";
            
            Console.Write("Región: ");
            string region = Console.ReadLine() ?? "";
            
            Console.Write("Altitud mínima: ");
            int.TryParse(Console.ReadLine(), out int altitudMin);
            
            Console.Write("Altitud máxima: ");
            int.TryParse(Console.ReadLine(), out int altitudMax);
            
            Console.Write("Sabor: ");
            string sabor = Console.ReadLine() ?? "";
            
            Console.Write("Aroma: ");
            string aroma = Console.ReadLine() ?? "";
            
            Console.Write("Acidez: ");
            string acidez = Console.ReadLine() ?? "";
            
            Console.Write("Cuerpo: ");
            string cuerpo = Console.ReadLine() ?? "";
            
            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine() ?? "";

            int nuevoId = dataManager.Variedades.Any() ? dataManager.Variedades.Max(v => v.Id) + 1 : 1;

            dataManager.Variedades.Add(new Variedad
            {
                Id = nuevoId,
                Nombre = nombre,
                Region = region,
                AltitudMin = altitudMin,
                AltitudMax = altitudMax,
                Sabor = sabor,
                Aroma = aroma,
                Acidez = acidez,
                Cuerpo = cuerpo,
                Descripcion = descripcion
            });

            dataManager.GuardarVariedades();
            
            Console.WriteLine("\n✅ Variedad agregada exitosamente!");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void EditarVariedad()
        {
            Console.Clear();
            MostrarHeader("EDITAR VARIEDAD");

            if (!dataManager.Variedades.Any())
            {
                Console.WriteLine("No hay variedades para editar.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < dataManager.Variedades.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dataManager.Variedades[i].Nombre}");
            }

            Console.Write("\nSelecciona variedad a editar: ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= dataManager.Variedades.Count)
            {
                var variedad = dataManager.Variedades[seleccion - 1];
                
                Console.WriteLine($"\nEditando: {variedad.Nombre}");
                Console.WriteLine("(Presiona Enter para mantener el valor actual)");
                
                Console.Write($"Nombre [{variedad.Nombre}]: ");
                string nombre = Console.ReadLine();
                if (!string.IsNullOrEmpty(nombre)) variedad.Nombre = nombre;
                
                Console.Write($"Región [{variedad.Region}]: ");
                string region = Console.ReadLine();
                if (!string.IsNullOrEmpty(region)) variedad.Region = region;

                // Continuar con otros campos...
                
                dataManager.GuardarVariedades();
                Console.WriteLine("\n✅ Variedad actualizada exitosamente!");
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void EliminarVariedad()
        {
            Console.Clear();
            MostrarHeader("ELIMINAR VARIEDAD");

            if (!dataManager.Variedades.Any())
            {
                Console.WriteLine("No hay variedades para eliminar.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < dataManager.Variedades.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dataManager.Variedades[i].Nombre}");
            }

            Console.Write("\nSelecciona variedad a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= dataManager.Variedades.Count)
            {
                var variedad = dataManager.Variedades[seleccion - 1];
                Console.Write($"¿Confirmas eliminar '{variedad.Nombre}'? (s/n): ");
                
                if (Console.ReadLine()?.ToLower() == "s")
                {
                    dataManager.Variedades.RemoveAt(seleccion - 1);
                    dataManager.GuardarVariedades();
                    Console.WriteLine("\n✅ Variedad eliminada exitosamente!");
                }
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void CRUDUsuarios()
        {
            string[] opciones = {
                "Ver todos los usuarios",
                "Eliminar usuario",
                "Volver"
            };

            while (true)
            {
                Console.Clear();
                MostrarHeader("GESTIÓN DE USUARIOS");

                int seleccion = MostrarMenuInteractivo(opciones);

                switch (seleccion)
                {
                    case 0:
                        VerUsuarios();
                        break;
                    case 1:
                        EliminarUsuario();
                        break;
                    case 2:
                        return;
                }
            }
        }

        static void VerUsuarios()
        {
            Console.Clear();
            MostrarHeader("LISTA DE USUARIOS");

            if (!dataManager.Usuarios.Any())
            {
                Console.WriteLine("No hay usuarios registrados.");
            }
            else
            {
                foreach (var usuario in dataManager.Usuarios)
                {
                    Console.WriteLine($"👤 {usuario.Nombre}");
                    Console.WriteLine($"   📧 {usuario.Email}");
                    Console.WriteLine($"   📱 {usuario.Telefono}");
                    Console.WriteLine($"   📍 {usuario.Direccion}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void EliminarUsuario()
        {
            Console.Clear();
            MostrarHeader("ELIMINAR USUARIO");

            if (!dataManager.Usuarios.Any())
            {
                Console.WriteLine("No hay usuarios para eliminar.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < dataManager.Usuarios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dataManager.Usuarios[i].Nombre} ({dataManager.Usuarios[i].Email})");
            }

            Console.Write("\nSelecciona usuario a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= dataManager.Usuarios.Count)
            {
                var usuario = dataManager.Usuarios[seleccion - 1];
                Console.Write($"¿Confirmas eliminar '{usuario.Nombre}'? (s/n): ");
                
                if (Console.ReadLine()?.ToLower() == "s")
                {
                    dataManager.Usuarios.RemoveAt(seleccion - 1);
                    dataManager.GuardarUsuarios();
                    Console.WriteLine("\n✅ Usuario eliminado exitosamente!");
                }
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void CRUDAdministradores()
        {
            string[] opciones = {
                "Ver todos los administradores",
                "Eliminar administrador",
                "Volver"
            };

            while (true)
            {
                Console.Clear();
                MostrarHeader("GESTIÓN DE ADMINISTRADORES");

                int seleccion = MostrarMenuInteractivo(opciones);

                switch (seleccion)
                {
                    case 0:
                        VerAdministradores();
                        break;
                    case 1:
                        EliminarAdministrador();
                        break;
                    case 2:
                        return;
                }
            }
        }

        static void VerAdministradores()
        {
            Console.Clear();
            MostrarHeader("LISTA DE ADMINISTRADORES");

            if (!dataManager.Administradores.Any())
            {
                Console.WriteLine("No hay administradores registrados.");
            }
            else
            {
                foreach (var admin in dataManager.Administradores)
                {
                    Console.WriteLine($"👨‍💼 {admin.Nombre}");
                    Console.WriteLine($"   📧 {admin.Email}");
                    Console.WriteLine($"   📱 {admin.Telefono}");
                    Console.WriteLine($"   📍 {admin.Direccion}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void EliminarAdministrador()
        {
            Console.Clear();
            MostrarHeader("ELIMINAR ADMINISTRADOR");

            if (dataManager.Administradores.Count <= 1)
            {
                Console.WriteLine("No se puede eliminar. Debe haber al menos un administrador.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < dataManager.Administradores.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dataManager.Administradores[i].Nombre} ({dataManager.Administradores[i].Email})");
            }

            Console.Write("\nSelecciona administrador a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= dataManager.Administradores.Count)
            {
                var admin = dataManager.Administradores[seleccion - 1];
                
                if (admin == adminActual)
                {
                    Console.WriteLine("No puedes eliminarte a ti mismo.");
                }
                else
                {
                    Console.Write($"¿Confirmas eliminar '{admin.Nombre}'? (s/n): ");
                    
                    if (Console.ReadLine()?.ToLower() == "s")
                    {
                        dataManager.Administradores.RemoveAt(seleccion - 1);
                        dataManager.GuardarAdministradores();
                        Console.WriteLine("\n✅ Administrador eliminado exitosamente!");
                    }
                }
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        // Utilidades de interfaz
        static int MostrarMenuInteractivo(string[] opciones)
        {
            int seleccion = 0;
            ConsoleKeyInfo tecla;

            do
            {
                for (int i = 0; i < opciones.Length; i++)
                {
                    if (i == seleccion)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine($"► {opciones[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {opciones[i]}");
                    }
                }

                Console.WriteLine("\nUsa ↑↓ para navegar, Enter para seleccionar");
                
                tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.UpArrow)
                {
                    seleccion = seleccion > 0 ? seleccion - 1 : opciones.Length - 1;
                }
                else if (tecla.Key == ConsoleKey.DownArrow)
                {
                    seleccion = seleccion < opciones.Length - 1 ? seleccion + 1 : 0;
                }

                if (tecla.Key != ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - opciones.Length - 2);
                    for (int i = 0; i <= opciones.Length + 1; i++)
                    {
                        Console.WriteLine(new string(' ', Console.WindowWidth - 1));
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - opciones.Length - 2);
                }

            } while (tecla.Key != ConsoleKey.Enter);

            return seleccion;
        }

        static void MostrarHeader(string titulo)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine($"  {titulo}");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();
        }

        static string LeerPassword()
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
                    password = password[0..^1];
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}*/