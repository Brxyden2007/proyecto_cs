using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.administradores.Domain.Entities;
using proyecto_cs.src.modules.altitudes.domain.models;
using proyecto_cs.src.modules.atributos_agronomicos.domain.models;
using proyecto_cs.src.modules.calidades_altitudes.domain.models;
using proyecto_cs.src.modules.historias_geneticas.domain.models;
using proyecto_cs.src.modules.portes.domain.models;
using proyecto_cs.src.modules.rendimientos.domain.models;
using proyecto_cs.src.modules.resistencias.domain.models;
using proyecto_cs.src.modules.tamanios_granos.domain.models;
using proyecto_cs.src.modules.usuarios.domain.models;
using proyecto_cs.src.modules.variedad_resistencia.domain.models;
using proyecto_cs.src.modules.variedades.domain.models;

namespace proyecto_cs;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    // en esta parte deben de ir los DbSet de cada entidad, este es un ejemplo 
    // public DbSet<User> Users => Set<User>();
    public DbSet<Administrador> Administradors => Set<Administrador>();
    public DbSet<Altitud> Altitudes => Set<Altitud>();
    public DbSet<AtributoAgronomico> AtributosAgronomicos => Set<AtributoAgronomico>();
    public DbSet<CalidadAltitud> CalidadesAltitudes => Set<CalidadAltitud>();
    public DbSet<HistoriaGenetica> HistoriasGeneticas => Set<HistoriaGenetica>();
    public DbSet<Persona> Personas => Set<Persona>();
    public DbSet<Porte> Portes => Set<Porte>();
    public DbSet<Rendimiento> Rendimientos => Set<Rendimiento>();
    public DbSet<Resistencia> Resistencias => Set<Resistencia>();
    public DbSet<TamanioGrano> TamaniosGranos => Set<TamanioGrano>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<VariedadResistencia> VariedadResistencias => Set<VariedadResistencia>();
    public DbSet<Variedad> Variedades => Set<Variedad>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
