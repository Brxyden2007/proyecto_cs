using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs.src.shared.utils;
public class Validaciones
{
  public string ValidarTexto(string? text)
  {
    // TODO: hacer que esto sea un condicionales porque si es un bucle se ejecuta constantemente el error de  
    while (string.IsNullOrWhiteSpace(text))
    {
      Console.WriteLine("error al ingresar un texto vacio, presione una tecla para continuar");
      text = Console.ReadLine();
    }
    // el trim sirve para quitar todos los caracteres de espacio en blanco iniciales y finales de la cadena actual, sirve para arreglar errores mios como copiar "hola " en lugar de "hola"
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
