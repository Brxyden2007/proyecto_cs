using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs;
public class Usuario
{
    public int id { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string password_hash { get; set; } = string.Empty;
    public DateTime created_at { get; set; } = DateTime.Now;
}