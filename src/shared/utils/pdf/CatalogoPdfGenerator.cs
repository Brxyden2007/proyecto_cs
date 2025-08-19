using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto_cs.src.modules.variedades.domain.models;
using QuestPDF.Fluent;

namespace proyecto_cs;
public class CatalogoPdfGenerator
{
    private readonly IEnumerable<Variedad> variedades;

    public CatalogoPdfGenerator(IEnumerable<Variedad> variedades)
    {
        this.variedades = variedades;
    }

    public byte[] Generate()
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Header().Element(c => PdfBaseStyles.Encabezado(c, "Catálogo de Variedades"));
                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(cols =>
                    {
                        cols.RelativeColumn(3); // nombre
                        cols.RelativeColumn(2); // porte
                        cols.RelativeColumn(2); // altitud
                        cols.RelativeColumn(2); // resistencia roya
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Nombre").Style(PdfBaseStyles.SubTitulo);
                        header.Cell().Text("Porte").Style(PdfBaseStyles.SubTitulo);
                        header.Cell().Text("Altitud").Style(PdfBaseStyles.SubTitulo);
                        header.Cell().Text("Roya").Style(PdfBaseStyles.SubTitulo);
                    });

                    foreach (var v in variedades)
                    {
                        table.Cell().Text(v.NombreComun);
                        table.Cell().Text(v.Porte.Nombre);
                        table.Cell().Text($"{v.Altitud} msnm");
                        table.Cell().Text(v.TamanioGrano.Nombre);
                    }
                });
            });
        }).GeneratePdf();
    }
}
