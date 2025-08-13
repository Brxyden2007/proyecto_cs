using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class Porte
{
    public int IdPorte { get; set; }
    public string Nombre { get; set; } = string.Empty;
    // se relaciona con la entidad Variedad
    public ICollection<Variedad> Variedades { get; set; } = new List<Variedad>();
    // define el constructor vacio
    public Porte()
    {
    }
    // define el constructor con parametros
    public Porte(string nombre)
    {
        Nombre = nombre;
    }
}