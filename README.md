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

## 🧱 Estructura General del Proyecto
```plaintext
<proyecto_cs>/
│
├── Program.cs
├── soccer_csharp.csproj
├── soccer_csharp.sln
├── README.md
├── appsettings.json
├── .gitignore
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
```
<!-- src/
  personas/
    domain/
      entities/
        Persona.cs
        Usuario.cs
        Administrador.cs
        LogAccion.cs
      repositories/
        IPersonaRepository.cs
        IUsuarioRepository.cs
        IAdministradorRepository.cs
        ILogAccionRepository.cs
      services/
        IPersonaService.cs
        IUsuarioService.cs
        ...
    application/
      usecases/
        CrearUsuario.cs
        EliminarUsuario.cs
        ...
    infrastructure/
      persistence/
        PersonaRepository.cs
        UsuarioRepository.cs
        ...
      mappers/
      dto/
    ui/
      console/
        PersonaMenu.cs
      api/
        PersonaController.cs

  catalogos/
    domain/
      entities/
        Porte.cs
        TamanioGrano.cs
        Altitud.cs
        ...
      repositories/
      services/
    application/
      usecases/
    infrastructure/
    ui/

  variedades/
    domain/
      entities/
        Variedad.cs
        VariedadResistencia.cs
        AtributoAgronomico.cs
        HistoriaGenetica.cs
      repositories/
      services/
    application/
      usecases/
    infrastructure/
    ui/

  pdfs/
    domain/
      entities/
        PdfCatalogo.cs
      repositories/
      services/
    application/
      usecases/
    infrastructure/
    ui/

shared/
  context/
    AppDbContext.cs
  data/
    IDbFactory.cs
    IGenericRepository.cs
    MySqlDbFactory.cs
  utils/
    Mappers.cs
    Validators.cs
 -->

## 🧭 Estructura del Menú Principal
(aqui la gracia es que al querer ingresar un usuario o un administrador, se puede hacer con el mismo menu y cuando lo ejecute se ejecutara el metodo correspondiente, en el caso de registro se crearia la persona)

1. Registro de administrador
2. Registro de usuario
3. Login administrador
4. Login usuario
5. Salir del programa 

### Submenús de registro de usuario
aqui se pedirian los datos de creacion de usuario, y luego se creara la persona, y luego se le asignara el registro en la tabla de usuario 

### Submenús de registro de administrador
aqui lo mismo que el registro de usuario pero en la tabla de administradores

### Submenús de login de administrar  
se va a pedir el email y password de un administrador, y luego se recorrela con la tabla de administradores, y si existe se le enviara al menu principal, si no se le mandara un mensaje de error

### Submenús de login usuario
lo mismo que en el login de administrador, pero en la tabla de usuarios 

---
### Menu principal usuario
1. Ver catalogo completo de variedades
2. Filtrar variedades 
3. Ver ficha técnica de una variedad
4. Generar PDF 
5. Cerrar sesion (volver al menu de login)

---
**variedades:**
- 1.1 Mostrar lista paginada de todas las variedades
- 1.2 Mostrar ficha tecnica detallada de una variedad

---
**Filtrar por:**
- 2.1 Filtrar por nombre
- 2.2 Filtrar por nombre cientifico
- 2.3 Filtrar por porte
- 2.4 FIltrar por tamanio de gramo
- 2.5 Filtrar por altitud
- 2.6 Filtrar por potencial de rendimiento
- 2.7 Filtrar por resistencia a enfermedades
- 2.8 Filtrar por tipo de variedad
- 2.9 Filtrar por atributos agronomicos
- 2.10 Filtrar por historia genetica

---
- 3.1 Mostrar lista paginada de todas las variedades
- 3.2 Mostrar ficha tecnica detallada de una variedad
- 3.3 Mostrar atributos agronomicos de una variedad
- 3.4 Mostrar historia genetica de una variedad

---
- 4.1 Generar pdf de una variedad
- 4.3 Generar pdf de todas las variedades
- 4.4 Generar PDF filtrado por características (por altitud, tamaño de grano, porte, rendimiento o resistencia.)
- 4.5 Generar PDF con ordenamiento personalizado (Ascendente o descendente por nombre, altitud, calidad, etc.)
- 4.6 Generar PDF con selección múltiple (El usuario marca ciertas variedades en el sistema y genera el catálogo solo con esas.)
- 4.7 Generar PDF resumido (Solo con nombre, imagen y características principales.)
- 4.8 Generar PDF detallado (Con ficha técnica completa, descripciones extensas e imágenes.)
---

### Menu principal administrador
(aqui seria colocar el crud basico de todas las tablas para que luego el usuario pueda realizar las consultas) 
1. CRUD variedad completa
<!-- 2. CRUD por de un atributo en específico -->
2. CRUD usuarios
3. CRUD administradores
4. Cerrar sesion (volver al menu de login)
---
(en el crud de variedades se coloca directamente el crud de atributos agronomicos y historia geneticas)
- 1.1 Crear una nueva variedad 
- 1.2 Actualizar una variedad
- 1.3 Eliminar una variedad
- 1.4 Mostrar todas las variedades
---
- 4.1 Crear un nuevo usuario
- 4.2 Actualizar un usuario
- 4.3 Eliminar un usuario
- 4.4 Mostrar todas las usuarios
---
- 5.1 Crear un nuevo administrador
- 5.2 Actualizar un administrador
- 5.3 Eliminar un administrador
- 5.4 Mostrar todas las administradores
---
# Proyecto Colombian Coffee - Sistema de Gestión de Variedades de Café
Integrantes: 
- **Ángel David Pinzón Serrano:** Backend principal, configuraciones, repositorios y pdfs
- **Brayden Nicolas Poveda Rueda:** Lider y gestor de proyectos 
- **Juan Sebastian Romero Cepeda:** Menus y funcionalides de interfaz y servicios  
- **Daniela Sofia Herrera Rojas:** Frontend y diseño de consola


