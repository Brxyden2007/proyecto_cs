using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs.src.modules.calidades_altitudes.domain.models;
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
