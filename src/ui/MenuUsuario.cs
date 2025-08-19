using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_cs.src.modules.usuarios.domain.models;
using proyecto_cs.src.modules.variedades.application.services;
using proyecto_cs.src.modules.variedades.domain.models;
using proyecto_cs.src.shared.utils.pdf;

namespace proyecto_cs;
public class MenuUsuario
{
    private readonly Usuario usuario;
    private readonly string[] opcionesMenu;
    private readonly VariedadService _variedadService;
    private readonly AppDbContext _context;
    private int opcionSeleccionada = 0;

    public MenuUsuario(Usuario usuarioActual)
    {
        usuario = usuarioActual;
        _context = DbContextFactory.Create();
        _variedadService = new VariedadService(_context);
        opcionesMenu = new string[]
        {
            "Ver catalogo completo de variedades",
            "Filtrar variedades",
            "Ver ficha técnica de una variedad",
            "Generar PDF",
            "Salir/Cerrar Sesion"
        };
    }

    public async Task MostrarMenu()
    {
        MostrarBienvenidaUsuario();
        
        bool continuar = true;
        do
        {
            DibujarMenu();
            var tecla = Console.ReadKey(true);

            switch (tecla.Key)
            {
                case ConsoleKey.UpArrow:
                    opcionSeleccionada = (opcionSeleccionada - 1 + opcionesMenu.Length) % opcionesMenu.Length;
                    break;

                case ConsoleKey.DownArrow:
                    opcionSeleccionada = (opcionSeleccionada + 1) % opcionesMenu.Length;
                    break;

                case ConsoleKey.Enter:
                    continuar = await EjecutarOpcion(opcionSeleccionada);
                    break;
            }
        } while (continuar);
    }

    private void MostrarBienvenidaUsuario()
    {
        Console.Clear();
        MenuPrincipal.EscribirConPausa("===============================================================", 10);
        MenuPrincipal.EscribirConPausa("  ▒█▀▀█ ▀█▀ ▒█▀▀▀ ▒█▄░▒█ ▒█░░▒█ ▒█▀▀▀ ▒█▄░▒█ ▀█▀ ▒█▀▀▄ ▒█▀▀▀█",10); 
        MenuPrincipal.EscribirConPausa("  ▒█▀▀▄ ▒█░ ▒█▀▀▀ ▒█▒█▒█ ░▒█▒█░ ▒█▀▀▀ ▒█▒█▒█ ▒█░ ▒█░▒█ ▒█░░▒█",10); 
        MenuPrincipal.EscribirConPausa("  ▒█▄▄█ ▄█▄ ▒█▄▄▄ ▒█░░▀█ ░░▀▄▀░ ▒█▄▄▄ ▒█░░▀█ ▄█▄ ▒█▄▄▀ ▒█▄▄▄█",10);
        MenuPrincipal.EscribirConPausa($"    Usuario {usuario.Nombre} {usuario.Apellido} 🌱!", 10);
        MenuPrincipal.EscribirConPausa("===============================================================", 10);
        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private void DibujarMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=====================================================");
        Console.WriteLine("█░█ █▀ █▀▀ █▀█   █▀▄▀█ █▀▀ █▄░█ █░█");
        Console.WriteLine("█▄█ ▄█ ██▄ █▀▄   █░▀░█ ██▄ █░▀█ █▄█");
        Console.WriteLine("=====================================================");
        Console.ResetColor();

        for (int i = 0; i < opcionesMenu.Length; i++)
        {
            if (i == opcionSeleccionada)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"🌱 {opcionesMenu[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {opcionesMenu[i]}");
            }
        }
        Console.WriteLine("\nUsa las flechas ↑ ↓ para moverte y Enter para seleccionar.");
    }

    private async Task<bool> EjecutarOpcion(int opcion)
    {
        switch (opcion)
        {
            case 0: // Ver catálogo completo
                await MostrarCatalogoCompleto();
                break;
            case 1: // Filtrar variedades
                await MostrarMenuFiltros();
                break;
            case 2: // Ver ficha técnica
                await MostrarFichaTecnica();
                break;
            case 3: // Generar PDF
                MenuPdf menuPdf = new MenuPdf();
                _ = menuPdf.EjecutarMenuMain();
                break;
            case 4: // Salir
                return false;
        }
        return true;
    }

    private async Task MostrarCatalogoCompleto()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("║            CATÁLOGO COMPLETO         ║");
            Console.WriteLine("║       🌱  VARIEDADES DE CAFÉ  🌱    ║");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();

        try
        {
            int paginaActual = 1;
            int tamanoPagina = 5;
            bool continuar = true;

            while (continuar)
            {
                var variedades = await _variedadService.ObtenerVariedadesPaginadasAsync(paginaActual, tamanoPagina);
                var listaVariedades = variedades.ToList();

                if (!listaVariedades.Any())
                {
                    Console.WriteLine("No hay variedades disponibles.");
                    Console.ReadKey();
                    break;
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("║           CATÁLOGO COMPLETO          ║");
            Console.WriteLine("║        🌱  PAGINA ACTUAL  🌱        ║");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();

                for (int i = 0; i < listaVariedades.Count; i++)
                {
                    var variedad = listaVariedades[i];
                    Console.WriteLine($"\n🌱 {i + 1}. {variedad.NombreComun}");
                    Console.WriteLine($"   Científico: {variedad.NombreCientifico}");
                    Console.WriteLine($"   Porte: {variedad.Porte.Nombre}");
                    Console.WriteLine($"   Tamaño: {variedad.TamanioGrano.Nombre}");
                    Console.WriteLine($"   Rendimiento: {variedad.Rendimiento.Nivel}");
                    Console.WriteLine("   " + new string('-', 50));
                }

                Console.WriteLine($"\n[A] Página Anterior | [S] Página Siguiente | [Q] Salir");
                var tecla = Console.ReadKey(true);

                switch (tecla.Key)
                {
                    case ConsoleKey.A:
                        if (paginaActual > 1) paginaActual--;
                        break;
                    case ConsoleKey.S:
                        paginaActual++;
                        break;
                    case ConsoleKey.Q:
                        continuar = false;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error al cargar el catálogo: {ex.Message}");
            Console.ResetColor();
            Console.ReadKey();
        }
    }

    private async Task MostrarMenuFiltros()
    {
        string[] opcionesFiltros = {
            "Filtrar por nombre",
            "Filtrar por nombre científico", 
            "Filtrar por porte",
            "Filtrar por tamaño de grano",
            "Filtrar por altitud",
            "Filtrar por potencial de rendimiento",
            "Filtrar por resistencia a enfermedades",
            "Filtrar por tipo de variedad",
            "Filtrar por atributos agronómicos",
            "Filtrar por historia genética",
            "Regresar al menú de usuario"
        };

        int opcionSeleccionadaFiltro = 0;
        bool continuarFiltros = true;

        while (continuarFiltros)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("║          FILTROS DE BÚSQUEDA         ║");
            Console.WriteLine("║        🔍  EN VARIEDADES  🔍        ║");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();

            for (int i = 0; i < opcionesFiltros.Length; i++)
            {
                if (i == opcionSeleccionadaFiltro)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"🔍 {opcionesFiltros[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {opcionesFiltros[i]}");
                }
            }

            Console.WriteLine("\nUsa las flechas ↑ ↓ para moverte y Enter para seleccionar.");
            var tecla = Console.ReadKey(true);

            switch (tecla.Key)
            {
                case ConsoleKey.UpArrow:
                    opcionSeleccionadaFiltro = (opcionSeleccionadaFiltro - 1 + opcionesFiltros.Length) % opcionesFiltros.Length;
                    break;
                case ConsoleKey.DownArrow:
                    opcionSeleccionadaFiltro = (opcionSeleccionadaFiltro + 1) % opcionesFiltros.Length;
                    break;
                case ConsoleKey.Enter:
                    continuarFiltros = await EjecutarFiltro(opcionSeleccionadaFiltro);
                    break;
            }
        }
    }

    private async Task<bool> EjecutarFiltro(int opcion)
    {
        try
        {
            switch (opcion)
            {
                case 0: // Filtrar por nombre
                    Console.Clear();
                    Console.Write("Ingrese el nombre a buscar: ");
                    string nombre = Console.ReadLine() ?? "";
                    var variedadesPorNombre = await _variedadService.FiltrarPorNombreAsync(nombre);
                    MostrarResultadosFiltro(variedadesPorNombre, $"Resultados para nombre: {nombre}");
                    break;

                case 1: // Filtrar por nombre científico
                    Console.Clear();
                    Console.Write("Ingrese el nombre científico a buscar: ");
                    string nombreCientifico = Console.ReadLine() ?? "";
                    var variedadesPorCientifico = await _variedadService.FiltrarPorNombreCientificoAsync(nombreCientifico);
                    MostrarResultadosFiltro(variedadesPorCientifico, $"Resultados para nombre científico: {nombreCientifico}");
                    break;

                case 2: // Filtrar por porte
                    Console.Clear();
                    await FiltrarPorPorte();
                    break;

                case 3: // Filtrar por tamaño de grano
                    Console.Clear();
                    await FiltrarPorTamanioGrano();
                    break;

                case 4: // Filtrar por altitud
                    Console.Clear();
                    await FiltrarPorAltitud();
                    break;

                case 5: // Filtrar por rendimiento
                    Console.Clear();
                    await FiltrarPorRendimiento();
                    break;

                case 6: // Filtrar por resistencia
                    Console.Clear();
                    await FiltrarPorResistencia();
                    break;

                case 7:
                    Console.Clear();
                    Console.Write("Ingrese el tipo de variedad a buscar: ");
                    string tipo = Console.ReadLine() ?? "";
                    var variedadesPorTipo = await _variedadService.FiltrarPorTipoVariedadAsync(tipo);
                    MostrarResultadosFiltro(variedadesPorTipo, $"Resultados para tipo: {tipo}");
                    break;

                case 8: // Filtrar por atributos agronómicos
                    Console.Clear();
                    await FiltrarPorAtributoAgronomico();
                    break;

                case 9: // Filtrar por historia genética
                    Console.Clear();
                    await FiltrarPorHistoriaGenetica();
                    break;

                case 10: // Regresar
                    Console.Clear();
                    return false;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error en el filtro: {ex.Message}");
            Console.ResetColor();
            Console.ReadKey();
        }
        return true;
    }

    private async Task FiltrarPorPorte()
    {
        Console.Clear();
        Console.WriteLine("Seleccione el porte:");
        var portes = await _context.Portes.ToListAsync();
        
        for (int i = 0; i < portes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {portes[i].Nombre}");
        }
        
        Console.Write("Ingrese el número: ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion > 0 && opcion <= portes.Count)
        {
            var porte = portes[opcion - 1];
            var variedades = await _variedadService.FiltrarPorPorteAsync(porte.IdPorte);
            MostrarResultadosFiltro(variedades, $"Resultados para porte: {porte.Nombre}");
        }
    }

    private async Task FiltrarPorTamanioGrano()
    {
        Console.Clear();
        Console.WriteLine("Seleccione el tamaño de grano:");
        var tamanios = await _context.TamaniosGranos.ToListAsync();
        
        for (int i = 0; i < tamanios.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tamanios[i].Nombre}");
        }
        
        Console.Write("Ingrese el número: ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion > 0 && opcion <= tamanios.Count)
        {
            var tamanio = tamanios[opcion - 1];
            var variedades = await _variedadService.FiltrarPorTamanioGranoAsync(tamanio.IdTamanio);
            MostrarResultadosFiltro(variedades, $"Resultados para tamaño: {tamanio.Nombre}");
        }
    }

    private async Task FiltrarPorAltitud()
    {
        Console.Clear();
        Console.WriteLine("Seleccione la altitud:");
        var altitudes = await _context.Altitudes.ToListAsync();
        
        for (int i = 0; i < altitudes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {altitudes[i].Rango}");
        }
        
        Console.Write("Ingrese el número: ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion > 0 && opcion <= altitudes.Count)
        {
            var altitud = altitudes[opcion - 1];
            var variedades = await _variedadService.FiltrarPorAltitudAsync(altitud.IdAltitud);
            MostrarResultadosFiltro(variedades, $"Resultados para altitud: {altitud.Rango}");
        }
    }

    private async Task FiltrarPorRendimiento()
    {
        Console.Clear();
        Console.WriteLine("Seleccione el rendimiento:");
        var rendimientos = await _context.Rendimientos.ToListAsync();
        
        for (int i = 0; i < rendimientos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {rendimientos[i].Nivel}");
        }
        
        Console.Write("Ingrese el número: ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion > 0 && opcion <= rendimientos.Count)
        {
            var rendimiento = rendimientos[opcion - 1];
            var variedades = await _variedadService.FiltrarPorRendimientoAsync(rendimiento.IdRendimiento);
            MostrarResultadosFiltro(variedades, $"Resultados para rendimiento: {rendimiento.Nivel}");
        }
    }

    private async Task FiltrarPorResistencia()
    {
        Console.Clear();
        Console.WriteLine("Seleccione la resistencia:");
        var resistencias = await _context.Resistencias.ToListAsync();
        
        for (int i = 0; i < resistencias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {resistencias[i].Enfermedad}");
        }
        
        Console.Write("Ingrese el número: ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion > 0 && opcion <= resistencias.Count)
        {
            var resistencia = resistencias[opcion - 1];
            var variedades = await _variedadService.FiltrarPorResistenciaAsync(resistencia.IdResistencia);
            MostrarResultadosFiltro(variedades, $"Resultados para resistencia: {resistencia.Enfermedad}");
        }
    }

    private async Task FiltrarPorAtributoAgronomico()
    {
        Console.Clear();
        Console.WriteLine("Seleccione el atributo agronómico:");
        var atributos = await _context.AtributosAgronomicos.ToListAsync();
        
        for (int i = 0; i < atributos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {atributos[i].IdAtributo} - {atributos[i].TiempoCosecha} - {atributos[i].Maduracion} - {atributos[i].Nutricion} - {atributos[i].DensidadSiembra}");
        }
        
        Console.Write("Ingrese el número: ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion > 0 && opcion <= atributos.Count)
        {
            var atributo = atributos[opcion - 1];
            var variedades = await _variedadService.FiltrarPorAtributoAgronomicoAsync(atributo.IdAtributo);
            MostrarResultadosFiltro(variedades, $"Resultados para atributo: {atributo.IdAtributo} - {atributo.TiempoCosecha} - {atributo.Maduracion} - {atributo.Nutricion} - {atributo.DensidadSiembra}");
        }
    }

    private async Task FiltrarPorHistoriaGenetica()
    {
        Console.Clear();
        Console.WriteLine("Seleccione la historia genética:");
        var historias = await _context.HistoriasGeneticas.ToListAsync();
        
        for (int i = 0; i < historias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {historias[i].Descripcion}");
        }
        
        Console.Write("Ingrese el número: ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion > 0 && opcion <= historias.Count)
        {
            var historia = historias[opcion - 1];
            var variedades = await _variedadService.FiltrarPorHistoriaGeneticaAsync(historia.IdHistoria);
            MostrarResultadosFiltro(variedades, $"Resultados para historia: {historia.Descripcion}");
        }
    }

    private void MostrarResultadosFiltro(IEnumerable<Variedad> variedades, string titulo)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║ {titulo.PadRight(60)}🔍 ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        var lista = variedades.ToList();
        if (!lista.Any())
        {
            Console.WriteLine("No se encontraron variedades con ese criterio.");
        }
        else
        {
            for (int i = 0; i < lista.Count; i++)
            {
                var variedad = lista[i];
                Console.WriteLine($"\n🌱 {i + 1}. {variedad.NombreComun}");
                Console.WriteLine($"   Científico: {variedad.NombreCientifico}");
                Console.WriteLine($"   Porte: {variedad.Porte.Nombre}");
                Console.WriteLine($"   Tamaño: {variedad.TamanioGrano.Nombre}");
                Console.WriteLine("   " + new string('-', 50));
            }
        }

        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private async Task MostrarFichaTecnica()
    {
        Console.Clear();
        Console.WriteLine("Ingrese el ID de la variedad para ver su ficha técnica:");
        
        try
        {
            // Mostrar lista de variedades disponibles
            var todasVariedades = await _variedadService.ObtenerTodasLasVariedadesAsync();
            var lista = todasVariedades.ToList();
            
            Console.WriteLine("\nVariedades disponibles:");
            for (int i = 0; i < Math.Min(lista.Count, 10); i++)
            {
                Console.WriteLine($"{lista[i].IdVariedad}. {lista[i].NombreComun}");
            }
            
            Console.Write("\nIngrese el ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var variedad = await _variedadService.ObtenerVariedadPorIdAsync(id);
                if (variedad != null)
                {
                    MostrarFichaTecnicaDetallada(variedad);
                }
                else
                {
                    Console.WriteLine("Variedad no encontrada.");
                    Console.ReadKey();
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            Console.ReadKey();
        }
    }

    private void MostrarFichaTecnicaDetallada(Variedad variedad)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("║             FICHA TÉCNICA            ║");
            Console.WriteLine("║            🌱  VARIEDAD  🌱         ║");
            Console.WriteLine("║ -------------------------------------║");
            Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine($"\n📋 INFORMACIÓN GENERAL");
        Console.WriteLine($"   Nombre Común: {variedad.NombreComun}");
        Console.WriteLine($"   Nombre Científico: {variedad.NombreCientifico}");
        Console.WriteLine($"   Descripción: {variedad.Descripcion}");

        Console.WriteLine($"\n🌿 CARACTERÍSTICAS FÍSICAS");
        Console.WriteLine($"   Porte: {variedad.Porte?.Nombre}");
        Console.WriteLine($"   Tamaño de Grano: {variedad.TamanioGrano?.Nombre}");

        Console.WriteLine($"\n🏔️ CONDICIONES AMBIENTALES");
        Console.WriteLine($"   Altitud: {variedad.Altitud?.Rango ?? "N/A"}");
        Console.WriteLine($"   Calidad de Altitud: {variedad.CalidadAltitud?.Nivel ?? "N/A"}");

        Console.WriteLine($"\n📈 RENDIMIENTO");
        Console.WriteLine($"   Potencial: {variedad.Rendimiento?.Nivel ?? "N/A"}");

        if (variedad.VariedadResistencias.Any())
        {
            Console.WriteLine($"\n🛡️ RESISTENCIAS");
            foreach (var vr in variedad.VariedadResistencias)
            {
                Console.WriteLine($"   • {vr.Resistencia?.Enfermedad ?? "N/A"} - Nivel: {vr.Resistencia?.Nivel ?? "N/A"}");
            }
        }

        if (variedad.AtributosAgronomicos.Any())
        {
            Console.WriteLine($"\n🌾 ATRIBUTOS AGRONÓMICOS");
            foreach (var attr in variedad.AtributosAgronomicos)
            {
                Console.WriteLine($"   • {attr.IdAtributo} - Tiempo de Cosecha: {attr.TiempoCosecha} - Maduración: {attr.Maduracion} - Nutrición: {attr.Nutricion} - Densidad de Siembra: {attr.DensidadSiembra}");
            }
        }

        if (variedad.HistoriasGeneticas.Any())
        {
            Console.WriteLine($"\n🧬 HISTORIA GENÉTICA");
            foreach (var historia in variedad.HistoriasGeneticas)
            {
                Console.WriteLine($"   • {historia.Descripcion}");
            }
        }

        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }
/*
    private async Task GenerarPDF()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    GENERAR PDF                              ║");
        Console.WriteLine("║                  📄 REPORTES 📄                            ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        Console.WriteLine("\nSeleccione el tipo de reporte:");
        Console.WriteLine("1. PDF de todas las variedades");
        Console.WriteLine("2. PDF de una variedad específica");
        Console.WriteLine("3. PDF de variedades filtradas");
        Console.WriteLine("4. Cancelar");

        Console.Write("\nSeleccione una opción: ");
        string opcion = Console.ReadLine() ?? "";

        try
        {
            switch (opcion)
            {
                case "1":
                    var todasVariedades = await _variedadService.ObtenerTodasLasVariedadesAsync();
                    GenerarPDFVariedades(todasVariedades.ToList(), "Catálogo_Completo_Variedades");
                    break;

                case "2":
                    Console.Write("Ingrese el ID de la variedad: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var variedad = await _variedadService.ObtenerVariedadPorIdAsync(id);
                        if (variedad != null)
                        {
                            GenerarPDFVariedades(new List<Variedad> { variedad }, $"Ficha_Tecnica_{variedad.NombreComun}");
                        }
                        else
                        {
                            Console.WriteLine("Variedad no encontrada.");
                        }
                    }
                    break;

                case "3":
                    Console.WriteLine("Funcionalidad de PDF filtrado disponible después de aplicar filtros.");
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error al generar PDF: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    private void GenerarPDFVariedades(List<Variedad> variedades, string nombreArchivo)
    {
        try
        {
            var pdfGenerator = new PdfGenerator();
            string rutaArchivo = pdfGenerator.GenerarPDFVariedades(variedades, nombreArchivo);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ PDF generado exitosamente: {rutaArchivo}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Error al generar PDF: {ex.Message}");
            Console.ResetColor();
        }
    }*/
}
