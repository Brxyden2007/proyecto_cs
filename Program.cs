using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_cs.src.shared.utils.pdf;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace proyecto_cs;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        // var cartelBienvenida = new MenuPrincipal();
        // await cartelBienvenida.CartelBienvenida();

        // var menuPrincipal = new MenuPrincipal();
        // await menuPrincipal.IniciarAplicacion();
        
        // 1. Crear el contexto de base de datos

        var context = DbContextFactory.Create();

        // esto es lo que he usado para crear unicamente una variedad
        var variedad = context.Variedades.FirstOrDefault(v => v.IdVariedad == 4);

        if (variedad == null)
        {
            Console.WriteLine("No se encontrÃ³ la variedad solicitada.");
            return;
        }
        // 2. Generar el PDF para de un id en especifico
        var generator = new VariedadPdfGenerator(variedad);
        generator.Compose(context);
        // // crear todas las variedades
        // var todasGenerator = new VariedadesTodasPdfGenerator(context);
        // todasGenerator.GenerateAll();

        Console.WriteLine("PDFs generados para todas las variedades.");
    }
}