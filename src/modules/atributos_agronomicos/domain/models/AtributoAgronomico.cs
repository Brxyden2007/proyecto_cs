using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs.src.modules.atributos_agronomicos.domain.models;
public class AtributoAgronomico
{
    public int IdAtributo { get; set; }
    public int IdVariedad { get; set; }
    public string TiempoCosecha { get; set; } = string.Empty;
    public string Maduracion { get; set; } = string.Empty;
    public string Nutricion { get; set; } = string.Empty;
    public string DensidadSiembra { get; set; } = string.Empty;
    // define la relacion con la entidad Variedad
    public Variedad Variedad { get; set; } = null!;
    // define el constructor vacio
    public AtributoAgronomico()
    {
    }
    // define el constructor con parametros
    public AtributoAgronomico(int idVariedad, string tiempoCosecha, string maduracion, string nutricion, string densidadSiembra)
    {
        IdVariedad = idVariedad;
        TiempoCosecha = tiempoCosecha;
        Maduracion = maduracion;
        Nutricion = nutricion;
        DensidadSiembra = densidadSiembra;
    }
}