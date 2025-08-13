using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;

public class HistoriaGenetica
{
    public int IdHistoria { get; set; }
    public int IdVariedad { get; set; }
    public string Obtentor { get; set; } = string.Empty;
    public string Familia { get; set; } = string.Empty;
    public string Grupo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    // define la relacion con la entidad Variedad
    public Variedad Variedad { get; set; } = null!;
    // define el constructor vacio
    public HistoriaGenetica()
    {
    }
    // define el constructor con parametros
    public HistoriaGenetica(int idVariedad, string obtentor, string familia, string grupo, string descripcion)
    {
        IdVariedad = idVariedad;
        Obtentor = obtentor;
        Familia = familia;
        Grupo = grupo;
        Descripcion = descripcion;
    }
}