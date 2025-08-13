using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class Resistencia
{
    public int IdResistencia { get; set; }
    public string Enfermedad { get; set; } = string.Empty;
    public string Nivel { get; set; } = string.Empty;
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