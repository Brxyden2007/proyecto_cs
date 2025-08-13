using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.administradores.Domain.Entities;

namespace proyecto_cs;

public class Persona
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public int Edad { get; set; }
    public string? Nacionalidad { get; set; }
    public int DocumentoIdentidad { get; set; }
    public string? Genero { get; set; }
    // esto se coloca ya que persona forma parte de la relación de usuarios y administradores 
    public Administrador Administrador { get; set; }  = null!;
    public Usuario Usuario { get; set; }  = null!;
    // define el constructor vacio
    public Persona()
    {
    }
    // define el constructor con parametros
    public Persona(string nombre, string apellido, int edad, string nacionalidad, int documento_identidad, string genero)
    {
        Nombre = nombre;
        Apellido = apellido;
        Edad = edad;
        Nacionalidad = nacionalidad;
        DocumentoIdentidad = documento_identidad;
        Genero = genero;
    }
}