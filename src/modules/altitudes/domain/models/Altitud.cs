using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class Altitud
{
    public int IdAltitud { get; set; }
    public string Rango { get; set; } = string.Empty;
    // define el constructor vacio
    public Altitud()
    {
    }
    // define el constructor con parametros
    public Altitud(string rango)
    {
        Rango = rango;
    }
}
