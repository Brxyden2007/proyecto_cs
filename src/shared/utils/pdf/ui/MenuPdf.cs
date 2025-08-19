using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.shared.utils;
using proyecto_cs.src.shared.utils.pdf;

namespace proyecto_cs;
public class MenuPdf
{
  private static Validaciones validate_data = new Validaciones();  
  private readonly string[] opcionesMenu =
  [
      "Generar pdf detallado de una variedad",
      "Generar pdf detallado de todas las variedades",
      "Generar pdf con ficha tecnica (historia genética y atributos agronómicos) de una variedad",
      "Generar pdf con ficha tecnica (historia genética y atributos agronómicos) de todas las variedades",
      "Generar reporte administrativo de administradores",
      "Generar reporte administrativo de usuarios",
      "Regresar al menu principal"
  ];

  // se declara la variable que se va a utilizar para el menu principal
  private int opcionSeleccionada = 0;
  // este es el metodo del menu principal en la consola con las flechas de arriba y abajo
  private void DibujarMenu()
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(@"
      ███╗░░░███╗███████╗███╗░░██╗██╗░░░██╗  ██████╗░███████╗  ██████╗░██████╗░███████╗
      ████╗░████║██╔════╝████╗░██║██║░░░██║  ██╔══██╗██╔════╝  ██╔══██╗██╔══██╗██╔════╝
      ██╔████╔██║█████╗░░██╔██╗██║██║░░░██║  ██║░░██║█████╗░░  ██████╔╝██║░░██║█████╗░░
      ██║╚██╔╝██║██╔══╝░░██║╚████║██║░░░██║  ██║░░██║██╔══╝░░  ██╔═══╝░██║░░██║██╔══╝░░
      ██║░╚═╝░██║███████╗██║░╚███║╚██████╔╝  ██████╔╝███████╗  ██║░░░░░██████╔╝██║░░░░░
      ╚═╝░░░░░╚═╝╚══════╝╚═╝░░╚══╝░╚═════╝░  ╚═════╝░╚══════╝  ╚═╝░░░░░╚═════╝░╚═╝░░░░░
    ");
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
        var context1 = DbContextFactory.Create();
        Console.WriteLine("Variedades en BD:");
        foreach (var v in context1.Variedades)
        {
          Console.WriteLine($"{v.IdVariedad}. {v.NombreComun}");
        }
        // pedir el id de la variedad
        Console.Write("Ingrese el id de la variedad que desea crear el pdf de la ficha tecnica: ");
        var idVariedad1 = validate_data.ValidarEntero(Console.ReadLine());

        // crear el pdf
        var variedadPdfGenerator = new VariedadPdfGenerator();
                _ = variedadPdfGenerator.Compose(context1, idVariedad1);
        // mostrar la ruta donde se guarda el pdf en caso de que haya sido creado
        var ruta1 = Path.Combine(Directory.GetCurrentDirectory(), $"variedad_{idVariedad1}.pdf");
        Console.WriteLine($"✅ PDF generado en: {ruta1}");
        // abrir el pdf en el explorador de archivos
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
          FileName = ruta1,
          UseShellExecute = true
        });
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 1:
        var context2 = DbContextFactory.Create();
        // convoca el pdf de las fichas tecnicas de todas las variedades
        var variedadesTodasPdfGenerator = new VariedadesTodasPdfGenerator();
          _ = variedadesTodasPdfGenerator.Compose(context2);
        // mostrar la ruta donde se guarda el pdf en caso de que haya sido creado
        var ruta2 = Path.Combine(Directory.GetCurrentDirectory(), $"Variedades_Todos.pdf");
        Console.WriteLine($"✅ PDF generado en: {ruta2}");
        // abrir el pdf en el explorador de archivos
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
          FileName = ruta2,
          UseShellExecute = true
        });
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 2:
        var context3 = DbContextFactory.Create();
        Console.WriteLine("Variedades en BD:");
        foreach (var v in context3.Variedades)
        {
          Console.WriteLine($"{v.IdVariedad}. {v.NombreComun}");
        }
        // pedir el id de la variedad
        Console.Write("Ingrese el id de la variedad que desea crear el pdf de la ficha tecnica: ");
        var idVariedad3 = validate_data.ValidarEntero(Console.ReadLine());

        // crear el pdf
        var fichaTecnicaPdfGenerator = new FichaTecnicaPdfGenerator();
                _ = fichaTecnicaPdfGenerator.Compose(context3, idVariedad3);
        // mostrar la ruta donde se guarda el pdf en caso de que haya sido creado
        var ruta3 = Path.Combine(Directory.GetCurrentDirectory(), $"Ficha_Tecnica_{idVariedad3}.pdf");
        Console.WriteLine($"✅ PDF generado en: {ruta3}");
        // abrir el pdf en el explorador de archivos
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
          FileName = ruta3,
          UseShellExecute = true
        });
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 3:
        var context4 = DbContextFactory.Create();
        // convoca el pdf de las fichas tecnicas de todas las variedades
        var pdfFichasTecnicasTodasPdfGenerator = new FichasTecnicasTodasPdfGenerator();
          _ = pdfFichasTecnicasTodasPdfGenerator.Compose(context4);
        // mostrar la ruta donde se guarda el pdf en caso de que haya sido creado
        var ruta4 = Path.Combine(Directory.GetCurrentDirectory(), $"Fichas_Tecnicas_Todos.pdf");
        Console.WriteLine($"✅ PDF generado en: {ruta4}");
        // abrir el pdf en el explorador de archivos
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
          FileName = ruta4,
          UseShellExecute = true
        });
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 4:
        var context5 = DbContextFactory.Create();
      // convoa el pdf de los administradores
        var reporteAdminPdfGenerator = new ReporteAdminPdfGenerator();
          _ = reporteAdminPdfGenerator.Compose(context5);
        // mostrar la ruta donde se guarda el pdf en caso de que haya sido creado
        var ruta5 = Path.Combine(Directory.GetCurrentDirectory(), $"ReporteAdmin.pdf");
        Console.WriteLine($"✅ PDF generado en: {ruta5}");
        // abrir el pdf en el explorador de archivos
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
          FileName = ruta5,
          UseShellExecute = true
        });
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 5:
        var context6 = DbContextFactory.Create();
        // // convoca el pdf de los usuarios 
        // var usuarioPdfGenerator = new UsuarioPdfGenerator();
        //   _ = usuarioPdfGenerator.Compose(context6);
        // // mostrar la ruta donde se guarda el pdf en caso de que haya sido creado
        // var ruta6 = Path.Combine(Directory.GetCurrentDirectory(), $"Usuarios.pdf");
        // Console.WriteLine($"✅ PDF generado en: {ruta6}");
        // // abrir el pdf en el explorador de archivos
        // System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        // {
        //   FileName = ruta6,
        //   UseShellExecute = true
        // });
        Console.ReadKey(true);
        return Task.FromResult(true);
      case 6: // Regresar
        return Task.FromResult(false);
      default:
        Console.Clear();
        Console.WriteLine("error al ingresar dato, intentelo de nuevo");
        Console.ReadKey(true);
        return Task.FromResult(true);
    }
  }
}
