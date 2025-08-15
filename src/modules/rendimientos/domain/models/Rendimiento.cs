using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs.src.modules.rendimientos.domain.models;
public class Rendimiento
{
    public int IdRendimiento { get; set; }
    public string Nivel { get; set; } = string.Empty;
    // tiene relacion con la entidad Variedad
    public ICollection<Variedad> Variedades { get; set; } = new List<Variedad>();
    // define el constructor vacio
    public Rendimiento()
    {
    }
    // define el constructor con parametros
    public Rendimiento(string nivel)
    {
        Nivel = nivel;
    }
}
