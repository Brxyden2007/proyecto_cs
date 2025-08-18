using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_cs.src.shared.utils;
using proyecto_cs.src.shared.utils.pdf;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace proyecto_cs;
internal class Program
{
    private static void Main(string[] args)
    {
        // esto es para crear la base de datos una vez se ejecute el programa, a su vez, los ejecuta con unos inserts de prueba de toda la base de datos
        DbUtil dbUtil = new DbUtil();
        dbUtil.CrearBaseDeDatos("server=localhost;user=campus2023;password=campus2023;", "proyecto_cs");
        dbUtil.CrearInserts("server=localhost;user=campus2023;password=campus2023;", "proyecto_cs");
        var context = DbContextFactory.Create();
        // esto es para facillitar el frontend de la consola
        Console.OutputEncoding = Encoding.UTF8;
        
        // esto es para poder ejecutar mensaje de bienvenida
        var cartelBienvenida = new MenuPrincipal();
        cartelBienvenida.CartelBienvenida();

        // esto es para ejecutar el menu del programa principal (login y registro)
        var menuPrincipal = new MenuPrincipal();
        _ = menuPrincipal.IniciarAplicacion();
        
        // 1. Crear el contexto de base de datos

        // var context = DbContextFactory.Create();

        // // esto es lo que he usado para crear unicamente una variedad
        // var variedad = context.Variedades.FirstOrDefault(v => v.IdVariedad == 4);

        // if (variedad == null)
        // {
        //     Console.WriteLine("No se encontrÃ³ la variedad solicitada.");
        //     return;
        // }
        // // 2. Generar el PDF para de un id en especifico
        // var generator = new VariedadPdfGenerator(variedad);
        // generator.Compose(context);
        // // // crear todas las variedades
        // // var todasGenerator = new VariedadesTodasPdfGenerator(context);
        // // todasGenerator.GenerateAll();

        // Console.WriteLine("PDFs generados para todas las variedades.");
    }
}