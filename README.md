# ☕ Colombian Coffee - Sistema de Gestión de Variedades de Café

## 📌 Descripción del proyecto
Colombian Coffee es una aplicación interactiva en **C# (.NET)** para la gestión, clasificación y exploración de variedades de café cultivadas en Colombia.  
El sistema permite visualizar fichas técnicas detalladas, filtrar por múltiples atributos agronómicos, generar catálogos PDF personalizados y administrar el contenido desde un **panel de control con autenticación**.  
El frontend será una **interfaz de consola avanzada**, y el backend se implementará con **Entity Framework Core + MySQL**, cumpliendo con **principios SOLID** y **arquitectura de puertos y adaptadores con Vertical Slicing**.

---

## 🚀 Características principales
- **Exploración de variedades**
  - Fichas técnicas con nombre común/científico, imagen, descripción y atributos.
  - Información agronómica, historia y linaje genético.
- **Filtros dinámicos**
  - Filtrado por porte, tamaño de grano, altitud, resistencia a enfermedades, etc.
  - Búsqueda avanzada con combinaciones de criterios.
- **Panel administrativo**
  - CRUD completo de variedades.
  - Gestión de usuarios y autenticación.
- **Generador de PDF**
  - Exportación de variedades filtradas a un catálogo PDF personalizado.
  - Vista previa antes de la descarga.
- **Frontend de consola**
  - Navegación intuitiva con menús, flechas y validaciones.
  - Alertas visuales para errores y confirmaciones.

---

## 🛠️ Tecnologías utilizadas
- **Lenguaje:** C# (.NET 9.0)
- **ORM:** Entity Framework Core
- **Base de datos:** MySQL 8.x
- **Arquitectura:** Puertos y adaptadores (Hexagonal) + Vertical Slicing
- **Principios:** SOLID, Clean Code
- **Generación de PDF:** [QuestPDF](https://www.questpdf.com/) o similar compatible con .NET
- **Control de versiones:** Git + Gitflow

---

## 📂 Estructura del proyecto
```plaintext
<proyecto_cs>/
├── src/
│   ├── Modules/
│   │   ├── Personas/
│   │   │   ├── Domain/
│   │   │   │   └── models/
│   │   │   │       └── Persona.cs
│   │   │   ├── Application/
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── IPersonaService.cs
│   │   │   │   │   ├── IPersonaRepository.cs
│   │   │   │   ├── Services/
│   │   │   │   │   └── PersonaService.cs
│   │   │   ├── Infrastructure/
│   │   │   │   └── Repository/
│   │   │   │       └── PersonaRepository.cs
│   │   │   └── UI/
│   │   │       └── MenuPersonas.cs
│   │   ├── Usuarios/
│   │   │   ├── Domain/
│   │   │   │   └── models/
│   │   │   │       └── Usuario.cs
│   │   │   ├── Application/
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── IUsuarioService.cs
│   │   │   │   │   ├── IUsuarioRepository.cs
│   │   │   │   ├── Services/
│   │   │   │   │   └── UsuarioService.cs
│   │   │   ├── Infrastructure/
│   │   │   │   └── Repository/
│   │   │   │       └── UsuarioRepository.cs
│   │   │   └── UI/
│   │   │       └── MenuUsuarios.cs
│   │   ├── Variedades/
│   │   │   ├── Domain/
│   │   │   │   └── models/
│   │   │   │       ├── Variedad.cs
│   │   │   │       ├── AtributosAgronomicos.cs
│   │   │   │       └── HistoriaGenetica.cs
│   │   │   ├── Application/
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── IVariedadService.cs
│   │   │   │   │   ├── IVariedadRepository.cs
│   │   │   │   ├── Services/
│   │   │   │   │   └── VariedadService.cs
│   │   │   ├── Infrastructure/
│   │   │   │   └── Repository/
│   │   │   │       └── VariedadRepository.cs
│   │   │   └── UI/
│   │   │       └── MenuVariedades.cs
│   ├── Shared/
│   │   ├── Configurations/   # Fluent API para EF Core
│   │   │   ├── PersonaConfig.cs
│   │   │   ├── UsuarioConfig.cs
│   │   │   ├── VariedadConfig.cs
│   │   │   ├── AtributosAgronomicosConfig.cs
│   │   │   └── HistoriaGeneticaConfig.cs
│   │   ├── Context/
│   │   │   └── AppDbContext.cs
│   │   ├── Helpers/
│   │   │   ├── DbContextFactory.cs
│   │   │   └── MySqlVersionResolver.cs
│   │   └── Utils/
│   │       ├── ConsoleUIHelpers.cs
│   │       └── PdfGenerator.cs
├── appsettings.json
├── proyecto_cs.csproj
├── proyecto_cs.sln
└── Program.cs
```

## diagrama 
