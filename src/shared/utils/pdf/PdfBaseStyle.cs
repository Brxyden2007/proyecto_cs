using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace proyecto_cs;
public static class PdfBaseStyles
{
    public static TextStyle Titulo => TextStyle.Default.FontSize(20).Bold().FontColor(Colors.Blue.Medium);
    public static TextStyle SubTitulo => TextStyle.Default.FontSize(14).SemiBold().FontColor(Colors.Grey.Darken2);
    public static TextStyle Texto => TextStyle.Default.FontSize(12).FontColor(Colors.Grey.Darken3);

    public static void Encabezado(IContainer container, string titulo)
    {
        container.Row(row =>
        {
            row.RelativeItem().Text(titulo).Style(Titulo);
            row.RelativeItem(50).Height(40).Background(Colors.Blue.Lighten1);
        });
    }
}
