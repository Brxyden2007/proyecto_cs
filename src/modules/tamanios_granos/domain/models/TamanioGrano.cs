using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class TamanioGrano
{
    public int IdTamanio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    // tiene relacion con la entidad Variedad
    public ICollection<Variedad> Variedades { get; set; } = new List<Variedad>();
    // define el constructor vacio
    public TamanioGrano()
    {
    }
    // define el constructor con parametros
    public TamanioGrano(string nombre)
    {
        Nombre = nombre;
    }
}
