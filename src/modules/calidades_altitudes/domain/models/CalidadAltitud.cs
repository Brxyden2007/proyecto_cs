using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class CalidadAltitud
{
    public int IdCalidadAltitud { get; set; }
    public string Nivel { get; set; } = string.Empty;
    // tiene relacion con la entidad Variedad
    public ICollection<Variedad> Variedades { get; set; } = new List<Variedad>();
    // define el constructor vacio
    public CalidadAltitud()
    {
    }
    // define el constructor con parametros
    public CalidadAltitud(string nivel)
    {
        Nivel = nivel;
    }
}
