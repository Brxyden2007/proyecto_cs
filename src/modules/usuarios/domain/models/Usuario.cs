using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    // se establece la relacion de uno a uno con persona
    public Persona Persona { get; set; } = null!;
    // define el constructor vacio
    public Usuario()
    {
    }
    // define el constructor con parametros
    public Usuario(string nombre, string apellido, string email, string passwordHash)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        PasswordHash = passwordHash;
    }
}