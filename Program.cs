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
    private static async Task Main(string[] args)
    {
        // esto es para crear la base de datos una vez se ejecute el programa, a su vez, los ejecuta con unos inserts de prueba de toda la base de datos
        DbUtil dbUtil = new DbUtil();
        dbUtil.CrearBaseDeDatos("server=localhost;user=root;password=BRAYDEN714bRayden714;", "proyecto_cs");
        dbUtil.CrearInserts("server=localhost;user=root;password=BRAYDEN714bRayden714;", "proyecto_cs");
        var context = DbContextFactory.Create();
        // esto es para facillitar el frontend de la consola
        Console.OutputEncoding = Encoding.UTF8;

        // esto es para poder ejecutar mensaje de bienvenida
        var cartelBienvenida = new MenuPrincipal();
        await cartelBienvenida.CartelBienvenida();

        // esto es para ejecutar el menu del programa principal (login y registro)
        var menuPrincipal = new MenuPrincipal();
        await menuPrincipal.IniciarAplicacion();
    }
}