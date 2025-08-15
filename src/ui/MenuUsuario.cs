using System;
using System.Threading.Tasks;

namespace proyecto_cs;
public class MenuUsuario
{
    private readonly proyecto_cs.Usuario usuario;
    private readonly string[] opcionesMenu;
    private int opcionSeleccionada = 0;

    public MenuUsuario(proyecto_cs.Usuario usuarioActual)
    {
        usuario = usuarioActual;
        opcionesMenu = new string[]
        {
            "Ver catalogo completo de variedades",
            "Filtrar variedades",
            "Ver ficha técnica de una variedad",
            "Generar PDF",
            "Salir/Cerrar Sesion"

        };
    }

    public async Task MostrarMenu()
    {
        MostrarBienvenidaUsuario();
        
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

    private void MostrarBienvenidaUsuario()
    {
        Console.Clear();
        MenuPrincipal.EscribirConPausa("===============================================================", 10);
        MenuPrincipal.EscribirConPausa("  ▒█▀▀█ ▀█▀ ▒█▀▀▀ ▒█▄░▒█ ▒█░░▒█ ▒█▀▀▀ ▒█▄░▒█ ▀█▀ ▒█▀▀▄ ▒█▀▀▀█",10); 
        MenuPrincipal.EscribirConPausa("  ▒█▀▀▄ ▒█░ ▒█▀▀▀ ▒█▒█▒█ ░▒█▒█░ ▒█▀▀▀ ▒█▒█▒█ ▒█░ ▒█░▒█ ▒█░░▒█",10); 
        MenuPrincipal.EscribirConPausa("  ▒█▄▄█ ▄█▄ ▒█▄▄▄ ▒█░░▀█ ░░▀▄▀░ ▒█▄▄▄ ▒█░░▀█ ▄█▄ ▒█▄▄▀ ▒█▄▄▄█",10);
        MenuPrincipal.EscribirConPausa($"    Usuario {usuario.Nombre} {usuario.Apellido} 🌱!", 10);
        MenuPrincipal.EscribirConPausa("===============================================================", 10);
        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private void DibujarMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("========== MENÚ USUARIO ==========\n");
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

    private Task<bool> EjecutarOpcion(int opcion)
    {
        Console.Clear();
        string seleccion = opcionesMenu[opcion];

        if (seleccion.Contains("Salir"))
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