using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_cs.src.modules.administradores.Domain.Entities
{
    public class Admin
    {
        public int id { get; set; }
        // Apunte: Recomendado probablemente usar sin null debido a que deberia ser requerido.
        public string? nombre { get; set; }
        public string? email { get; set; }
        public string? password_hash { get; set; }
        public DateTime created_at { get; set; }

    }
}