using Microsoft.EntityFrameworkCore.Internal;
using proyecto_cs;
internal class Program
{
  private static async Task Main(string[] args)
  {
  var context = DbContextFactory.Create();
  bool salir = false;
    while (!salir)
    {
      Console.Clear();
      Console.WriteLine("\n --- Login ---");
      Console.WriteLine("1. Registrar Usuario");
      Console.WriteLine("2. Login Usuario");
      Console.WriteLine("3. Registrar Admin");
      Console.WriteLine("4. Login Admin");
      Console.WriteLine("5. Salir");
      Console.Write("Opcion: ");
      int opRegister = int.Parse(Console.ReadLine()!);

      switch (opRegister)
      {
        case 1:
          await context.Database.EnsureCreatedAsync();
          /*Console.WriteLine("Registrar Usuario");
          Console.Write("Ingrese su nombre: ");
          string nombre = Console.ReadLine()!;
          Console.Write("Ingrese su apellido: ");
          string apellido = Console.ReadLine()!;
          Console.Write("Ingrese su edad: ");
          int edad = int.Parse(Console.ReadLine()!);
          Console.Write("Ingrese su nacionalidad: ");
          string nacionalidad = Console.ReadLine()!;
          Console.Write("Ingrese su documento de identidad: ");
          int documentoIdentidad = int.Parse(Console.ReadLine()!);
          Console.Write("Ingrese su genero: ");
          string genero = Console.ReadLine()!;*/
          Console.Clear();
          break;
        case 2:
          Console.Clear();
          break;
        case 3:
          Console.Clear();
          break;
        case 4:
          Console.Clear();
          break;
        case 5:
          salir = true;
          break;
        default:
          Console.WriteLine("Opcion Invalida, vuelva a introducir una opcion correcta.");
          break;
      }
    }
  }
}