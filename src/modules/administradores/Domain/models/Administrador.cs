using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs.src.modules.administradores.Domain.Entities;

public class Administrador
{
    public int Id { get; set; }
    // Apunte: Recomendado probablemente usar sin null debido a que deberia ser requerido.
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    // se establece la relacion de uno a uno con persona
    public Persona Persona { get; set; } = null!;
    // define el constructor vacio
    public Administrador()
    {
    }
    // define el constructor con parametros
    public Administrador(string nombre, string email, string passwordHash)
    {
        Nombre = nombre;
        Email = email;
        PasswordHash = passwordHash;
    }
}
