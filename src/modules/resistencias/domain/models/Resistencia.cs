using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedad_resistencia.domain.models;

namespace proyecto_cs.src.modules.resistencias.domain.models;
public class Resistencia
{
    public int IdResistencia { get; set; }
    public string Enfermedad { get; set; } = string.Empty;
    public string Nivel { get; set; } = string.Empty;
    // tiene relacion cona la entidad VariedadResistencia
    public ICollection<VariedadResistencia> VariedadResistencias { get; set; } = new List<VariedadResistencia>();
    // define el constructor vacio
    public Resistencia()
    {
    }
    // define el constructor con parametros
    public Resistencia(string nivel, string enfermedad)
    {
        Enfermedad = enfermedad;
        Nivel = nivel;
    }
}