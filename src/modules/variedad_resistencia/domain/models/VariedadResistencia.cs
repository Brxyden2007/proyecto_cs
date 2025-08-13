using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class VariedadResistencia
{
    public int IdVariedad { get; set; }
    public int IdResistencia { get; set; }
    // define la relacion con la entidad Variedad y resistencia
    public Variedad Variedad { get; set; } = null!;
    public Resistencia Resistencia { get; set; } = null!;
    // define el constructor vacio
    public VariedadResistencia()
    {
    }
    // define el constructor con parametros
    public VariedadResistencia(int idVariedad, int idResistencia)
    {
        IdVariedad = idVariedad;
        IdResistencia = idResistencia;
    }
}