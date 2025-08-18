using System;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs;

public class MenuAdministrador
{
    private readonly Administrador administrador;
    private readonly string[] opcionesMenu;
    private int opcionSeleccionada = 0;
    
    public MenuAdministrador(Administrador administradorActual)
    {
        administrador = administradorActual;
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
        Console.WriteLine("==========================================\n");
        Console.WriteLine("▄▀█ █▀▄ █▀▄▀█ █ █▄░█   █▀▄▀█ █▀▀ █▄░█ █░█");
        Console.WriteLine("█▀█ █▄▀ █░▀░█ █ █░▀█   █░▀░█ ██▄ █░▀█ █▄█");
        Console.WriteLine("==========================================\n");
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
        Console.Clear();
        string seleccion = opcionesMenu[opcion];
        
        switch (opcion)
        {
            case 0: // CRUD variedad completa
                MostrarMenuVariedades();
                break;
                
            case 1: // CRUD usuarios
                MostrarMenuUsuarios();
                break;
                
            case 2: // CRUD administradores
                MostrarMenuAdministradores();
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

    private void MostrarMenuVariedades()
    {
        bool continuar = true;
        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("║        GESTIÓN DE VARIEDADES         ║");
            Console.WriteLine("║       🌱  CAFÉ COLOMBIANO  🌱       ║");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Crear Variedad");
            Console.WriteLine("2. Listar Variedades");
            Console.WriteLine("3. Actualizar Variedad");
            Console.WriteLine("4. Eliminar Variedad");
            Console.WriteLine("5. Volver al menú principal");
            
            Console.Write("Selecciona una opción: ");
            string opcion = Console.ReadLine()!;
            
            switch (opcion)
            {
                case "1":
                    Console.WriteLine("📋Creando nueva variedad de café...");
                    // Aquí implementarías la lógica de crear
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("n✅Listando variedades...");
                    Console.WriteLine("1. Caturra - Porte Bajo - Alto Rendimiento");
                    Console.WriteLine("2. Típica - Porte Alto - Medio Rendimiento");
                    Console.WriteLine("3. Variedad Colombia - Porte Medio - Excepcional");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Actualizando variedad...");
                    // Aquí implementarías la lógica de actualizar
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("❌Eliminando variedad...");
                    // Aquí implementarías la lógica de eliminar
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "5":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("❌Opción no válida. Presiona cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void MostrarMenuUsuarios()
    {
        bool continuar = true;
        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("║      🌱  GESTIÓN DE UNIDADES  🌱    ║");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("╚══════════════════════════════════════╝");   
            Console.WriteLine();
            Console.WriteLine("1. Crear Usuario");
            Console.WriteLine("2. Listar Usuarios");
            Console.WriteLine("3. Actualizar Usuario");
            Console.WriteLine("4. Eliminar Usuario");
            Console.WriteLine("5. Volver al menú principal");
            
            Console.Write("Selecciona una opción: ");
            string opcion = Console.ReadLine()!;
            
            switch (opcion)
            {
                case "1":
                    Console.WriteLine("📋Creando nuevo usuario...");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("✅Listando usuarios...");
                    Console.WriteLine("1. Juan Pérez - Agricultor - Huila");
                    Console.WriteLine("2. María García - Técnico - Nariño");
                    Console.WriteLine("3. Carlos López - Exportador - Caldas");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Actualizando usuario...");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("❌Eliminando usuario...");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "5":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("❌Opción no válida. Presiona cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void MostrarMenuAdministradores()
    {
        bool continuar = true;
        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║--------------------------------------║");
            Console.WriteLine("║   🌱 GESTIÓN DE ADMINISTRADORES 🌱  ║");
            Console.WriteLine("║--------------------------------------║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Crear Administrador");
            Console.WriteLine("2. Listar Administradores");
            Console.WriteLine("3. Actualizar Administrador");
            Console.WriteLine("4. Eliminar Administrador");
            Console.WriteLine("5. Volver al menú principal");
            
            Console.Write("Selecciona una opción: ");
            string opcion = Console.ReadLine()!;
            
            switch (opcion)
            {
                case "1":
                    Console.WriteLine("📋Creando nuevo administrador...");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("✅Listando administradores...");
                    Console.WriteLine("1. Admin Principal - Nivel 5 - Sistemas");
                    Console.WriteLine("2. Ana Rodríguez - Nivel 3 - Contenido");
                    Console.WriteLine("3. Luis Martínez - Nivel 4 - Operaciones");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Actualizando administrador...");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("❌Eliminando administrador...");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "5":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("❌Opción no válida. Presiona cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}