using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs.src.shared.utils;
public class Validaciones
{
  public string ValidarTexto(string? text)
  {
    do
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        Console.WriteLine("Error al ingresar un texto vacío. Presione una tecla para continuar...");
        Console.ReadKey(); // pausa hasta que el usuario presione una tecla
        Console.Write("Ingrese de nuevo el valor solicitado: ");
        text = Console.ReadLine();
      }
      else
      {
        break; // si es válido, sale del ciclo
      }
    } while (true);
    return text.Trim();
  }
  public int ValidarEntero(string? num)
  {
    int resultado = 0;
    while (!int.TryParse(num, out resultado))
    {
      Console.WriteLine("error al tratar de ingresar un valor entero");
      num = Console.ReadLine();
    }
    return resultado;
  }
  public float ValidarDecimal(string? valor_decimal)
  {
    float resultado;
    while (!float.TryParse(valor_decimal, out resultado))
    {
      Console.WriteLine("error al tratar de ingresar un valor decimal valido");
      valor_decimal = Console.ReadLine();
    }
    return resultado;
  }
  public bool ValidarBoleano(string? boleano)
  {
    while (true)
    {
      if (string.IsNullOrWhiteSpace(boleano))
      {
        Console.WriteLine("error al tratar de ingresar una respuesta válida (s/n): ");
        boleano = Console.ReadLine();
        continue;
      }
      // validar que la entrar evite espacios y sea en minuscula
      boleano = boleano.Trim().ToLower();

      if (boleano == "si" || boleano == "s")
        return true;

      if (boleano == "no" || boleano == "n")
        return false;

      Console.WriteLine("error al tratar de validar un valor boleano");
      boleano = Console.ReadLine();
    }
  }
  public DateTime ValidarFecha(string? fecha)
  {
    DateTime resultado;
    while (!DateTime.TryParse(fecha, out resultado))
    {
      Console.WriteLine("error al tratar de ingresar una fecha valida");
      fecha = Console.ReadLine();
    }
    return resultado;
  }
}
