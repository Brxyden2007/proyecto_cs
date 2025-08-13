using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class Variedad
{
    public int IdVariedad { get; set; }
    public string NombreComun { get; set; } = string.Empty;
    public string NombreCientifico { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string ImagenUrl { get; set; } = string.Empty;
    public int IdPorte { get; set; }
    public int IdTamanio { get; set; }
    public int IdAltitud { get; set; }
    public int IdRendimiento { get; set; }
    public int IdCalidad { get; set; }
    // define la relacion con la entidad Porte
    public Porte Porte { get; set; } = null!;
    // define la relacion con la entidad TamanioGrano
    public TamanioGrano TamanioGrano { get; set; } = null!;
    // define la relacion con la entidad Altitud
    public Altitud Altitud { get; set; } = null!;
    // define la relacion con la entidad Rendimiento
    public Rendimiento Rendimiento { get; set; } = null!;
    // define la relacion con la entidad CalidadAltitud
    public CalidadAltitud CalidadAltitud { get; set; } = null!;
    // define la relacion con la entidad HistoriaGenetica
    public ICollection<HistoriaGenetica> HistoriasGeneticas { get; set; } = new List<HistoriaGenetica>();
    // define la relacion con la entidad AtributoAgronomico
    public ICollection<AtributoAgronomico> AtributosAgronomicos { get; set; } = new List<AtributoAgronomico>();
    // define la relacion con la entidad VariedadResistencia
    public ICollection<VariedadResistencia> VariedadResistencias { get; set; } = new List<VariedadResistencia>();
    // define el constructor vacio
    public Variedad()
    {
    }
    // define el constructor con parametros
    public Variedad(string nombreComun, string nombreCientifico, string descripcion, string imagenUrl, int idPorte, int idTamanio, int idAltitud, int idRendimiento, int idCalidad)
    {
        NombreComun = nombreComun;
        NombreCientifico = nombreCientifico;
        Descripcion = descripcion;
        ImagenUrl = imagenUrl;
        IdPorte = idPorte;
        IdTamanio = idTamanio;
        IdAltitud = idAltitud;
        IdRendimiento = idRendimiento;
        IdCalidad = idCalidad;
    }   
}
