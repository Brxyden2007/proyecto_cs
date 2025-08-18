using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.shared.utils.pdf;

namespace proyecto_cs;
public class MenuPdf
{
  private readonly string[] opcionesMenu =
  [
      "Generar pdf detallado de una variedad",
      "Generar pdf detallado de todas las variedades",
      "Generar pdf con solo los atributos agronomicos de todas las variedades",
      "Generar pdf con solo los historia genetica de todas las variedades",
      "Generar PDF resumido (Solo con nombre, imagen y características principales.)",
      "Generar PDF de un usuario",
      "Generar reporte administrativo de usuarios",
      "Salir"
  ];

  // se declara la variable que se va a utilizar para el menu principal
  private int opcionSeleccionada = 0;
  // este es el metodo del menu principal en la consola con las flechas de arriba y abajo
  private void DibujarMenu()
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("========== MENÚ DE PDF ==========\n");
    Console.ResetColor();
    // este ciclo se encarga de dibujar las opciones del menu principal de acuerdo a la opcion seleccioada, recorriendo el arreglo de opcionesMenu definidco previamente
    for (int i = 0; i < opcionesMenu.Length; i++)
    {
      if (i == opcionSeleccionada)
      {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"▶ {opcionesMenu[i]}");
        Console.ResetColor();
      }
      else
      {
        Console.WriteLine($"  {opcionesMenu[i]}");
      }
    }
    Console.WriteLine("\nUsa las flechas ↑ ↓ para moverte y Enter para seleccionar.");
  }
  // este es el metodo que se encarga de ejecutar el menu principal, donde se va a manejar la logica del menu y de los inputs
  public async Task EjecutarMenuMain()
  {
    bool validate_program = true;
    do
    {
      DibujarMenu();
      // lee la tecla presionada por el usuario
      var tecla_input = Console.ReadKey(true);

      switch (tecla_input.Key)
      {
        // si es la flecha hacia arriba se decrementa la opcion seleccionada
        case ConsoleKey.UpArrow:
          opcionSeleccionada--;
          // si la opcion seleccionada es menor a 0, se asigna el ultimo elemento del arreglo de opcionesMenu
          if (opcionSeleccionada < 0) opcionSeleccionada = opcionesMenu.Length - 1;
          break;
        // si es la flecha hacia abajo se aumenta la opcion seleccionada en el arreglo de opcionesMenu
        case ConsoleKey.DownArrow:
          opcionSeleccionada++;
          // si la opcion seleccionada es mayor o igual al largo del arreglo de opcionesMenu, se asigna 0
          if (opcionSeleccionada >= opcionesMenu.Length) opcionSeleccionada = 0;
          break;
        // si se preisona Enter se ejecuta el metodo de EjecutarOpcion con la opcion seleccionada
        case ConsoleKey.Enter:
          validate_program = await EjecutarOpcion(opcionSeleccionada);
          break;
      }
    } while (validate_program);
    // Console.WriteLine("\nPresiona cualquier tecla para cerrar...");
    // Console.ReadKey();
  }
  // este metodo se encarga de ejecutar las opciones del menu principal, y se trabaja con boolean para determinar si se debe de continuar o no con el programa, es por eso que esta el Console.ReadKey(true), para que el programa espere a que el usuario precione una tecla antes de continua
  private static Task<bool> EjecutarOpcion(int opcion_seleccionada)
  {
    Console.Clear();
    switch (opcion_seleccionada)
    {
      case 0:
        Console.Write("Ingrese el id de la variedad que desea crear el pdf: ");
        var idVariedad = int.Parse(Console.ReadLine() ?? "0");
        var context1 = DbContextFactory.Create();

        var variedadPdfGenerator = new VariedadPdfGenerator();
        variedadPdfGenerator.Compose(context1, idVariedad);
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 1:
        var context2 = DbContextFactory.Create();
        VariedadesTodasPdfGenerator variedadesTodasPdfGenerator = new VariedadesTodasPdfGenerator(context2);
        variedadesTodasPdfGenerator.GenerateAll(context2);

        Console.ReadKey(true);
        return Task.FromResult(true);
      case 2:
        // var context3 = DbContextFactory.Create();
        // VariedadAtributosPdfGenerator variedadAtributosPdfGenerator = new VariedadAtributosPdfGenerator();
        // variedadAtributosPdfGenerator.Compose(context3);
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 3:
        // var context4 = DbContextFactory.Create();
        // VariedadHistoriaPdfGenerator variedadHistoriaPdfGenerator = new VariedadHistoriaPdfGenerator();
        // variedadHistoriaPdfGenerator.Compose(context4);
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 4:
        // var context5 = DbContextFactory.Create();
        // VariedadResumidaPdfGenerator variedadResumidaPdfGenerator = new VariedadResumidaPdfGenerator();
        // variedadResumidaPdfGenerator.Compose(context5);
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 5: // UsuarioPdf
        Console.WriteLine("Ingrese el ID del usuario:");
        var userId = int.Parse(Console.ReadLine() ?? "0");
        var ctxU = DbContextFactory.Create();
        var usuario = ctxU.Usuarios.FirstOrDefault(u => u.Id == userId);

        if (usuario != null)
        {
          var pdfUsuario = new UsuarioPdfGenerator(usuario).Generate();
          File.WriteAllBytes("Usuario.pdf", pdfUsuario);
          Console.WriteLine("✅ PDF de usuario generado con éxito.");
        }
        else
        {
          Console.WriteLine("❌ Usuario no encontrado.");
        }
        Console.ReadKey(true);
        return Task.FromResult(true);

      case 6: // ReporteAdminPdf
        var ctxR = DbContextFactory.Create();
        var usuarios = ctxR.Usuarios.ToList();
        var pdfReporte = new ReporteAdminPdfGenerator(usuarios).Generate();
        File.WriteAllBytes("ReporteUsuarios.pdf", pdfReporte);
        Console.WriteLine("✅ Reporte administrativo generado con éxito.");
        Console.ReadKey(true);
        return Task.FromResult(true);

      case 7:
        return Task.FromResult(false);
      default:
        Console.Clear();
        Console.WriteLine("error al ingresar dato, intentelo de nuevo");
        Console.ReadKey(true);
        return Task.FromResult(true);
    }
  }
}
