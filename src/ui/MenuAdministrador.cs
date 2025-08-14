using System;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace ProyectoCS
{
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
                "Consultar Variedades de café",
                "Recomendar café según preferencias",
                "Ficha Técnica de café", 
                "Consultar Proveedores",
                "Consultar Precios",
                "Consultar Beneficios del café",
                "Recomendaciones Para Usuarios",
                "Panel Administrativo (CRUD)",
                "Gestión de Usuarios",
                "Reportes y Estadísticas",
                "Volver al menú principal"
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
            MenuPrincipal.EscribirConPausa("=====================================================", 10);
            MenuPrincipal.EscribirConPausa("▄▀█ █▀▄ █▀▄▀█ █ █▄░█ █ █▀ ▀█▀ █▀█ ▄▀█ █▀▄ █▀█ █▀█", 10);
            MenuPrincipal.EscribirConPausa("█▀█ █▄▀ █░▀░█ █ █░▀█ █ ▄█ ░█░ █▀▄ █▀█ █▄▀ █▄█ █▀▄", 10);
            MenuPrincipal.EscribirConPausa($"    Bienvenido/a Admin {administrador.Nombre} {administrador.Apellido} 👑", 10);
            MenuPrincipal.EscribirConPausa($"    Email: {administrador.Email}", 10);
            MenuPrincipal.EscribirConPausa("=====================================================", 10);
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void DibujarMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("========== MENÚ ADMINISTRADOR ==========\n");
            Console.ResetColor();

            for (int i = 0; i < opcionesMenu.Length; i++)
            {
                if (i == opcionSeleccionada)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"👑 {opcionesMenu[i]}");
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

            if (seleccion.Contains("Volver"))
            {
                return false; // Volver al menú principal
            }
            else
            {
                Console.WriteLine($"Opción seleccionada: {seleccion}");
                Console.WriteLine("(Funcionalidad pendiente de implementar)");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return true;
            }
        }
    }
}
