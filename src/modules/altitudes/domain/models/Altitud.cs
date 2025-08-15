using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs.src.modules.altitudes.domain.models;
public class Altitud
{
    public int IdAltitud { get; set; }
    public string Rango { get; set; } = string.Empty;
    // tiene relacion con la entidad Variedad
    public ICollection<Variedad> Variedades { get; set; } = new List<Variedad>();
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
