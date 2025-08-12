using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;

public class Persona
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public string? apellido { get; set; }
    public int edad { get; set; }
    public string? nacionalidad { get; set; }
    public int documento_identidad { get; set; }
    public string? genero { get; set; }
    // Deduzco que despues se debera agregar la Foreign Key de Usuario y Admin.
    // public int usuario_id { get; set; }
    // public int admin_id { get; set; }
    // Son posibles ejemplos de FK.
    // public Usuario? usuario { get; set; }
    // public Admin? admin { get; set; }
}