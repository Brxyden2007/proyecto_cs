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
        MenuPrincipal.EscribirConPausa("  ▒█▀▀█ ▀█▀ ▒█▀▀▀ ▒█▄░▒█ ▒█░░▒█ ▒█▀▀▀ ▒█▄░▒█ ▀█▀ ▒█▀▀▄ ▒█▀▀▀█",10); 
        MenuPrincipal.EscribirConPausa("  ▒█▀▀▄ ▒█░ ▒█▀▀▀ ▒█▒█▒█ ░▒█▒█░ ▒█▀▀▀ ▒█▒█▒█ ▒█░ ▒█░▒█ ▒█░░▒█",10); 
        MenuPrincipal.EscribirConPausa("  ▒█▄▄█ ▄█▄ ▒█▄▄▄ ▒█░░▀█ ░░▀▄▀░ ▒█▄▄▄ ▒█░░▀█ ▄█▄ ▒█▄▄▀ ▒█▄▄▄█",10);
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
        Console.WriteLine("▄▀█ █▀▄ █▀▄▀█ █ █▄░█   █▀▄▀█ █▀▀ █▄░█ █░█");
        Console.WriteLine("█▀█ █▄▀ █░▀░█ █ █░▀█   █░▀░█ ██▄ █░▀█ █▄█");
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

    private Task<bool> EjecutarOpcion(int opcion)
    {
        Console.Clear();
        string seleccion = opcionesMenu[opcion];

        if (seleccion.Contains("Salir")) // Se implementa esto para que cuando el usuario quiera salir, aquella palabra reservada "Salir" se ejecute y salga del menu
        {
            return Task.FromResult(false); // Volver al menú principal
        }
        else
        {
            Console.WriteLine($"Opción seleccionada: {seleccion}");
            Console.WriteLine("(Funcionalidad pendiente de implementar)");
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
            return Task.FromResult(true);
        }
    }
}